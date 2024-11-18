using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HH; 

namespace HH
{
    public partial class UCTreeView : UserControl
    {
        public UCTreeView()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            // MainTreeView1 초기화
            MainTreeView1.Nodes.Clear();

            // 노드 추가
            MainTreeView1.AddNode(null, "Root Node 1");
            MainTreeView1.AddNode(null, "Root Node 2");

            // 서브 노드 추가
            var rootNode1 = MainTreeView1.Nodes[0];
            MainTreeView1.AddNode(rootNode1, "Child Node 1");
            MainTreeView1.AddNode(rootNode1, "Child Node 2");

            // 드래그 앤 드롭 활성화
            MainTreeView1.EnableDragAndDrop();

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MainTreeView1.SelectedNode != null)
            {
                MainTreeView1.RemoveNode(MainTreeView1.SelectedNode);
            }
            else
            {
                MessageBox.Show("Please select a node to delete.");
            }
        }
    }
}
