using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

public static class DataGridViewExtensions
{
    private static Dictionary<int, object> originalData = new Dictionary<int, object>();
    private static HashSet<int> modifiedRowsIndex = new HashSet<int>();
    private static Dictionary<int, HashSet<int>> modifiedCells = new Dictionary<int, HashSet<int>>();
    private static List<object> deletedRows = new List<object>();

    /// <summary>
    /// 그리드의 기본 스타일을 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    public static void GridHwhSetting(this DataGridView grid)
    {
        grid.BackgroundColor = Color.WhiteSmoke;

        // 그리드 셀 기본 스타일
        var cellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.White,
            ForeColor = Color.Black,
            SelectionBackColor = Color.LightSkyBlue,
            SelectionForeColor = Color.Black,
            Font = new Font("Segoe UI", 12) // 셀 글자 크기 조절
        };

        grid.DefaultCellStyle = cellStyle;
        grid.AlternatingRowsDefaultCellStyle = cellStyle;

        // 그리드 헤더 스타일
        var headerStyle = new DataGridViewCellStyle
        {
            BackColor = Color.LightGray,
            ForeColor = Color.Black,
            Font = new Font("Segoe UI", 14, FontStyle.Bold) // 헤더 글자 크기 조절
        };

        grid.ColumnHeadersDefaultCellStyle = headerStyle;
        grid.GridColor = Color.Gray;
    }

    /// <summary>
    /// List<T>를 받아서 그리드에 데이터를 바인딩하는 확장 메서드
    /// </summary>
    /// <typeparam name="T">바인딩할 데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>
    public static void BindData<T>(this DataGridView grid, List<T> data) where T : class
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        BindingList<T> bindingList = new BindingList<T>(data);
        BindingSource bindingSource = new BindingSource
        {
            DataSource = bindingList
        };

        grid.DataSource = bindingSource;

        // 처음에는 읽기 전용으로 설정
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.EditMode = DataGridViewEditMode.EditProgrammatically;

        grid.Dock = DockStyle.Fill;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

    }

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
    /// 수정 모드를 활성화하거나 비활성화하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    public static void EnableEditMode(this DataGridView grid)
    {
        if (grid.ReadOnly)
        {
            // 수정 모드 활성화
            grid.ReadOnly = false;
            grid.AllowUserToAddRows = true;
            grid.AllowUserToDeleteRows = true;
            grid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            // 셀 값 변경 이벤트 핸들러 추가 (수정 모드 활성화 시)
            grid.CellValueChanged += Grid_CellValueChanged;
            grid.CellBeginEdit += Grid_CellBeginEdit;
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
        }
    }

    /// <summary>
    /// 그리드에서 수정된 모든 행의 수정 전후 데이터를 리턴하는 확장 메서드
    /// </summary>
    /// <typeparam name="T">바인딩할 데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <returns>수정된 행의 수정 전후 데이터를 포함하는 리스트</returns>
    public static List<ModifiedRow<T>> GetModifiedRows<T>(this DataGridView grid) where T : class, new()
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

                if (originalItem != null && modifiedItem != null)
                {
                    modifiedRows.Add(new ModifiedRow<T>
                    {
                        OriginalData = originalItem,
                        ModifiedData = modifiedItem,
                        RowIndex = rowIndex
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
    /// 수정 모드일 때 클릭한 행을 삭제하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    public static void DeleteSelectedRow(this DataGridView grid)
    {
        if (grid.ReadOnly)
        {
            MessageBox.Show("수정 모드가 아닙니다.");
            return;
        }

        if (grid.SelectedRows.Count > 0)
        {
            var selectedRow = grid.SelectedRows[0];
            deletedRows.Add(selectedRow.DataBoundItem);
            grid.Rows.Remove(selectedRow);
        }
    }

    /// <summary>
    /// 수정 모드일 때 삭제한 행을 출력하는 메서드
    /// </summary>
    /// <typeparam name="T">데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <returns>삭제된 행의 데이터 리스트</returns>
    public static List<T> GetDeletedRows<T>(this DataGridView grid) where T : class
    {
        return deletedRows.Cast<T>().ToList();
    }

    /// <summary>
    /// 수정된 행의 정보를 저장하는 클래스
    /// </summary>
    /// <typeparam name="T">데이터 타입</typeparam>
    public class ModifiedRow<T>
    {
        public T OriginalData { get; set; }
        public T ModifiedData { get; set; }
        public int RowIndex { get; set; }
    }

    /// <summary>
    /// 그리드의 해더를 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="headers">설정할 해더 리스트</param>
    public static void SetHeaders(this DataGridView grid, List<string> headers)
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        if (headers == null || headers.Count == 0)
        {
            MessageBox.Show("헤더 리스트가 null이거나 비어 있습니다.");
            return;
        }

        // 지정된 해더로 그리드의 컬럼 설정
        grid.Columns.Clear();
        foreach (var header in headers)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                DataPropertyName = header,
                Name = header
            });
        }

        grid.AutoGenerateColumns = false; // 자동 생성된 컬럼 비활성화
    }

    /// <summary>
    /// 그리드의 기존 해더를 사용하여 데이터를 바인딩하는 메서드
    /// </summary>
    /// <typeparam name="T">데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>
    public static void BindDataToHeaders<T>(this DataGridView grid, List<T> data) where T : class
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        if (data == null || data.Count == 0)
        {
            MessageBox.Show("데이터가 null이거나 비어 있습니다.");
            return;
        }

        // 데이터를 바인딩
        BindingList<T> bindingList = new BindingList<T>(data);
        BindingSource bindingSource = new BindingSource
        {
            DataSource = bindingList
        };

        grid.DataSource = bindingSource;

        // 기본 설정
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.EditMode = DataGridViewEditMode.EditProgrammatically;

        grid.Dock = DockStyle.Fill;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
