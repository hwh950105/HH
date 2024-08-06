using HH.Commons;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HH.Views.UserControls
{
    public partial class UFiniConfig : UserControl
    {
        // 상대 경로를 절대 경로로 변환하여 사용
        private string ConfigIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        private IniFile iniFile;
        private NotifyIcon notifyIcon;

        public UFiniConfig()
        {
            InitializeComponent();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information; // 예제 아이콘 사용
            notifyIcon.Visible = true;

            // 버튼을 추가하여 알림을 표시
      

            iniFile = new IniFile(ConfigIniFilePath);
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigIniFilePath))
            {
                iniFile.Write("LoginSettings", "ID", "DefaultName");
                iniFile.Write("Settings", "Password", "DefaultValue");
                iniFile.Write("Settings1", "Name", "DefaultValue");
                iniFile.Write("Settings1", "Value", "DefaultValue");
                iniFile.Write("Settings2", "Name", "DefaultValue");
                iniFile.Write("Settings2", "Value", "DefaultValue");
                iniFile.Write("Settings3", "Name", "DefaultValue");
                iniFile.Write("Settings3", "Value", "DefaultValue");
            }

            string name = iniFile.Read("Settings", "Name");
            string value = iniFile.Read("Settings", "Value");

            Debug.WriteLine(name);
            Debug.WriteLine(value);
        }

        private void SaveConfig()
        {
            iniFile.Write("Settings", "Name", "수정1");
            iniFile.Write("Settings", "Value", "수정2");
        }

        private void airButton1_Click(object sender, EventArgs e)
        {
            SaveConfig();

          
            string name = iniFile.Read("Settings2", "Name");
            string value = iniFile.Read("Settings2", "Value");


            Debug.WriteLine(name);
            Debug.WriteLine(value);


        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Error; // 예제 아이콘 사용
            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = "알림 제목";
            notifyIcon.BalloonTipText = "여기에 알림 내용이 표시됩니다.";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;

           
            notifyIcon.ShowBalloonTip(3000); // 3초 동안 표시
        }
    }
}
