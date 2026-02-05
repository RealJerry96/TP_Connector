namespace TP_Connector
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelServer = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.labelPwd = new System.Windows.Forms.Label();
            this.ServerIP = new System.Windows.Forms.TextBox();
            this.ServerPort = new System.Windows.Forms.TextBox();
            this.UserID = new System.Windows.Forms.TextBox();
            this.UserPwd = new System.Windows.Forms.TextBox();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelServer.ForeColor = System.Drawing.Color.Black;
            this.labelServer.Location = new System.Drawing.Point(40, 35);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(31, 15);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "서버";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelPort.ForeColor = System.Drawing.Color.Black;
            this.labelPort.Location = new System.Drawing.Point(40, 65);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(31, 15);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "포트";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelID.ForeColor = System.Drawing.Color.Black;
            this.labelID.Location = new System.Drawing.Point(40, 100);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(43, 15);
            this.labelID.TabIndex = 2;
            this.labelID.Text = "아이디";
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelPwd.ForeColor = System.Drawing.Color.Black;
            this.labelPwd.Location = new System.Drawing.Point(40, 132);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(31, 15);
            this.labelPwd.TabIndex = 3;
            this.labelPwd.Text = "암호";
            // 
            // ServerIP
            // 
            this.ServerIP.Location = new System.Drawing.Point(95, 31);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(180, 21);
            this.ServerIP.TabIndex = 4;
            this.ServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(95, 61);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(180, 21);
            this.ServerPort.TabIndex = 5;
            this.ServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserID
            // 
            this.UserID.Location = new System.Drawing.Point(95, 96);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(180, 21);
            this.UserID.TabIndex = 6;
            // 
            // UserPwd
            // 
            this.UserPwd.Location = new System.Drawing.Point(95, 128);
            this.UserPwd.Name = "UserPwd";
            this.UserPwd.PasswordChar = '*';
            this.UserPwd.Size = new System.Drawing.Size(180, 21);
            this.UserPwd.TabIndex = 7;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LoginBtn.Location = new System.Drawing.Point(95, 174);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(85, 30);
            this.LoginBtn.TabIndex = 8;
            this.LoginBtn.Text = "로그인";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.CancelBtn.Location = new System.Drawing.Point(190, 174);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(85, 30);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "취소";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.LoginBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
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
            this.Controls.Add(this.labelPwd);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "로그인";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.TextBox ServerIP;
        private System.Windows.Forms.TextBox ServerPort;
        private System.Windows.Forms.TextBox UserID;
        private System.Windows.Forms.TextBox UserPwd;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}