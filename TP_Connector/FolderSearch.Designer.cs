namespace TP_Connector
{
    partial class FolderSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderSearch));
            this.lblFolderName = new DevExpress.XtraEditors.LabelControl();
            this.txtFolderName = new DevExpress.XtraEditors.TextEdit();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.searchCnt = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFolderName
            // 
            this.lblFolderName.Location = new System.Drawing.Point(24, 27);
            this.lblFolderName.Name = "lblFolderName";
            this.lblFolderName.Size = new System.Drawing.Size(30, 14);
            this.lblFolderName.TabIndex = 0;
            this.lblFolderName.Text = "폴더명";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(76, 24);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(193, 20);
            this.txtFolderName.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(286, 22);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(63, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "확인";
            this.btnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // searchCnt
            // 
            this.searchCnt.Location = new System.Drawing.Point(76, 50);
            this.searchCnt.Name = "searchCnt";
            this.searchCnt.Size = new System.Drawing.Size(29, 14);
            this.searchCnt.TabIndex = 3;
            this.searchCnt.Text = "(0/0)";
            // 
            // FolderSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 76);
            this.Controls.Add(this.searchCnt);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.lblFolderName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FolderSearch.IconOptions.SvgImage")));
            this.Name = "FolderSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "폴더 검색";
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblFolderName;
        private DevExpress.XtraEditors.TextEdit txtFolderName;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.LabelControl searchCnt;
    }
}