using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public static class DataGridViewExtensions3
{
    /// <summary>
    /// 그리드의 특정 컬럼을 정렬하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="columnName">정렬할 컬럼 이름</param>
    /// <param name="ascending">오름차순 정렬 여부</param>
    public static void SortColumn(this DataGridView grid, string columnName, bool ascending = true)
    {
        if (grid.Columns.Contains(columnName))
        {
            grid.Sort(grid.Columns[columnName], ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }
    }

    /// <summary>
    /// 그리드의 특정 컬럼을 필터링하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="columnName">필터링할 컬럼 이름</param>
    /// <param name="filterText">필터링할 텍스트</param>
    public static void FilterColumn(this DataGridView grid, string columnName, string filterText)
    {
        if (grid.DataSource is DataTable dataTable && grid.Columns.Contains(columnName))
        {
            dataTable.DefaultView.RowFilter = $"{columnName} LIKE '%{filterText}%'";
        }
    }

    /// <summary>
    /// 그리드의 데이터를 CSV 파일로 내보내는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="filePath">저장할 CSV 파일 경로</param>
    public static void ExportToCsv(this DataGridView grid, string filePath)
    {
        var sb = new StringBuilder();

        // 컬럼 헤더
        var headers = grid.Columns.Cast<DataGridViewColumn>().Select(column => column.HeaderText);
        sb.AppendLine(string.Join(",", headers));

        // 행 데이터
        foreach (DataGridViewRow row in grid.Rows)
        {
            var cells = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString() ?? string.Empty);
            sb.AppendLine(string.Join(",", cells));
        }

        File.WriteAllText(filePath, sb.ToString());
    }

    /// <summary>
    /// CSV 파일에서 데이터를 가져와 그리드에 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="filePath">읽어올 CSV 파일 경로</param>
    public static void ImportFromCsv(this DataGridView grid, string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        if (lines.Length == 0) return;

        var headers = lines[0].Split(',');

        var dataTable = new DataTable();
        foreach (var header in headers)
        {
            dataTable.Columns.Add(header);
        }

        foreach (var line in lines.Skip(1))
        {
            var cells = line.Split(',');
            dataTable.Rows.Add(cells);
        }

        grid.DataSource = dataTable;
    }

    /// <summary>
    /// 검색어를 포함하는 셀을 하이라이트하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="searchText">검색어</param>
    /// <param name="highlightColor">하이라이트 색상</param>
    public static void SearchAndHighlight(this DataGridView grid, string searchText, Color highlightColor)
    {
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
    /// 그리드의 특정 컬럼의 색상을 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="columnName">컬럼 이름</param>
    /// <param name="color">설정할 색상</param>
    public static void SetColumnColor(this DataGridView grid, string columnName, Color color)
    {
        if (grid.Columns.Contains(columnName))
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Cells[columnName].Style.BackColor = color;
            }
        }
    }

    /// <summary>
    /// 그리드의 셀 테두리 스타일을 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="borderStyle">설정할 셀 테두리 스타일</param>
    public static void SetGridLines(this DataGridView grid, DataGridViewCellBorderStyle borderStyle)
    {
        grid.CellBorderStyle = borderStyle;
    }

    /// <summary>
    /// 그리드의 행 색상을 교차하여 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="color1">첫 번째 색상</param>
    /// <param name="color2">두 번째 색상</param>
    public static void AlternateRowColors(this DataGridView grid, Color color1, Color color2)
    {
        foreach (DataGridViewRow row in grid.Rows)
        {
            row.DefaultCellStyle.BackColor = row.Index % 2 == 0 ? color1 : color2;
        }
    }

    /// <summary>
    /// 그리드의 컬럼 툴팁을 설정하는 메서드
    /// </summary>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="tooltips">컬럼 이름과 툴팁 텍스트의 딕셔너리</param>
    public static void SetTooltips(this DataGridView grid, Dictionary<string, string> tooltips)
    {
        foreach (var column in grid.Columns.Cast<DataGridViewColumn>())
        {
            if (tooltips.ContainsKey(column.Name))
            {
                column.ToolTipText = tooltips[column.Name];
            }
        }
    }

    /// <summary>
    /// List<T> 데이터를 그리드에 바인딩하는 메서드
    /// </summary>
    /// <typeparam name="T">바인딩할 데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>
    public static void BindData<T>(this DataGridView grid, List<T> data) where T : class
    {
        var bindingList = new BindingList<T>(data);
        var bindingSource = new BindingSource(bindingList, null);
        grid.DataSource = bindingSource;
    }

    /// <summary>
    /// 데이터와 포맷팅 함수를 사용하여 데이터를 그리드에 바인딩하는 메서드
    /// </summary>
    /// <typeparam name="T">바인딩할 데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>
    /// <param name="formatFunc">포맷팅 함수</param>
    public static void BindDataWithFormatting<T>(this DataGridView grid, List<T> data, Func<T, DataGridViewCellStyle> formatFunc) where T : class
    {
        grid.BindData(data);
        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.DataBoundItem is T item)
            {
                row.DefaultCellStyle = formatFunc(item);
            }
        }
    }

    /// <summary>
    /// 데이터와 유효성 검사 함수를 사용하여 데이터를 그리드에 바인딩하는 메서드
    /// </summary>
    /// <typeparam name="T">바인딩할 데이터 타입</typeparam>
    /// <param name="grid">설정할 DataGridView</param>
    /// <param name="data">바인딩할 데이터 리스트</param>
    /// <param name="validateFunc">유효성 검사 함수</param>
    public static void BindDataWithValidation<T>(this DataGridView grid, List<T> data, Func<T, bool> validateFunc) where T : class
    {
        grid.BindData(data);
        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.DataBoundItem is T item)
            {
                row.DefaultCellStyle.BackColor = validateFunc(item) ? Color.White : Color.Red;
            }
        }
    }
}
