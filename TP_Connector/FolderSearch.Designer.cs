namespace TP_Connector
{
    partial class FolderSearch
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFolderName = new System.Windows.Forms.Label();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.searchCnt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFolderName
            // 
            this.lblFolderName.AutoSize = true;
            this.lblFolderName.Location = new System.Drawing.Point(24, 27);
            this.lblFolderName.Name = "lblFolderName";
            this.lblFolderName.Size = new System.Drawing.Size(41, 12);
            this.lblFolderName.TabIndex = 0;
            this.lblFolderName.Text = "자료관리";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(76, 24);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(193, 21);
            this.txtFolderName.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(286, 22);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(63, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "확인";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // searchCnt
            // 
            this.searchCnt.AutoSize = true;
            this.searchCnt.Location = new System.Drawing.Point(76, 50);
            this.searchCnt.Name = "searchCnt";
            this.searchCnt.Size = new System.Drawing.Size(33, 12);
            this.searchCnt.TabIndex = 3;
            this.searchCnt.Text = "(0/0)";
            // 
            // FolderSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 76);
            this.Controls.Add(this.searchCnt);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.lblFolderName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FolderSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "폴더 검색";
            this.Enter += new System.EventHandler(this.FolderSearch_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblFolderName;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label searchCnt;
    }
}