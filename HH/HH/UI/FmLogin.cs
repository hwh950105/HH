using HH.Commons;
using HH.Models;
using HH.Views;
using ReaLTaiizor.Controls;
using System;
using System.IO;
using System.Windows.Forms;

namespace HH
{
    public partial class FmLogin : Form
    {
        private bool isPasswordFieldEmpty = true;
        private bool isEmailFieldEmpty = true;
        private string configIniFilePath;
        private IniFile iniFile;

        public FmLogin()
        {
            InitializeComponent();
            InitializeConfiguration();
            InitializeEvents();
        }

        private void InitializeConfiguration()
        {
            configIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HHconfig.ini");
            iniFile = new IniFile(configIniFilePath);

            if (!File.Exists(configIniFilePath))
            {
                iniFile.Write("LoginSettings", "ID", "hwh");
                iniFile.Write("LoginSettings", "Password", "a12a12");
                iniFile.Write("LoginSettings", "PasswordChecked", "1");
            }

            LoadLoginSettings();
        }

        private void LoadLoginSettings()
        {
            string userId = iniFile.Read("LoginSettings", "ID");
            string userPassword = iniFile.Read("LoginSettings", "Password");
            string passwordChecked = iniFile.Read("LoginSettings", "PasswordChecked");

            bool isPasswordChecked = passwordChecked == "1";

            if (isPasswordChecked)
            {
                TbEmail.Text = userId;
                TbPassword.Text = userPassword;
                SwitchPw.Checked = isPasswordChecked;
                TbPassword.UseSystemPasswordChar = true;
            }
        }

        private void InitializeEvents()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            TbPassword.TextChanged += OnPasswordTextChanged;
            TbPassword.Enter += OnPasswordTextEnter;
            TbEmail.TextChanged += OnEmailTextChange;

        }

        private void OnEmailTextChange(object sender, EventArgs e)
        {
            if (isEmailFieldEmpty)
            {
                TbEmail.Text = string.Empty;
                isEmailFieldEmpty = false;
            }
        }

        private void OnPasswordTextChanged(object sender, EventArgs e)
        {
            HandlePasswordFieldState();
        }

        private void OnPasswordTextEnter(object sender, EventArgs e)
        {
            HandlePasswordFieldState();
        }

        private void HandlePasswordFieldState()
        {
            if (isPasswordFieldEmpty)
            {
                TbPassword.Text = string.Empty;
                isPasswordFieldEmpty = false;
            }

            TbPassword.UseSystemPasswordChar = true;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            var loginModel = new LoginModel
            {
                id = TbEmail.Text,
                password = SecurityHelper.ComputeMD5Hash(TbPassword.Text.Trim()),
            };

            if (DbCall.GetLogin(loginModel))
            {
                SaveLoginSettings();
                OpenMainForm();
            }
        }

        private void SaveLoginSettings()
        {
            iniFile.Write("LoginSettings", "ID", TbEmail.Text);
            iniFile.Write("LoginSettings", "Password", TbPassword.Text);
            iniFile.Write("LoginSettings", "PasswordChecked", SwitchPw.Checked ? "1" : "0");
        }

        private void OpenMainForm()
        {
            FmMain mainForm = new FmMain(TbEmail.Text);
            mainForm.Show();
            this.Hide();
        }
    }
}
