using HH.Commons;
using HH.Models;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DataGridViewExtensions;
using Button = ReaLTaiizor.Controls.Button;

namespace HH.Views
{
    public partial class UCMain : UserControl
    {
        public FmMainController FmMainCont { get; set; }
        private string iniFilePath = "path_to_ini_file.ini";
        private FmMain fmMain;
        private TextBox searchTextBox; // 검색어 입력 TextBox

        public class TB_TEST_MODEL
        {
            public int index { get; set; }
            public string title { get; set; }
            public string content { get; set; }

            public string ComboBox { get; set; }
        

            private string _checkBoxValue;
            public string checkBoxValue // DB 에서 찐데이터
            {
                get { return _checkBoxValue; }
                set
                {
                    _checkBoxValue = value;
                    CheckBox = _checkBoxValue == "1";
                }
            }

            private bool _checkBox;
            public bool CheckBox
            {
                get { return _checkBox; }
                set
                {
                    _checkBox = value; //DB 에서 온값 0 false
                    _checkBoxValue = _checkBox ? "1" : "0";
                }
            }
        }

        public UCMain(FmMain main)
        {
            fmMain = main;
            InitializeComponent();
            poisonDataGridView1.GridHwhSetting();
            InitializeDataGridView();
            InitializeButtons();
        }

        private void InitializeDataGridView()
        {
            var columnSettings = new List<DataGridViewColumnSetting>
            {
                new DataGridViewColumnSetting { Name = "index", Title = "INDEX", Width = 120, ContentAlign = ContentAlign.Center,ReadOnly = true },
                new DataGridViewColumnSetting { Name = "title", Title = "TITLE", Width = 210, ContentAlign = ContentAlign.Left,ReadOnly = false  },
                new DataGridViewColumnSetting { Name = "content", Title = "CONTENT", Width = 200, ContentAlign = ContentAlign.Right,ReadOnly = true  },
                new DataGridViewColumnSetting { Name = "ComboBox", Title = "COMBOBOX", Width = 150, ContentAlign = ContentAlign.Left, ColumnType = ColumnType.ComboBox, ComboBoxItems = new List<string> { "Option1", "Option2", "Option3" } },
                
                new DataGridViewColumnSetting { Name = "CheckBox", Title = "CHECKBOX", Width = 60, ContentAlign = ContentAlign.Center, ColumnType = ColumnType.CheckBox },
                new DataGridViewColumnSetting { Name = "Button", Title = "BUTTON", Width = 100, ContentAlign = ContentAlign.Center, ColumnType = ColumnType.Button }
            };

            poisonDataGridView1.SetCustomHeaders(columnSettings);

            var people = new List<TB_TEST_MODEL>();

            using (SQLiteDbHelper DB = new SQLiteDbHelper())
            {
                people = DB.ExecuteList<TB_TEST_MODEL>("select * from TB_TEST");

        

            }
      



        poisonDataGridView1.SetBindDataToHeaders(people, 30);
        }
        

        private void InitializeButtons()
        {
            // 검색어 입력 TextBox 추가
            searchTextBox = new TextBox
            {
                Dock = DockStyle.Fill
            };
            tableLayoutPanel2.RowCount++;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel2.Controls.Add(searchTextBox, 0, tableLayoutPanel2.RowCount - 1);

            var buttons = new List<(string Text, EventHandler Handler)>
            {
                ("검색 하이라이트", materialButton1_Click),
                ("셀 폰트 설정", materialButton4_Click),
                ("콤보박스 컬럼 추가", materialButton13_Click),
                ("체크된 행 보기", materialButton5_Click),
                ("체크박스 토글", materialButton6_Click),
                ("셀 색상 설정", materialButton7_Click),
                ("행 색상 설정", materialButton8_Click),
                ("행 폰트 설정", materialButton9_Click),
                ("자동 컬럼 크기", materialButton10_Click),
                ("자동 행 크기", materialButton11_Click),
                ("체크박스 컬럼 추가", materialButton12_Click),
                ("초기화", materialButtonReset_Click),
                ("텍박스 컬럼 추가", materialButton14_Click),
                ("버튼 컬럼 추가", materialButton15_Click),
                ("이미지 컬럼 추가", materialButton16_Click)
            };

            foreach (var (text, handler) in buttons)
            {
                var button = new Button
                {
                    Text = text,
                    Dock = DockStyle.Fill
                };
                button.Click += handler;
                tableLayoutPanel2.RowCount++;
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tableLayoutPanel2.Controls.Add(button, 0, tableLayoutPanel2.RowCount - 1);
            }
        }

        private void PoisonDataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            SaveColumnWidths();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // 검색어를 포함하는 셀을 하이라이트
            poisonDataGridView1.SearchAndHighlight(searchTextBox.Text, Color.LightGreen);
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            // 특정 조건에 맞는 셀의 글꼴을 변경
            poisonDataGridView1.SetCellFont(cell => cell.Value != null && cell.Value.ToString().Contains(searchTextBox.Text), new Font("Arial", 12, FontStyle.Bold));
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // 체크박스 컬럼에서 체크된 행을 가져와서 출력
            var checkedRows = poisonDataGridView1.GetCheckedRows<TB_TEST_MODEL>("CheckBoxColumn");
            foreach (var row in checkedRows)
            {
                Debug.WriteLine($"Checked Row: Id: {row.index}, Name: {row.title}, Age: {row.content}");
            }
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            // 체크박스 컬럼 토글
            poisonDataGridView1.ToggleCheckBoxColumn("CheckBoxColumn");
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            // 특정 조건에 맞는 셀의 색상을 변경
            poisonDataGridView1.SetCellColor(cell => cell.Value != null && cell.Value.ToString().Contains(searchTextBox.Text), Color.Yellow);
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            // 특정 조건에 맞는 행의 색상을 변경
            poisonDataGridView1.SetRowColor(row => row.Cells["title"].Value != null && row.Cells["title"].Value.ToString().Contains(searchTextBox.Text), Color.LightBlue);
        }

        private void materialButton9_Click(object sender, EventArgs e)
        {
            // 특정 조건에 맞는 행의 글꼴을 변경
            poisonDataGridView1.SetRowFont(row => row.Cells["title"].Value != null && row.Cells["title"].Value.ToString().Contains(searchTextBox.Text), new Font("Arial", 12, FontStyle.Italic));
        }

        private void materialButton10_Click(object sender, EventArgs e)
        {
            // 컬럼 자동 크기 조정
            poisonDataGridView1.AutoSizeColumns();
        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            // 행 자동 크기 조정
            poisonDataGridView1.AutoSizeRows();
        }

        private void materialButton12_Click(object sender, EventArgs e)
        {
            // 체크박스 컬럼 추가a
            poisonDataGridView1.ReadOnly = false;
            poisonDataGridView1.AddCheckBoxColumn("CheckBoxColumn", " ", poisonDataGridView1.ColumnCount);
        }

        private void materialButton13_Click(object sender, EventArgs e)
        {
            // 콤보박스 컬럼 추가
            poisonDataGridView1.AddComboBoxColumn("ComboBoxColumn", "Options", new List<string> { "Option1", "Option2", "Option3" }, poisonDataGridView1.ColumnCount);
        }

        private void materialButton14_Click(object sender, EventArgs e)
        {
            // 텍스트박스 컬럼 추가
            poisonDataGridView1.AddTextBoxColumn("TextBoxColumn", "Text", poisonDataGridView1.ColumnCount);
        }

        private void materialButton15_Click(object sender, EventArgs e)
        {
            // 버튼 컬럼 추가
            poisonDataGridView1.AddButtonColumn("ButtonColumn", "Action", "Click Me", poisonDataGridView1.ColumnCount);
        }

        private void materialButton16_Click(object sender, EventArgs e)
        {
            // 이미지 컬럼 추가
            poisonDataGridView1.AddImageColumn("ImageColumn", "Image", poisonDataGridView1.ColumnCount);
        }

        private void materialButtonReset_Click(object sender, EventArgs e)
        {
            ResetDataGridView();
            InitializeDataGridView();
        }

        private void ResetDataGridView()
        {
            poisonDataGridView1.DataSource = null;
            poisonDataGridView1.Rows.Clear();
            poisonDataGridView1.Columns.Clear();
        }

        private void SaveColumnWidths()
        {
            poisonDataGridView1.SaveColumnWidths(iniFilePath, "DataGridViewColumnWidths");
        }

        private void ReadColumnWidths()
        {
            poisonDataGridView1.LoadColumnWidths(iniFilePath, "DataGridViewColumnWidths");
        }
        private void poisonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var row = poisonDataGridView1.Rows[e.RowIndex];

                // 일반 셀 데이터 출력
                if (row.DataBoundItem is TB_TEST_MODEL asd)
                {
                    Debug.WriteLine($"Selected Data: Id: {asd.index}, Title: {asd.title}, Content: {asd.content}");
                }

                // 선택한 행의 모든 셀 데이터 출력
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell is DataGridViewComboBoxCell comboBoxCell)
                    {
                        Debug.WriteLine($"ComboBox Cell [{cell.ColumnIndex}]: {comboBoxCell.Value}");
                    }
                    else if (cell is DataGridViewCheckBoxCell checkBoxCell)
                    {
                        Debug.WriteLine($"CheckBox Cell [{cell.ColumnIndex}]: {checkBoxCell.Value}");
                    }
                    else
                    {
                        Debug.WriteLine($"Cell [{cell.ColumnIndex}]: {cell.Value}");
                    }
                }
            }
        }

        private void poisonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 버튼 컬럼의 인덱스 또는 이름을 확인하여 버튼 클릭을 감지
            if (e.RowIndex >= 0 && poisonDataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // 클릭된 버튼 컬럼의 이름을 가져옴
                var columnName = poisonDataGridView1.Columns[e.ColumnIndex].Name;

                // 예시: 특정 버튼 컬럼이 클릭되었을 때 처리할 작업
                if (columnName == "Button")
                {
                    var rowData = poisonDataGridView1.Rows[e.RowIndex].DataBoundItem as TB_TEST_MODEL;
                    if (rowData != null)
                    {
                        using (SQLiteDbHelper db = new SQLiteDbHelper())
                        {
                            db.ExecuteNonQuery($"update TB_TEST set ComboBox = '{rowData.ComboBox}', CheckBox = '{rowData.checkBoxValue}' where [index] = '{rowData.index}'");
                        }
                    }
                }

            }
        }

    }
}
