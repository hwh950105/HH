using HH.Models;
using HH.Views.UserControls;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace HH.Views
{
    public partial class FmMain : Form
    {
        private Dictionary<Type, UserControl> userControls;
    
        public string LoginID = string.Empty;   

        public FmMain(string loginID)
        {
            InitializeComponent();
            InitializeUserControls();
            LoginID = loginID;
        }

        private void InitializeUserControls()
        {
            userControls = new Dictionary<Type, UserControl>();

            // UFMain 초기화 및 설정
            UCMain ufMain = new UCMain(this);
            ufMain.Dock = DockStyle.Fill;
            panel2.Controls.Add(ufMain);
            userControls[typeof(UCMain)] = ufMain;

            // 초기에는 UFMain만 표시
            ufMain.Visible = true;
        }

        private void LoadContent(Type userControlType)
        {
            if (!userControls.TryGetValue(userControlType, out UserControl userControl))
            {
                // 해당 컨트롤이 없으면 새로 생성하여 추가
                ConstructorInfo constructor = userControlType.GetConstructor(new Type[] { typeof(FmMain) });
                if (constructor != null)
                {
                    userControl = (UserControl)constructor.Invoke(new object[] { this });
                }
                else
                {
                    userControl = (UserControl)Activator.CreateInstance(userControlType);
                }

                userControl.Dock = DockStyle.Fill;
                panel2.Controls.Add(userControl);
                userControls[userControlType] = userControl;
            }

            // 모든 컨트롤을 숨기고 선택된 컨트롤만 표시
            foreach (Control control in panel2.Controls)
            {
                control.Visible = false;
            }

            userControl.Visible = true;
        }



        private void Btmainview_Click(object sender, EventArgs e)
        {
            LoadContent(typeof(UCMain));
        }


        private void Btview1_Click(object sender, EventArgs e)
        {
            LoadContent(typeof(UCmanu1));

        }

        private void Btview2_Click(object sender, EventArgs e)
        {
            LoadContent(typeof(UCTcpClient));
        }

        private void Btview3_Click(object sender, EventArgs e)
        {
            LoadContent(typeof(UCJsonConfig));
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            LoadContent(typeof(UCiniConfig));
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            LoadContent(typeof(UCTreeView));
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {

        }

        private void materialButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
