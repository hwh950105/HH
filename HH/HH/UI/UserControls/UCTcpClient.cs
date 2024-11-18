using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HH.Views
{
    public partial class UCTcpClient : UserControl
    {
        public UCTcpClient()
        {
            InitializeComponent();
            InitializeListView();
        }

        private void InitializeListView()
        {
            // 컬럼 추가
            var columns = new List<ColumnHeaderModel>
            {
                new ColumnHeaderModel("시간", 100),
                new ColumnHeaderModel("상태", 100),
                new ColumnHeaderModel("메시지", 200)
            };

            foreach (var column in columns)
            {
                materialListView1.Columns.Add(column.Name, column.Width);
            }

            // 깜박임 최소화
            materialListView1.DoubleBuffered(true);
        }

        // 리스트뷰에 데이터를 추가하는 메서드
        public void AddData(ListViewItemModel model)
        {
            if (materialListView1.InvokeRequired)
            {
                materialListView1.Invoke(new Action<ListViewItemModel>(AddData), model);
            }
            else
            {
                // 리스트뷰에 데이터 추가
                ListViewItem item = new ListViewItem(model.Time.ToString("HH:mm:ss"));
                item.SubItems.Add(model.Status);
                item.SubItems.Add(model.Message);

                materialListView1.Items.Add(item);

                // 항목 수가 500개를 넘으면 오래된 항목 제거
                if (materialListView1.Items.Count > 500)
                {
                    materialListView1.Items.RemoveAt(0);
                }

                // 리스트뷰 다시 그리기
                materialListView1.EnsureVisible(materialListView1.Items.Count - 1);
                materialListView1.Invalidate();
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            List<ListViewItemModel> Data = new List<ListViewItemModel>
            {
                new ListViewItemModel { Time = DateTime.Now, Status = "XX1", Message = "asd5" },
                new ListViewItemModel { Time = DateTime.Now, Status = "XX2", Message = "asd4" },
                new ListViewItemModel { Time = DateTime.Now, Status = "XX3", Message = "asd3" },
                new ListViewItemModel { Time = DateTime.Now, Status = "XX3", Message = "asd2" }
            };

            foreach (var item in Data)
            {
                AddData(item);
            }

            Console.WriteLine("Data added to ListView");
        }
    }

    // 컬럼 헤더 모델 클래스
    public class ColumnHeaderModel
    {
        public string Name { get; set; }
        public int Width { get; set; }

        public ColumnHeaderModel(string name, int width)
        {
            Name = name;
            Width = width;
        }
    }

    // ListViewItemModel 클래스는 이전에 정의한 대로 사용
    public class ListViewItemModel
    {
        public DateTime Time { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public ListViewItemModel() { }
        public ListViewItemModel(DateTime time, string status, string message)
        {
            Time = time;
            Status = status;
            Message = message;
        }
    }

    // ListView의 DoubleBuffered 속성을 설정하는 확장 메서드
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (doubleBufferPropertyInfo != null)
            {
                doubleBufferPropertyInfo.SetValue(control, enable, null);
            }
        }
    }
}
