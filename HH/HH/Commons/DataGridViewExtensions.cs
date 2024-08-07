using HH.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class DataGridViewExtensions
{
    /// <summary>
    /// 그리드의 기본 스타일을 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    public static void GridHwhSetting(this DataGridView grid)
    {
        grid.BackgroundColor = Color.WhiteSmoke;
        grid.ScrollBars = ScrollBars.None; // 행 스크롤 비활성화

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
            Font = new Font("Segoe UI", 14, FontStyle.Bold)
        };

        grid.ColumnHeadersDefaultCellStyle = headerStyle;
        grid.GridColor = Color.Gray;
    }

    /// <summary>
    /// 컬럼 너비를 INI 파일에 저장하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="iniFilePath">INI 파일 경로</param>
    /// <param name="section">INI 파일 섹션 이름</param>
    public static void SaveColumnWidths(this DataGridView grid, string iniFilePath, string section)
    {
        IniFile iniFile = new IniFile(iniFilePath);

        foreach (DataGridViewColumn column in grid.Columns)
        {
            iniFile.Write(section, column.Name, column.Width.ToString());
        }
    }
    /// <summary>
    /// 지정된 행 수만큼 빈 행을 추가
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="rowCount">행 수</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>

    public static void AddEmptyRows(this DataGridView grid, int rowCount)
    {
        if (grid == null)
        {
            throw new ArgumentNullException(nameof(grid), "The DataGridView cannot be null.");
        }

        if (rowCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rowCount), "The number of rows must be a non-negative integer.");
        }

        // Add the specified number of empty rows
        for (int i = 0; i < rowCount; i++)
        {
            grid.Rows.Add();
        }
    }
    /// <summary>
    /// 행수 고정
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="rowCount">행 수</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>

    public static void EnsureRowCount(this DataGridView grid, int rowCount)
    {
        if (grid == null)
        {
            throw new ArgumentNullException(nameof(grid), "The DataGridView cannot be null.");
        }

        if (rowCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rowCount), "The number of rows must be a non-negative integer.");
        }

        // 현재 행 수를 확인하고 부족한 경우 빈 행을 추가합니다.
        int currentRowCount = grid.Rows.Count;
        if (currentRowCount < rowCount)
        {
            for (int i = currentRowCount; i < rowCount; i++)
            {
                grid.Rows.Add();
            }
        }
    }


    /// <summary>
    /// INI 파일에서 컬럼 너비를 불러오는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="iniFilePath">INI 파일 경로</param>
    /// <param name="section">INI 파일 섹션 이름</param>
    public static void LoadColumnWidths(this DataGridView grid, string iniFilePath, string section)
    {
        IniFile iniFile = new IniFile(iniFilePath);

        foreach (DataGridViewColumn column in grid.Columns)
        {
            string width = iniFile.Read(section, column.Name);
            if (!string.IsNullOrEmpty(width) && int.TryParse(width, out int colWidth))
            {
                column.Width = colWidth;
            }
        }
    }

    /// <summary>
    /// 그리드의 해더를 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="headers">설정할 해더 리스트</param>
    public static void SetCustomHeaders(this DataGridView grid, List<DataGridViewColumnSetting> columnSettings)
    {
        if (grid == null || columnSettings == null || columnSettings.Count == 0)
        {
            MessageBox.Show("그리드 또는 컬럼 설정이 null이거나 비어 있습니다.");
            return;
        }

        grid.Columns.Clear();
        grid.Tag = columnSettings; // columnSettings를 grid의 Tag 속성에 저장

        foreach (var setting in columnSettings)
        {
            DataGridViewColumn column = null;

            switch (setting.ColumnType)
            {
                case ColumnType.TextBox:
                    column = new DataGridViewTextBoxColumn
                    {
                        Name = setting.Name,
                        HeaderText = setting.Title,
                        Width = setting.Width,
                        DataPropertyName = setting.Name,
                        ReadOnly = false
                    };
                    break;

                case ColumnType.ComboBox:
                    var comboBoxColumn = new DataGridViewComboBoxColumn
                    {
                        Name = setting.Name,
                        HeaderText = setting.Title,
                        Width = setting.Width,
                        DataPropertyName = setting.Name,
                        ReadOnly = false
                    };

                    if (setting.ComboBoxItems != null)
                    {
                        comboBoxColumn.Items.AddRange(setting.ComboBoxItems.ToArray());
                    }

                    column = comboBoxColumn;
                    break;

                case ColumnType.Button:
                    column = new DataGridViewButtonColumn
                    {
                        Name = setting.Name,
                        HeaderText = setting.Title,
                        Width = setting.Width,
                        DataPropertyName = setting.Name,
                        Text = "Click",
                        UseColumnTextForButtonValue = true,
                        ReadOnly = false
                    };
                    break;

                case ColumnType.CheckBox:
                    column = new DataGridViewCheckBoxColumn
                    {
                        Name = setting.Name,
                        HeaderText = setting.Title,
                        Width = setting.Width,
                        DataPropertyName = setting.Name,
                        ReadOnly = false
                    };
                    break;

                default:
                    column = new DataGridViewTextBoxColumn
                    {
                        Name = setting.Name,
                        HeaderText = setting.Title,
                        Width = setting.Width,
                        DataPropertyName = setting.Name,
                        ReadOnly = true
                    };
                    break;
            }

            if (column != null)
            {
                // Align content
                switch (setting.ContentAlign)
                {
                    case ContentAlign.Left:
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case ContentAlign.Center:
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case ContentAlign.Right:
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }

                grid.Columns.Add(column);
            }
        }

        grid.AutoGenerateColumns = false; // 자동 생성된 컬럼 비활성화
    }

    /// <summary>
    /// List<T>를 받아서 그리드에 데이터를 바인딩하는 확장 메서드
    /// </summary>
    /// <typeparam name="T">바인딩할 데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>
    public static void SetBindData<T>(this DataGridView grid, List<T> data) where T : class
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
    /// 그리드의 기존 해더를 사용하여 데이터를 바인딩하는 메서드
    /// </summary>
    /// <typeparam name="T">데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>

    public static void SetBindDataToHeaders<T>(this DataGridView grid, List<T> data, int rowCount = 0) where T : class
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        if (data == null)
        {
            MessageBox.Show("데이터가 null입니다.");
            data = new List<T>();
        }

        var columnSettings = grid.Tag as List<DataGridViewColumnSetting>;
        if (columnSettings == null)
        {
            MessageBox.Show("컬럼 설정이 null입니다.");
            return;
        }

        // 데이터를 바인딩
        BindingList<T> bindingList = new BindingList<T>(data);
        BindingSource bindingSource = new BindingSource
        {
            DataSource = bindingList
        };

        grid.DataSource = bindingSource;

        // 빈 행 추가
        if (rowCount > data.Count)
        {
            for (int i = data.Count; i < rowCount; i++)
            {
                bindingList.Add(Activator.CreateInstance<T>());
            }
        }

        // 기본 설정
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.EditMode = DataGridViewEditMode.EditOnEnter;

        grid.Dock = DockStyle.Fill;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        // 컬럼 타입에 따라 ReadOnly 설정
        foreach (DataGridViewColumn column in grid.Columns)
        {
            var setting = columnSettings.FirstOrDefault(s => s.Name == column.Name);
            if (setting != null && ( setting.ColumnType == ColumnType.ComboBox ||
                                    setting.ColumnType == ColumnType.Button || setting.ColumnType == ColumnType.CheckBox))
            {
                column.ReadOnly = false;
            }
            else
            {
                column.ReadOnly = true;
            }
        }
    }

    /// <summary>
    /// 특정 값을 검색하고 해당 값을 포함하는 셀을 강조 표시하는 메서드
    /// </summary>
    public static void SearchAndHighlight(this DataGridView grid, string searchText, Color highlightColor)
    {
        if (grid == null || string.IsNullOrEmpty(searchText))
        {
            MessageBox.Show("그리드가 null이거나 검색어가 유효하지 않습니다.");
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && cell.Value.ToString().Contains(searchText))
                {
                    cell.Style.BackColor = highlightColor;
                }
                else
                {
                    cell.Style.BackColor = Color.White;
                }
            }
        }
    }

    /// <summary>
    /// 특정 조건에 따라 행의 색상을 설정하는 메서드
    /// </summary>
    public static void SetRowColor(this DataGridView grid, Func<DataGridViewRow, bool> condition, Color color)
    {
        if (grid == null || condition == null)
        {
            MessageBox.Show("그리드가 null이거나 조건이 유효하지 않습니다.");
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            if (condition(row))
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = color;
                }
            }
        }
    }

    /// <summary>
    /// 특정 조건에 따라 셀의 색상을 설정하는 메서드
    /// </summary>
    public static void SetCellColor(this DataGridView grid, Func<DataGridViewCell, bool> condition, Color color)
    {
        if (grid == null || condition == null)
        {
            MessageBox.Show("그리드가 null이거나 조건이 유효하지 않습니다.");
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (condition(cell))
                {
                    cell.Style.BackColor = color;
                }
            }
        }
    }

    /// <summary>
    /// 특정 조건에 따라 행의 글꼴을 설정하는 메서드
    /// </summary>
    public static void SetRowFont(this DataGridView grid, Func<DataGridViewRow, bool> condition, Font font)
    {
        if (grid == null || condition == null || font == null)
        {
            MessageBox.Show("그리드가 null이거나 조건 또는 글꼴이 유효하지 않습니다.");
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            if (condition(row))
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.Font = font;
                }
            }
        }
    }

    /// <summary>
    /// 특정 조건에 따라 셀의 글꼴을 설정하는 메서드
    /// </summary>
    public static void SetCellFont(this DataGridView grid, Func<DataGridViewCell, bool> condition, Font font)
    {
        if (grid == null || condition == null || font == null)
        {
            MessageBox.Show("그리드가 null이거나 조건 또는 글꼴이 유효하지 않습니다.");
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (condition(cell))
                {
                    cell.Style.Font = font;
                }
            }
        }
    }

    /// <summary>
    /// 컬럼 너비를 데이터에 맞게 자동 조절하는 메서드
    /// </summary>
    public static void AutoSizeColumns(this DataGridView grid)
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        foreach (DataGridViewColumn column in grid.Columns)
        {
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }

    /// <summary>
    /// 행 높이를 데이터에 맞게 자동 조절하는 메서드
    /// </summary>
    public static void AutoSizeRows(this DataGridView grid)
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    }

    /// <summary>
    /// 체크박스 컬럼을 추가하는 메서드
    /// </summary>
   
        public static void AddCheckBoxColumn(this DataGridView grid, string columnName, string headerText, int position = 0)
        {
            if (!grid.Columns.Contains(columnName))
            {
                var checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = columnName,
                    HeaderText = headerText,
                    Width = 30,
                    MinimumWidth = 30,
                    ReadOnly = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                if (position < 0 || position > grid.Columns.Count)
                {
                    position = 0;
                }

                grid.Columns.Insert(position, checkBoxColumn);
            }
        }

        public static void AddComboBoxColumn(this DataGridView grid, string columnName, string headerText, List<string> items, int position = 0)
        {
            if (!grid.Columns.Contains(columnName))
            {
                var comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    Name = columnName,
                    HeaderText = headerText,
                    DataSource = items,
                    Width = 100,
                    MinimumWidth = 100,
                    ReadOnly = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                if (position < 0 || position > grid.Columns.Count)
                {
                    position = 0;
                }

                grid.Columns.Insert(position, comboBoxColumn);
            }
        }

        public static void AddTextBoxColumn(this DataGridView grid, string columnName, string headerText, int position = 0)
        {
            if (!grid.Columns.Contains(columnName))
            {
                var textBoxColumn = new DataGridViewTextBoxColumn
                {
                    Name = columnName,
                    HeaderText = headerText,
                    Width = 100,
                    MinimumWidth = 100,
                    ReadOnly = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                if (position < 0 || position > grid.Columns.Count)
                {
                    position = 0;
                }

                grid.Columns.Insert(position, textBoxColumn);
            }
        }

        public static void AddButtonColumn(this DataGridView grid, string columnName, string headerText, string buttonText, int position = 0)
        {
            if (!grid.Columns.Contains(columnName))
            {
                var buttonColumn = new DataGridViewButtonColumn
                {
                    Name = columnName,
                    HeaderText = headerText,
                    Text = buttonText,
                    UseColumnTextForButtonValue = true,
                    Width = 100,
                    MinimumWidth = 100,
                    ReadOnly = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                if (position < 0 || position > grid.Columns.Count)
                {
                    position = 0;
                }

                grid.Columns.Insert(position, buttonColumn);
            }
        }

        public static void AddImageColumn(this DataGridView grid, string columnName, string headerText, int position = 0)
        {
            if (!grid.Columns.Contains(columnName))
            {
                var imageColumn = new DataGridViewImageColumn
                {
                    Name = columnName,
                    HeaderText = headerText,
                    Width = 100,
                    MinimumWidth = 100,
                    ReadOnly = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };

                if (position < 0 || position > grid.Columns.Count)
                {
                    position = 0;
                }

                grid.Columns.Insert(position, imageColumn);
            }
        }

    /// <summary>
    /// 체크박스 컬럼의 선택 상태를 토글하는 메서드
    /// </summary>
    public static void ToggleCheckBoxColumn(this DataGridView grid, string columnName)
    {
        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            DataGridViewCheckBoxCell checkBoxCell = row.Cells[columnName] as DataGridViewCheckBoxCell;
            if (checkBoxCell != null)
            {
                checkBoxCell.Value = !(bool)(checkBoxCell.Value ?? false);
            }
        }
    }

    /// <summary>
    /// 체크박스 컬럼에서 선택된 행을 가져오는 메서드
    /// </summary>
    public static List<T> GetCheckedRows<T>(this DataGridView grid, string columnName) where T : class
    {
        List<T> checkedRows = new List<T>();

        if (grid == null)
        {
            MessageBox.Show("그리드가 null입니다.");
            return checkedRows;
        }

        foreach (DataGridViewRow row in grid.Rows)
        {
            DataGridViewCheckBoxCell checkBoxCell = row.Cells[columnName] as DataGridViewCheckBoxCell;
            if (checkBoxCell != null && (bool)(checkBoxCell.Value ?? false))
            {
                checkedRows.Add(row.DataBoundItem as T);
            }
        }

        return checkedRows;
    }
}

/// <summary>
/// 컬럼 설정을 위한 클래스
/// </summary>
public enum ColumnType
{
    TextBox,
    ComboBox,
    Button,
    CheckBox
}

public class DataGridViewColumnSetting
{
    public string Name { get; set; }
    public string Title { get; set; }
    public int Width { get; set; }
    public ContentAlign ContentAlign { get; set; }
    public ColumnType ColumnType { get; set; } = ColumnType.TextBox;
    public List<string> ComboBoxItems { get; set; } // ComboBox의 경우 아이템 목록
}


/// <summary>
/// 내용 정렬을 위한 열거형
/// </summary>
public enum ContentAlign
{
    Left,
    Center,
    Right
}
