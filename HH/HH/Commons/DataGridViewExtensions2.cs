using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HH.Commons
{
    public static class DataGridViewExtensions2
    {
        private static Dictionary<int, object> originalData = new Dictionary<int, object>();
        private static HashSet<int> modifiedRowsIndex = new HashSet<int>();
        private static Dictionary<int, HashSet<int>> modifiedCells = new Dictionary<int, HashSet<int>>();
        private static List<ModifiedRow<object>> deletedRows = new List<ModifiedRow<object>>();

        /// <summary>
        /// 셀 편집 시작 시 호출되는 이벤트 핸들러
        /// </summary>
        private static void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count)
            {
                return;
            }

            var row = grid.Rows[e.RowIndex];
            if (!originalData.ContainsKey(e.RowIndex))
            {
                // Deep copy the original data to ensure modifications do not affect it
                originalData[e.RowIndex] = CloneData(row.DataBoundItem);
            }
        }


        /// <summary>
        /// 셀 값 변경 시 호출되는 이벤트 핸들러
        /// </summary>
        private static void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count)
            {
                return;
            }

            if (!modifiedCells.ContainsKey(e.RowIndex))
            {
                modifiedCells[e.RowIndex] = new HashSet<int>();
            }

            modifiedCells[e.RowIndex].Add(e.ColumnIndex);
            modifiedRowsIndex.Add(e.RowIndex);

            // 변경된 셀의 색상 변경
            grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
        }


        /// <summary>
        /// 신규 행 추가 시 호출되는 이벤트 핸들러
        /// </summary>
        private static void Grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count)
            {
                return;
            }

            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                if (!modifiedCells.ContainsKey(i))
                {
                    modifiedCells[i] = new HashSet<int>();
                }

                modifiedRowsIndex.Add(i);
            }
        }

        /// <summary>
        /// 수정 모드를 활성화하거나 비활성화하는 메서드
        /// </summary>
        /// <param name="grid">설정할 DataGridView</param>
        public static void EnableEditMode(this DataGridView grid)
        {
            if (grid.Tag?.ToString() == "DeleteMode")
            {
                MessageBox.Show("삭제 모드가 활성화되어 있습니다. 먼저 삭제 모드를 비활성화하세요.");
                return;
            }

            if (grid.ReadOnly)
            {
                // 수정 모드 활성화
                grid.ReadOnly = false;
                grid.AllowUserToAddRows = true;
                grid.AllowUserToDeleteRows = false; // 삭제 비활성화
                grid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

                // 셀 값 변경 이벤트 핸들러 추가 (수정 모드 활성화 시)
                grid.CellValueChanged += Grid_CellValueChanged;
                grid.CellBeginEdit += Grid_CellBeginEdit;
                grid.RowsAdded += Grid_RowsAdded;

                grid.Tag = "EditMode"; // 수정 모드 태그 설정
            }
            else
            {
                // 수정 모드 해제
                grid.ReadOnly = true;
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.EditMode = DataGridViewEditMode.EditProgrammatically;

                // 셀 값 변경 이벤트 핸들러 제거 (수정 모드 비활성화 시)
                grid.CellValueChanged -= Grid_CellValueChanged;
                grid.CellBeginEdit -= Grid_CellBeginEdit;
                grid.RowsAdded -= Grid_RowsAdded;

                grid.Tag = null; // 태그 제거
            }
        }


        /// <summary>
        /// 삭제 모드를 활성화하거나 비활성화하는 메서드
        /// </summary>
        /// <param name="grid">설정할 DataGridView</param>
        public static void EnableDeleteMode(this DataGridView grid)
        {
            if (grid.Tag?.ToString() == "EditMode")
            {
                MessageBox.Show("수정 모드가 활성화되어 있습니다. 먼저 수정 모드를 비활성화하세요.");
                return;
            }

            if (grid.Tag?.ToString() == "DeleteMode")
            {
                // 삭제 모드 해제
                grid.EditMode = DataGridViewEditMode.EditProgrammatically;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    if (column is DataGridViewCheckBoxColumn)
                    {
                        grid.Columns.Remove(column);
                    }
                }

                grid.Tag = null; // 태그 제거
            }
            else
            {
                // 삭제 모드 활성화
                grid.EditMode = DataGridViewEditMode.EditOnEnter;
                grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                var checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    HeaderText = "삭제",
                    Width = 30,
                    Name = "checkBoxColumn",
                    ReadOnly = false // 체크 박스를 편집 가능하게 설정
                };
                grid.Columns.Insert(0, checkBoxColumn);

                grid.Tag = "DeleteMode"; // 삭제 모드 태그 설정
            }
        }

        /// <summary>
        /// 수정 모드에서 변경된 모든 행의 수정 전후 데이터를 리턴하는 확장 메서드
        /// </summary>
        /// <typeparam name="T">데이터 타입</typeparam>
        /// <param name="grid">설정할 DataGridView</param>
        /// <returns>수정된 행의 수정 전후 데이터를 포함하는 리스트</returns>
        public static List<ModifiedRow<T>> GeteditRows<T>(this DataGridView grid) where T : class, new()
        {
            List<ModifiedRow<T>> modifiedRows = new List<ModifiedRow<T>>();

            if (grid == null || grid.DataSource == null)
            {
                MessageBox.Show("그리드가 null이거나 데이터 소스가 설정되지 않았습니다.");
                return modifiedRows;
            }

            BindingSource bindingSource = grid.DataSource as BindingSource;
            if (bindingSource == null)
            {
                MessageBox.Show("BindingSource가 설정되지 않았습니다.");
                return modifiedRows;
            }

            foreach (var rowIndex in modifiedRowsIndex)
            {
                if (rowIndex >= 0 && rowIndex < grid.Rows.Count && !grid.Rows[rowIndex].IsNewRow)
                {
                    var originalItem = originalData.ContainsKey(rowIndex) ? originalData[rowIndex] as T : null;
                    var modifiedItem = grid.Rows[rowIndex].DataBoundItem as T;

                    var situation = SituationType.None;

                    if (originalItem == null && modifiedItem != null)
                    {
                        situation = SituationType.New;
                    }
                    else if (originalItem != null && modifiedItem != null)
                    {
                        situation = SituationType.Updated;
                    }

                    if (originalItem != null || modifiedItem != null)
                    {
                        modifiedRows.Add(new ModifiedRow<T>
                        {
                            OriginalData = originalItem,
                            ModifiedData = modifiedItem,
                            OriginalRowIndex = rowIndex,
                            Situation = situation
                        });
                    }
                }
            }

            // 수정된 행 인덱스 초기화
            modifiedRowsIndex.Clear();
            originalData.Clear();
            modifiedCells.Clear();

            return modifiedRows;
        }


        /// <summary>
        /// 클릭한 행의 정보를 가져오는 메서드
        /// </summary>
        /// <typeparam name="T">데이터 타입</typeparam>
        /// <param name="grid">설정할 DataGridView</param>
        /// <returns>선택된 행의 데이터</returns>
        public static T GetSelectedRowData<T>(this DataGridView grid) where T : class
        {
            if (grid.SelectedRows.Count > 0)
            {
                return grid.SelectedRows[0].DataBoundItem as T;
            }
            return null;
        }


        /// <summary>
        /// 삭제 모드일 때 클릭한 행을 삭제하는 메서드
        /// </summary>
        /// <param name="grid">설정할 DataGridView</param>
        public static void DeleteSelectedRows(this DataGridView grid)
        {
            if (grid.EditMode != DataGridViewEditMode.EditOnEnter)
            {
                MessageBox.Show("삭제 모드가 아닙니다.");
                return;
            }

            for (int i = grid.Rows.Count - 1; i >= 0; i--)
            {
                if (grid.Rows[i].Cells["checkBoxColumn"] is DataGridViewCheckBoxCell checkBoxCell && (bool)checkBoxCell.Value == true)
                {
                    var originalItem = grid.Rows[i].DataBoundItem;
                    var rowIndex = grid.Rows[i].Index;

                    if (originalData.ContainsKey(rowIndex))
                    {
                        // 기존 데이터를 삭제
                        deletedRows.Add(new ModifiedRow<object>
                        {
                            OriginalData = originalItem,
                            ModifiedData = null,
                            OriginalRowIndex = rowIndex,
                            Situation = SituationType.Deleted
                        });
                        originalData.Remove(rowIndex); // originalData에서도 제거
                    }
                    else
                    {
                        // 신규 데이터를 삭제
                        if (originalItem != null)
                        {
                            deletedRows.Add(new ModifiedRow<object>
                            {
                                OriginalData = originalItem,
                                ModifiedData = null,
                                OriginalRowIndex = rowIndex,
                                Situation = SituationType.Deleted // 신규 데이터도 삭제로 설정
                            });
                        }
                    }

                    grid.Rows.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 삭제된 행을 출력하는 메서드
        /// </summary>
        /// <typeparam name="T">데이터 타입</typeparam>
        /// <param name="grid">설정할 DataGridView</param>
        /// <returns>삭제된 행의 데이터 리스트</returns>
        public static List<T> GetDeletedRows<T>(this DataGridView grid) where T : class
        {
            return deletedRows.Where(r => r.Situation == SituationType.Deleted).Select(r => r.OriginalData as T).ToList();
        }

        /// <summary>
        /// 객체를 깊은 복사하는 메서드
        /// </summary>
        /// <param name="source">복사할 객체</param>
        /// <returns>깊은 복사된 객체</returns>
        private static object CloneData(object source)
        {
            if (source == null)
            {
                return null;
            }

            var cloneMethod = source.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return cloneMethod?.Invoke(source, null);
        }

    }

    /// <summary>
    /// 수정된 행의 정보를 저장하는 클래스
    /// </summary>
    /// <typeparam name="T">데이터 타입</typeparam>
    public class ModifiedRow<T>
    {
        public T OriginalData { get; set; }
        public T ModifiedData { get; set; }
        public int OriginalRowIndex { get; set; }
        public SituationType Situation { get; set; }
    }

    /// <summary>
    /// 수정 상태를 나타내는 열거형
    /// </summary>
    public enum SituationType
    {
        None,
        New,
        Updated,
        Deleted
    }
}
