namespace HH
{
    partial class FmLogin
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmLogin));
            this.panel2 = new System.Windows.Forms.Panel();
            this.hopePictureBox2 = new ReaLTaiizor.Controls.HopePictureBox();
            this.materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            this.dungeonLinkLabel1 = new ReaLTaiizor.Controls.DungeonLinkLabel();
            this.materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            this.SwitchPw = new ReaLTaiizor.Controls.HopeSwitch();
            this.TbPassword = new ReaLTaiizor.Controls.HopeTextBox();
            this.TbEmail = new ReaLTaiizor.Controls.HopeTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hopePictureBox1 = new ReaLTaiizor.Controls.HopePictureBox();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hopePictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hopePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.panel2.Controls.Add(this.hopePictureBox2);
            this.panel2.Controls.Add(this.materialButton1);
            this.panel2.Controls.Add(this.dungeonLinkLabel1);
            this.panel2.Controls.Add(this.materialLabel1);
            this.panel2.Controls.Add(this.SwitchPw);
            this.panel2.Controls.Add(this.TbPassword);
            this.panel2.Controls.Add(this.TbEmail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 417);
            this.panel2.TabIndex = 2;
            // 
            // hopePictureBox2
            // 
            this.hopePictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.hopePictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.hopePictureBox2.Enabled = false;
            this.hopePictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("hopePictureBox2.Image")));
            this.hopePictureBox2.Location = new System.Drawing.Point(61, 12);
            this.hopePictureBox2.Name = "hopePictureBox2";
            this.hopePictureBox2.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.hopePictureBox2.Size = new System.Drawing.Size(144, 116);
            this.hopePictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.hopePictureBox2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.hopePictureBox2.TabIndex = 8;
            this.hopePictureBox2.TabStop = false;
            this.hopePictureBox2.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            this.materialButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.Font = new System.Drawing.Font("굴림", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.materialButton1.Location = new System.Drawing.Point(10, 354);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(260, 36);
            this.materialButton1.TabIndex = 20;
            this.materialButton1.Text = "로그인";
            this.materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = true;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // dungeonLinkLabel1
            // 
            this.dungeonLinkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(72)))), ((int)(((byte)(20)))));
            this.dungeonLinkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dungeonLinkLabel1.AutoSize = true;
            this.dungeonLinkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.dungeonLinkLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dungeonLinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.dungeonLinkLabel1.LinkColor = System.Drawing.Color.Silver;
            this.dungeonLinkLabel1.Location = new System.Drawing.Point(157, 265);
            this.dungeonLinkLabel1.Name = "dungeonLinkLabel1";
            this.dungeonLinkLabel1.Size = new System.Drawing.Size(103, 20);
            this.dungeonLinkLabel1.TabIndex = 18;
            this.dungeonLinkLabel1.TabStop = true;
            this.dungeonLinkLabel1.Text = "비밀번호 찾기";
            this.dungeonLinkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(119)))), ((int)(((byte)(70)))));
            // 
            // materialLabel1
            // 
            this.materialLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            this.materialLabel1.HighEmphasis = true;
            this.materialLabel1.Location = new System.Drawing.Point(58, 269);
            this.materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(74, 17);
            this.materialLabel1.TabIndex = 12;
            this.materialLabel1.Text = "로그인정보 저장";
            this.materialLabel1.UseAccent = true;
            // 
            // SwitchPw
            // 
            this.SwitchPw.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SwitchPw.AutoSize = true;
            this.SwitchPw.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.SwitchPw.BaseOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(141)))), ((int)(((byte)(144)))));
            this.SwitchPw.BaseOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.SwitchPw.Checked = true;
            this.SwitchPw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SwitchPw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SwitchPw.Location = new System.Drawing.Point(12, 267);
            this.SwitchPw.Name = "SwitchPw";
            this.SwitchPw.Size = new System.Drawing.Size(40, 20);
            this.SwitchPw.TabIndex = 10;
            this.SwitchPw.Text = "hopeSwitch1";
            this.SwitchPw.UseVisualStyleBackColor = true;
            // 
            // TbPassword
            // 
            this.TbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TbPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.TbPassword.BaseColor = System.Drawing.Color.Transparent;
            this.TbPassword.BorderColorA = System.Drawing.Color.DodgerBlue;
            this.TbPassword.BorderColorB = System.Drawing.Color.DarkGray;
            this.TbPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TbPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.TbPassword.Hint = "";
            this.TbPassword.Location = new System.Drawing.Point(10, 208);
            this.TbPassword.MaxLength = 128;
            this.TbPassword.Multiline = false;
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.PasswordChar = '\0';
            this.TbPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TbPassword.SelectedText = "";
            this.TbPassword.SelectionLength = 0;
            this.TbPassword.SelectionStart = 0;
            this.TbPassword.Size = new System.Drawing.Size(260, 38);
            this.TbPassword.TabIndex = 8;
            this.TbPassword.TabStop = false;
            this.TbPassword.Text = "Password";
            this.TbPassword.UseSystemPasswordChar = false;
            // 
            // TbEmail
            // 
            this.TbEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TbEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.TbEmail.BaseColor = System.Drawing.Color.Transparent;
            this.TbEmail.BorderColorA = System.Drawing.Color.DodgerBlue;
            this.TbEmail.BorderColorB = System.Drawing.Color.DarkGray;
            this.TbEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TbEmail.ForeColor = System.Drawing.Color.Gainsboro;
            this.TbEmail.Hint = "";
            this.TbEmail.Location = new System.Drawing.Point(10, 154);
            this.TbEmail.MaxLength = 128;
            this.TbEmail.Multiline = false;
            this.TbEmail.Name = "TbEmail";
            this.TbEmail.PasswordChar = '\0';
            this.TbEmail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TbEmail.SelectedText = "";
            this.TbEmail.SelectionLength = 0;
            this.TbEmail.SelectionStart = 0;
            this.TbEmail.Size = new System.Drawing.Size(260, 38);
            this.TbEmail.TabIndex = 7;
            this.TbEmail.TabStop = false;
            this.TbEmail.Text = "Email";
            this.TbEmail.UseSystemPasswordChar = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.hopePictureBox1);
            this.panel1.Controls.Add(this.nightControlBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(280, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 417);
            this.panel1.TabIndex = 3;
            // 
            // hopePictureBox1
            // 
            this.hopePictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.hopePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.hopePictureBox1.Enabled = false;
            this.hopePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("hopePictureBox1.Image")));
            this.hopePictureBox1.Location = new System.Drawing.Point(39, 47);
            this.hopePictureBox1.Name = "hopePictureBox1";
            this.hopePictureBox1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.hopePictureBox1.Size = new System.Drawing.Size(307, 328);
            this.hopePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.hopePictureBox1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.hopePictureBox1.TabIndex = 8;
            this.hopePictureBox1.TabStop = false;
            this.hopePictureBox1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = false;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(244, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 7;
            // 
            // FmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 417);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(126, 50);
            this.Name = "FmLogin";
            this.ShowIcon = false;
            this.Text = "themeForm1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hopePictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hopePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private ReaLTaiizor.Controls.DungeonLinkLabel dungeonLinkLabel1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.HopeSwitch SwitchPw;
        private ReaLTaiizor.Controls.HopeTextBox TbPassword;
        private ReaLTaiizor.Controls.HopeTextBox TbEmail;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.HopePictureBox hopePictureBox1;
        private ReaLTaiizor.Controls.HopePictureBox hopePictureBox2;
    }
}

