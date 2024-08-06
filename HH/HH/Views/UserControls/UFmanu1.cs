using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HH.Views
{
    public partial class UFmanu1 : UserControl
    {
        public UFmanu1()
        {
            InitializeComponent();
            InitializeListView();
        }
        private void InitializeListView()
        {
            listView1.View = View.Details;

            // 모델을 기반으로 컬럼 추가
            listView1.AddColumnsFromModel<MyModel>();

            // 컬럼 크기 설정
            listView1.SetColumnWidths(
            new[] { 100, 100, 100 },
            new[] { HorizontalAlignment.Center, HorizontalAlignment.Center, HorizontalAlignment.Right }
        );




            // 항목 업데이트
            //  

            // 항목 삭제
            //  

            // 모든 항목 삭제
            //

            // 항목 찾기


            //// 모든 항목 찾기

        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            listView1.AddItem(new[] { "Value1", "Value2", "Value3" });

            // 항목 대량 추가
            var items = new List<string[]>
        {
            new[] { "Value4", "Value5", "Value6" },
            new[] { "Value7", "Value8", "Value9" }
        };
            listView1.AddItems(items);

            // 모델을 기반으로 항목 추가
            var model1 = new MyModel { Column1 = "Value11", Column2 = "Value12", Column3 = "Value13" };
            listView1.AddItem2(model1);

            // 모델 리스트를 기반으로 항목 대량 추가
            var models = new List<MyModel>
        {
            new MyModel { Column1 = "Value14", Column2 = "Value15", Column3 = "Value16" },
            new MyModel { Column1 = "Value17", Column2 = "Value18", Column3 = "Value19" }
        };
            listView1.AddItems2(models);

            Random random = new Random();
            models = new List<MyModel>();


            for (int i = 0; i < 40; i++)
            {
                models.Add(new MyModel
                {
                    Column1 = $"Value{random.Next(1, 100)}",
                    Column2 = $"Value{random.Next(1, 100)}",
                    Column3 = $"Value{random.Next(1, 100)}"
                });
            }
            listView1.AddItems2(models);

        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            listView1.UpdateItem(0, new[] { "1", "2", "3" });
            listView1.UpdateItem(1, new[] { "1", "2", "3" });
            listView1.UpdateItem(2, new[] { "1", "2", "3" });

        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            listView1.RemoveItem(0);
        }
       


        

    private void materialButton1_Click(object sender, EventArgs e)
        {
            listView1.ClearAllItems();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            var foundItem = listView1.FindItemByText("1");

            var indexdata = listView1.GetItemData(foundItem.Index);
            Debug.WriteLine(indexdata.ToString());
            foreach (var item2 in indexdata)
            {
                Debug.WriteLine(item2.ToString());
            }
       
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            var foundItems = listView1.FindAllItemsByText("1");
        
            foreach (var item in foundItems)
            {
                var indexdata = listView1.GetItemData(item.Index);
                foreach (var item2 in indexdata)
                {
                    Debug.WriteLine(item2.ToString());
                }

            }

          
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {

        }
    }

    public class MyModel
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }




}

