namespace TP_Connector
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlvFolder = new BrightIdeasSoftware.TreeListView();
            this.pnlSystemHeader = new System.Windows.Forms.Panel();
            this.folderSearchButton = new System.Windows.Forms.Button();
            this.lblSystemTitle = new System.Windows.Forms.Label();
            this.tlvData = new BrightIdeasSoftware.TreeListView();
            this.pnlTopMenu = new System.Windows.Forms.Panel();
            this.rowAdd = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tlvFolder2 = new BrightIdeasSoftware.TreeListView();
            this.pnlSystemHeader2 = new System.Windows.Forms.Panel();
            this.lblSystemTitle2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tlvLocalFolder = new BrightIdeasSoftware.TreeListView();
            this.pnlLocalHeader = new System.Windows.Forms.Panel();
            this.btnLocalFolderSelect = new System.Windows.Forms.Button();
            this.lblLocalTitle = new System.Windows.Forms.Label();
            this.newAddGrid = new BrightIdeasSoftware.TreeListView();
            this.pnlTopMenu2 = new System.Windows.Forms.Panel();
            this.resetGrid = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvFolder)).BeginInit();
            this.pnlSystemHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvData)).BeginInit();
            this.pnlTopMenu.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvFolder2)).BeginInit();
            this.pnlSystemHeader2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvLocalFolder)).BeginInit();
            this.pnlLocalHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newAddGrid)).BeginInit();
            this.pnlTopMenu2.SuspendLayout();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1293, 691);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.pnlTopMenu);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1285, 665);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "체크인/체크아웃";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 88);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlvFolder);
            this.splitContainer1.Panel1.Controls.Add(this.pnlSystemHeader);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tlvData);
            this.splitContainer1.Size = new System.Drawing.Size(1279, 574);
            this.splitContainer1.SplitterDistance = 268;
            this.splitContainer1.TabIndex = 1;
            // 
            // tlvFolder
            // 
            this.tlvFolder.CellEditUseWholeCell = false;
            this.tlvFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvFolder.HideSelection = false;
            this.tlvFolder.Location = new System.Drawing.Point(0, 35);
            this.tlvFolder.Name = "tlvFolder";
            this.tlvFolder.ShowGroups = false;
            this.tlvFolder.Size = new System.Drawing.Size(268, 539);
            this.tlvFolder.TabIndex = 1;
            this.tlvFolder.UseCompatibleStateImageBehavior = false;
            this.tlvFolder.View = System.Windows.Forms.View.Details;
            this.tlvFolder.VirtualMode = true;
            this.tlvFolder.CellClick += new System.EventHandler<BrightIdeasSoftware.CellClickEventArgs>(this.tlvFolder_CellClick);
            this.tlvFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tlvFolder_MouseDoubleClick);
            // 
            // pnlSystemHeader
            // 
            this.pnlSystemHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.pnlSystemHeader.Controls.Add(this.folderSearchButton);
            this.pnlSystemHeader.Controls.Add(this.lblSystemTitle);
            this.pnlSystemHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSystemHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSystemHeader.Name = "pnlSystemHeader";
            this.pnlSystemHeader.Size = new System.Drawing.Size(268, 35);
            this.pnlSystemHeader.TabIndex = 0;
            // 
            // folderSearchButton
            // 
            this.folderSearchButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.folderSearchButton.Location = new System.Drawing.Point(205, 0);
            this.folderSearchButton.Name = "folderSearchButton";
            this.folderSearchButton.Size = new System.Drawing.Size(63, 35);
            this.folderSearchButton.TabIndex = 1;
            this.folderSearchButton.Text = "검색";
            this.folderSearchButton.UseVisualStyleBackColor = true;
            this.folderSearchButton.Visible = false;
            this.folderSearchButton.Click += new System.EventHandler(this.folderSearchButton_Click);
            // 
            // lblSystemTitle
            // 
            this.lblSystemTitle.BackColor = System.Drawing.Color.SeaGreen;
            this.lblSystemTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystemTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSystemTitle.ForeColor = System.Drawing.Color.White;
            this.lblSystemTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSystemTitle.Name = "lblSystemTitle";
            this.lblSystemTitle.Size = new System.Drawing.Size(268, 35);
            this.lblSystemTitle.TabIndex = 0;
            this.lblSystemTitle.Text = "시스템 폴더";
            this.lblSystemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlvData
            // 
            this.tlvData.CellEditUseWholeCell = false;
            this.tlvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvData.FullRowSelect = true;
            this.tlvData.GridLines = true;
            this.tlvData.HideSelection = false;
            this.tlvData.Location = new System.Drawing.Point(0, 0);
            this.tlvData.MultiSelect = false;
            this.tlvData.Name = "tlvData";
            this.tlvData.RowHeight = 25;
            this.tlvData.ShowGroups = false;
            this.tlvData.Size = new System.Drawing.Size(1007, 574);
            this.tlvData.TabIndex = 0;
            this.tlvData.UseCompatibleStateImageBehavior = false;
            this.tlvData.View = System.Windows.Forms.View.Details;
            this.tlvData.VirtualMode = true;
            // 
            // pnlTopMenu
            // 
            this.pnlTopMenu.Controls.Add(this.rowAdd);
            this.pnlTopMenu.Controls.Add(this.btnRun);
            this.pnlTopMenu.Controls.Add(this.btnCheckOut);
            this.pnlTopMenu.Controls.Add(this.btnCheckIn);
            this.pnlTopMenu.Controls.Add(this.btnRefresh);
            this.pnlTopMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopMenu.Location = new System.Drawing.Point(3, 3);
            this.pnlTopMenu.Name = "pnlTopMenu";
            this.pnlTopMenu.Size = new System.Drawing.Size(1279, 85);
            this.pnlTopMenu.TabIndex = 0;
            // 
            // rowAdd
            // 
            this.rowAdd.Location = new System.Drawing.Point(575, 4);
            this.rowAdd.Name = "rowAdd";
            this.rowAdd.Size = new System.Drawing.Size(70, 75);
            this.rowAdd.TabIndex = 2;
            this.rowAdd.Text = "추가 파일 등록";
            this.rowAdd.UseVisualStyleBackColor = true;
            this.rowAdd.Click += new System.EventHandler(this.rowAdd_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(499, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(70, 75);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "프로그램\r\n실행";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Location = new System.Drawing.Point(423, 4);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(70, 75);
            this.btnCheckOut.TabIndex = 1;
            this.btnCheckOut.Text = "체크아웃";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(349, 4);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(70, 75);
            this.btnCheckIn.TabIndex = 1;
            this.btnCheckIn.Text = "체크인";
            this.btnCheckIn.UseVisualStyleBackColor = true;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(273, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 75);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "새로\r\n고침";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Controls.Add(this.pnlTopMenu2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1285, 665);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "신규등록";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 88);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tlvFolder2);
            this.splitContainer2.Panel1.Controls.Add(this.pnlSystemHeader2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1279, 574);
            this.splitContainer2.SplitterDistance = 268;
            this.splitContainer2.TabIndex = 1;
            // 
            // tlvFolder2
            // 
            this.tlvFolder2.CellEditUseWholeCell = false;
            this.tlvFolder2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvFolder2.HideSelection = false;
            this.tlvFolder2.Location = new System.Drawing.Point(0, 35);
            this.tlvFolder2.Name = "tlvFolder2";
            this.tlvFolder2.ShowGroups = false;
            this.tlvFolder2.Size = new System.Drawing.Size(268, 539);
            this.tlvFolder2.TabIndex = 1;
            this.tlvFolder2.UseCompatibleStateImageBehavior = false;
            this.tlvFolder2.View = System.Windows.Forms.View.Details;
            this.tlvFolder2.VirtualMode = true;
            this.tlvFolder2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tlvFolder2_MouseDoubleClick);
            // 
            // pnlSystemHeader2
            // 
            this.pnlSystemHeader2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.pnlSystemHeader2.Controls.Add(this.lblSystemTitle2);
            this.pnlSystemHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSystemHeader2.Location = new System.Drawing.Point(0, 0);
            this.pnlSystemHeader2.Name = "pnlSystemHeader2";
            this.pnlSystemHeader2.Size = new System.Drawing.Size(268, 35);
            this.pnlSystemHeader2.TabIndex = 0;
            // 
            // lblSystemTitle2
            // 
            this.lblSystemTitle2.BackColor = System.Drawing.Color.DarkRed;
            this.lblSystemTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystemTitle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSystemTitle2.ForeColor = System.Drawing.Color.White;
            this.lblSystemTitle2.Location = new System.Drawing.Point(0, 0);
            this.lblSystemTitle2.Name = "lblSystemTitle2";
            this.lblSystemTitle2.Size = new System.Drawing.Size(268, 35);
            this.lblSystemTitle2.TabIndex = 0;
            this.lblSystemTitle2.Text = "시스템 신규등록";
            this.lblSystemTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tlvLocalFolder);
            this.splitContainer3.Panel1.Controls.Add(this.pnlLocalHeader);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.newAddGrid);
            this.splitContainer3.Size = new System.Drawing.Size(1007, 574);
            this.splitContainer3.SplitterDistance = 330;
            this.splitContainer3.TabIndex = 0;
            // 
            // tlvLocalFolder
            // 
            this.tlvLocalFolder.CellEditUseWholeCell = false;
            this.tlvLocalFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvLocalFolder.HideSelection = false;
            this.tlvLocalFolder.Location = new System.Drawing.Point(0, 35);
            this.tlvLocalFolder.Name = "tlvLocalFolder";
            this.tlvLocalFolder.ShowGroups = false;
            this.tlvLocalFolder.Size = new System.Drawing.Size(330, 539);
            this.tlvLocalFolder.TabIndex = 1;
            this.tlvLocalFolder.UseCompatibleStateImageBehavior = false;
            this.tlvLocalFolder.View = System.Windows.Forms.View.Details;
            this.tlvLocalFolder.VirtualMode = true;
            // 
            // pnlLocalHeader
            // 
            this.pnlLocalHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.pnlLocalHeader.Controls.Add(this.btnLocalFolderSelect);
            this.pnlLocalHeader.Controls.Add(this.lblLocalTitle);
            this.pnlLocalHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLocalHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlLocalHeader.Name = "pnlLocalHeader";
            this.pnlLocalHeader.Size = new System.Drawing.Size(330, 35);
            this.pnlLocalHeader.TabIndex = 0;
            // 
            // btnLocalFolderSelect
            // 
            this.btnLocalFolderSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLocalFolderSelect.Location = new System.Drawing.Point(264, 0);
            this.btnLocalFolderSelect.Name = "btnLocalFolderSelect";
            this.btnLocalFolderSelect.Size = new System.Drawing.Size(66, 35);
            this.btnLocalFolderSelect.TabIndex = 1;
            this.btnLocalFolderSelect.Text = "선택";
            this.btnLocalFolderSelect.UseVisualStyleBackColor = true;
            // 
            // lblLocalTitle
            // 
            this.lblLocalTitle.BackColor = System.Drawing.Color.Chocolate;
            this.lblLocalTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocalTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLocalTitle.ForeColor = System.Drawing.Color.White;
            this.lblLocalTitle.Location = new System.Drawing.Point(0, 0);
            this.lblLocalTitle.Name = "lblLocalTitle";
            this.lblLocalTitle.Size = new System.Drawing.Size(330, 35);
            this.lblLocalTitle.TabIndex = 0;
            this.lblLocalTitle.Text = "로컬 폴더";
            this.lblLocalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // newAddGrid
            // 
            this.newAddGrid.AllowDrop = true;
            this.newAddGrid.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.newAddGrid.CellEditTabChangesRows = true;
            this.newAddGrid.CellEditUseWholeCell = false;
            this.newAddGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newAddGrid.FullRowSelect = true;
            this.newAddGrid.GridLines = true;
            this.newAddGrid.HideSelection = false;
            this.newAddGrid.Location = new System.Drawing.Point(0, 0);
            this.newAddGrid.Name = "newAddGrid";
            this.newAddGrid.RowHeight = 25;
            this.newAddGrid.ShowGroups = false;
            this.newAddGrid.Size = new System.Drawing.Size(673, 574);
            this.newAddGrid.TabIndex = 0;
            this.newAddGrid.UseCompatibleStateImageBehavior = false;
            this.newAddGrid.View = System.Windows.Forms.View.Details;
            this.newAddGrid.VirtualMode = true;
            this.newAddGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.newAddGrid_DragDrop);
            this.newAddGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.newAddGrid_DragEnter);
            // 
            // pnlTopMenu2
            // 
            this.pnlTopMenu2.Controls.Add(this.resetGrid);
            this.pnlTopMenu2.Controls.Add(this.btnUpload);
            this.pnlTopMenu2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopMenu2.Location = new System.Drawing.Point(3, 3);
            this.pnlTopMenu2.Name = "pnlTopMenu2";
            this.pnlTopMenu2.Size = new System.Drawing.Size(1279, 85);
            this.pnlTopMenu2.TabIndex = 0;
            // 
            // resetGrid
            // 
            this.resetGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetGrid.Location = new System.Drawing.Point(1042, 4);
            this.resetGrid.Name = "resetGrid";
            this.resetGrid.Size = new System.Drawing.Size(112, 75);
            this.resetGrid.TabIndex = 2;
            this.resetGrid.Text = "초기화";
            this.resetGrid.UseVisualStyleBackColor = true;
            this.resetGrid.Click += new System.EventHandler(this.resetGrid_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Location = new System.Drawing.Point(1159, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(112, 75);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "업로드\r\n확정";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // status
            // 
            this.status.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.status.Location = new System.Drawing.Point(0, 669);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1293, 22);
            this.status.TabIndex = 1;
            this.status.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(121, 17);
            this.statusLabel.Text = "toolStripStatusLabel1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 691);
            this.Controls.Add(this.status);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "TP_Connector";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvFolder)).EndInit();
            this.pnlSystemHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvData)).EndInit();
            this.pnlTopMenu.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvFolder2)).EndInit();
            this.pnlSystemHeader2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvLocalFolder)).EndInit();
            this.pnlLocalHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.newAddGrid)).EndInit();
            this.pnlTopMenu2.ResumeLayout(false);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlSystemHeader;
        private System.Windows.Forms.Button folderSearchButton;
        private System.Windows.Forms.Label lblSystemTitle;

        // ★★★ 이 부분(ObjectListView 선언)이 있는지 꼭 확인해주세요! ★★★
        private BrightIdeasSoftware.TreeListView tlvFolder;
        private BrightIdeasSoftware.TreeListView tlvData;

        private System.Windows.Forms.Panel pnlTopMenu;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer splitContainer2;

        // ★★★ 이 부분도 있는지 확인해주세요! ★★★
        private BrightIdeasSoftware.TreeListView tlvFolder2;
        private BrightIdeasSoftware.TreeListView newAddGrid;

        private System.Windows.Forms.Panel pnlSystemHeader2;
        private System.Windows.Forms.Label lblSystemTitle2;
        private System.Windows.Forms.Panel pnlTopMenu2;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

        // [추가] 로컬 폴더 영역 컨트롤
        private System.Windows.Forms.SplitContainer splitContainer3;
        private BrightIdeasSoftware.TreeListView tlvLocalFolder;
        private System.Windows.Forms.Panel pnlLocalHeader;
        private System.Windows.Forms.Button btnLocalFolderSelect;
        private System.Windows.Forms.Label lblLocalTitle;
        private System.Windows.Forms.Button resetGrid;
        private System.Windows.Forms.Button rowAdd;
    }
}