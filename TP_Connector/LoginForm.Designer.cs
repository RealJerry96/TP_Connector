namespace TP_Connector
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ServerIP = new DevExpress.XtraEditors.TextEdit();
            this.ServerPort = new DevExpress.XtraEditors.TextEdit();
            this.UserID = new DevExpress.XtraEditors.TextEdit();
            this.UserPwd = new DevExpress.XtraEditors.TextEdit();
            this.LoginBtn = new DevExpress.XtraEditors.SimpleButton();
            this.CancelBtn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ServerIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserPwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(40, 35);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "서버";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(40, 65);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 15);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "포트";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(40, 100);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 15);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "아이디";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(40, 132);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 15);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "암호";
            // 
            // ServerIP
            // 
            this.ServerIP.Location = new System.Drawing.Point(95, 31);
            this.ServerIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Properties.Appearance.Options.UseTextOptions = true;
            this.ServerIP.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ServerIP.Properties.AutoHeight = false;
            this.ServerIP.Size = new System.Drawing.Size(180, 24);
            this.ServerIP.TabIndex = 4;
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(95, 61);
            this.ServerPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Properties.Appearance.Options.UseTextOptions = true;
            this.ServerPort.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ServerPort.Properties.AutoHeight = false;
            this.ServerPort.Size = new System.Drawing.Size(180, 24);
            this.ServerPort.TabIndex = 5;
            // 
            // UserID
            // 
            this.UserID.Location = new System.Drawing.Point(95, 96);
            this.UserID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UserID.Name = "UserID";
            this.UserID.Properties.AutoHeight = false;
            this.UserID.Size = new System.Drawing.Size(180, 24);
            this.UserID.TabIndex = 6;
            // 
            // UserPwd
            // 
            this.UserPwd.Location = new System.Drawing.Point(95, 128);
            this.UserPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UserPwd.Name = "UserPwd";
            this.UserPwd.Properties.AutoHeight = false;
            this.UserPwd.Properties.PasswordChar = '*';
            this.UserPwd.Size = new System.Drawing.Size(180, 24);
            this.UserPwd.TabIndex = 7;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LoginBtn.Appearance.Options.UseFont = true;
            this.LoginBtn.Location = new System.Drawing.Point(95, 174);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(85, 30);
            this.LoginBtn.TabIndex = 8;
            this.LoginBtn.Text = "로그인";
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.CancelBtn.Appearance.Options.UseFont = true;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(190, 174);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(85, 30);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "취소";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.LoginBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(306, 219);
            this.ControlBox = false;
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.UserPwd);
            this.Controls.Add(this.UserID);
            this.Controls.Add(this.ServerPort);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("LoginForm.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "로그인";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServerIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserPwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;

        // 기존 코드와의 호환성을 위해 변수명은 대소문자 그대로 유지했습니다.
        private DevExpress.XtraEditors.TextEdit ServerIP;
        private DevExpress.XtraEditors.TextEdit ServerPort;
        private DevExpress.XtraEditors.TextEdit UserID;
        private DevExpress.XtraEditors.TextEdit UserPwd;

        private DevExpress.XtraEditors.SimpleButton LoginBtn;
        private DevExpress.XtraEditors.SimpleButton CancelBtn;
    }
}