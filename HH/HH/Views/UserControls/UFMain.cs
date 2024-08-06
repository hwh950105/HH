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

namespace HH.Views
{
    public partial class UFMain : UserControl
    {
        public FmMainController FmMainCont { get; set; }

        private FmMain fmMain;

        public UFMain(FmMain main)
        {
            fmMain = main;
            InitializeComponent();
            poisonDataGridView1.GridHwhSetting();

            var headers = new List<string> { "index", "title", "content" };
            poisonDataGridView1.SetHeaders(headers);


        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            var people = new List<TB_TEST_MODEL>();

            using (SQLiteDbHelper DB = new SQLiteDbHelper())
            {
                people = DB.ExecuteList<TB_TEST_MODEL>("select * from TB_TEST");
            }

            poisonDataGridView1.SetBindDataToHeaders(people);
        }
        private void materialButton7_Click(object sender, EventArgs e)
        {
            poisonDataGridView1.EnableEditMode();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            poisonDataGridView1.EnableDeleteMode();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {


            var modifiedData = poisonDataGridView1.GeteditRows<TB_TEST_MODEL>();

            using (SQLiteDbHelper DB = new SQLiteDbHelper())
            {
                foreach (var model in modifiedData)
                {
                    Debug.WriteLine($"OriginalRowIndex: {model.OriginalRowIndex}");

                    var originalData = model.OriginalData;
                    var modifiedDataRow = model.ModifiedData;

                    string query = string.Empty;
                    if (model.Situation == SituationType.New)
                    {
                        query = $"INSERT INTO TB_TEST (title, content, reg_id, Creati_time) " +
                                $"VALUES('{modifiedDataRow.title}', '{modifiedDataRow.content}', '{fmMain.LoginID}', '{DateTime.Now.ToString("yyyyMMddHHmmss")}')";
                        DB.ExecuteNonQuery(query);
                    }
                    else if (model.Situation == SituationType.Updated)
                    {
                        query = $"UPDATE TB_TEST SET title = '{modifiedDataRow.title}', content = '{modifiedDataRow.content}' WHERE [index] = {originalData.index}";
                        DB.ExecuteNonQuery(query);
                    }
                    else if (model.Situation == SituationType.Deleted)
                    {
                        query = $"DELETE FROM TB_TEST WHERE [index] = {originalData.index}";
                        DB.ExecuteNonQuery(query);
                    }

                    if (originalData != null)
                    {
                        Debug.WriteLine("Original Data:");
                        Debug.WriteLine($"Id: {originalData.index}, Name: {originalData.title}, Age: {originalData.content}");
                    }

                    if (modifiedDataRow != null)
                    {
                        Debug.WriteLine("Modified Data:");
                        Debug.WriteLine($"Id: {modifiedDataRow.index}, Name: {modifiedDataRow.title}, Age: {modifiedDataRow.content}");
                    }
                }

                poisonDataGridView1.SetBindData(DB.ExecuteList<TB_TEST_MODEL>("SELECT * FROM TB_TEST"));
            }
        }




        private void poisonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

          //  LoginModel loginModel = poisonDataGridView1.GetSelectedRowData<LoginModel>();
            //Debug.WriteLine($"Id: {loginModel.index}, Name: {loginModel.id}, Age: {loginModel.password}");

        }

        int cellClickindex = 0;
        private void poisonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TB_TEST_MODEL loginModel = poisonDataGridView1.GetSelectedRowData<TB_TEST_MODEL>();

            if (loginModel != null) {
                metroTextBox1.Text = loginModel.title;
                richTextBox1.Text = loginModel.content;
                cellClickindex = loginModel.index;
                Debug.WriteLine($"Id: {loginModel.index}, Name: {loginModel.title}, Age: {loginModel.content}");
            }
        
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            var deletedData = poisonDataGridView1.GetDeletedRows<TB_TEST_MODEL>();
            foreach (var item in deletedData)
            {
                Debug.WriteLine($"Deleted Data: Id: {item.index}, Name: {item.title}, Age: {item.content}");
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
 
        }

        public class TB_TEST_MODEL
        {
            public int index { get; set; }
            public string title { get; set; }
            public string content { get; set; }
   
        }



        private void materialButton6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteDbHelper DB = new SQLiteDbHelper())
                {

                    string Qruey = $"update TB_TEST set title = '{metroTextBox1.Text}' , content = '{richTextBox1.Text}'  where [index] = {cellClickindex}";
                    DB.ExecuteNonQuery(Qruey);
                    poisonDataGridView1.SetBindData(DB.ExecuteList<TB_TEST_MODEL>("select * from TB_TEST"));
                }
            }
            catch (Exception ex) 
            { 
                   Debug.WriteLine (ex);
            }
       
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}