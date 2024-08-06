using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HH.Views.UserControls
{
    public partial class UFJsonConfig : UserControl
    {

        private const string ConfigFilePath = "jsonconfig.json";
        private ConfigModel configModel;


        public UFJsonConfig()
        {
            InitializeComponent(); 
            LoadConfig();
        }


        private void LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                // JSON 파일이 없으면 기본 모델로 파일 생성
                configModel = new ConfigModel();

                // 기본 항목을 추가
                configModel.ConfigItems.Add(new ConfigItem());

                SaveConfig();  // 기본 모델을 JSON 파일로 저장
            }
            else
            {
                // JSON 파일이 있으면 파일을 읽어서 모델에 저장
                var json = File.ReadAllText(ConfigFilePath);
                configModel = JsonConvert.DeserializeObject<ConfigModel>(json);
            }
        }


        private void SaveConfig()
        {
            var json = JsonConvert.SerializeObject(configModel, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, json);
        }

        private void DisplayConfig()
        {
            
            var json = JsonConvert.SerializeObject(configModel, Formatting.Indented);
            richTextBox1.Text = json;
        }




        private void materialButton1_Click(object sender, EventArgs e)
        {
            var json = richTextBox1.Text;
            configModel = JsonConvert.DeserializeObject<ConfigModel>(json);
            SaveConfig();
        }



        public class ConfigItem
        {
            public string Name { get; set; }
            public string Value { get; set; }

            // 기본 생성자
            public ConfigItem()
            {
                Name = "DefaultName";
                Value = "DefaultValue";
            }
        }

        public class ConfigModel
        {
            public List<ConfigItem> ConfigItems { get; set; } = new List<ConfigItem>();
        }


        private void materialButton2_Click(object sender, EventArgs e)
        {
            DisplayConfig();
        }
    }
}
