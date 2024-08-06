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
    public static void SetBindDataToHeaders<T>(this DataGridView grid, List<T> data) where T : class
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



}


