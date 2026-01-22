namespace TP_Connector
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabFormControl1 = new DevExpress.XtraBars.TabFormControl();
            this.barLabel = new DevExpress.XtraBars.BarStaticItem();
            this.tabFormDefaultManager1 = new DevExpress.XtraBars.TabFormDefaultManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tabFormPage1 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer1 = new DevExpress.XtraBars.TabFormContentContainer();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlFolder = new DevExpress.XtraTreeList.TreeList();
            this.colFolder = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnlSystemHeader = new DevExpress.XtraEditors.PanelControl();
            this.folderSerachButton = new DevExpress.XtraEditors.SimpleButton();
            this.lblSystemTitle = new DevExpress.XtraEditors.LabelControl();
            this.tlData = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colVersion = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGrade = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnlTopMenu = new DevExpress.XtraEditors.PanelControl();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheckIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.tabFormPage2 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer2 = new DevExpress.XtraBars.TabFormContentContainer();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormDefaultManager1)).BeginInit();
            this.tabFormContentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystemHeader)).BeginInit();
            this.pnlSystemHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopMenu)).BeginInit();
            this.pnlTopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFormControl1
            // 
            this.tabFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLabel});
            this.tabFormControl1.Location = new System.Drawing.Point(0, 0);
            this.tabFormControl1.Manager = this.tabFormDefaultManager1;
            this.tabFormControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabFormControl1.Name = "tabFormControl1";
            this.tabFormControl1.Pages.Add(this.tabFormPage1);
            this.tabFormControl1.Pages.Add(this.tabFormPage2);
            this.tabFormControl1.SelectedPage = this.tabFormPage1;
            this.tabFormControl1.Size = new System.Drawing.Size(1106, 72);
            this.tabFormControl1.TabForm = this;
            this.tabFormControl1.TabIndex = 0;
            this.tabFormControl1.TabStop = false;
            this.tabFormControl1.TitleItemLinks.Add(this.barLabel);
            this.tabFormControl1.OuterFormCreating += new DevExpress.XtraBars.OuterFormCreatingEventHandler(this.OnOuterFormCreating);
            // 
            // barLabel
            // 
            this.barLabel.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barLabel.Id = 3;
            this.barLabel.ImageToTextAlignment = DevExpress.XtraBars.BarItemImageToTextAlignment.AfterText;
            this.barLabel.Name = "barLabel";
            // 
            // tabFormDefaultManager1
            // 
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlTop);
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlBottom);
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlLeft);
            this.tabFormDefaultManager1.DockControls.Add(this.barDockControlRight);
            this.tabFormDefaultManager1.DockingEnabled = false;
            this.tabFormDefaultManager1.Form = this;
            this.tabFormDefaultManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLabel});
            this.tabFormDefaultManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 72);
            this.barDockControlTop.Manager = null;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1106, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 569);
            this.barDockControlBottom.Manager = null;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1106, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 72);
            this.barDockControlLeft.Manager = null;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 497);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1106, 72);
            this.barDockControlRight.Manager = null;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 497);
            // 
            // tabFormPage1
            // 
            this.tabFormPage1.ContentContainer = this.tabFormContentContainer1;
            this.tabFormPage1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("tabFormPage1.ImageOptions.SvgImage")));
            this.tabFormPage1.Name = "tabFormPage1";
            this.tabFormPage1.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.tabFormPage1.Text = "체크인/체크아웃";
            // 
            // tabFormContentContainer1
            // 
            this.tabFormContentContainer1.Controls.Add(this.splitContainerControl1);
            this.tabFormContentContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer1.Location = new System.Drawing.Point(0, 72);
            this.tabFormContentContainer1.Name = "tabFormContentContainer1";
            this.tabFormContentContainer1.Size = new System.Drawing.Size(1106, 497);
            this.tabFormContentContainer1.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.splitContainerControl1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tlFolder);
            this.splitContainerControl1.Panel1.Controls.Add(this.pnlSystemHeader);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.tlData);
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlTopMenu);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.splitContainerControl1.Size = new System.Drawing.Size(1106, 497);
            this.splitContainerControl1.SplitterPosition = 268;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // tlFolder
            // 
            this.tlFolder.Appearance.FocusedRow.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tlFolder.Appearance.FocusedRow.Options.UseBackColor = true;
            this.tlFolder.Appearance.SelectedRow.BackColor = System.Drawing.Color.Gray;
            this.tlFolder.Appearance.SelectedRow.Options.UseBackColor = true;
            this.tlFolder.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tlFolder.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colFolder});
            this.tlFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlFolder.Location = new System.Drawing.Point(0, 35);
            this.tlFolder.Name = "tlFolder";
            this.tlFolder.OptionsBehavior.Editable = false;
            this.tlFolder.OptionsView.ShowColumns = false;
            this.tlFolder.OptionsView.ShowHorzLines = false;
            this.tlFolder.OptionsView.ShowIndicator = false;
            this.tlFolder.OptionsView.ShowVertLines = false;
            this.tlFolder.Size = new System.Drawing.Size(268, 458);
            this.tlFolder.TabIndex = 1;
            // 
            // colFolder
            // 
            this.colFolder.Caption = "분류폴더";
            this.colFolder.FieldName = "FolderName";
            this.colFolder.Name = "colFolder";
            this.colFolder.Visible = true;
            this.colFolder.VisibleIndex = 0;
            // 
            // pnlSystemHeader
            // 
            this.pnlSystemHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.pnlSystemHeader.Appearance.Options.UseBackColor = true;
            this.pnlSystemHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlSystemHeader.Controls.Add(this.folderSerachButton);
            this.pnlSystemHeader.Controls.Add(this.lblSystemTitle);
            this.pnlSystemHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSystemHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSystemHeader.Name = "pnlSystemHeader";
            this.pnlSystemHeader.Size = new System.Drawing.Size(268, 35);
            this.pnlSystemHeader.TabIndex = 0;
            // 
            // folderSerachButton
            // 
            this.folderSerachButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.folderSerachButton.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.folderSerachButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("folderSerachButton.ImageOptions.SvgImage")));
            this.folderSerachButton.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.folderSerachButton.Location = new System.Drawing.Point(230, 0);
            this.folderSerachButton.Name = "folderSerachButton";
            this.folderSerachButton.Size = new System.Drawing.Size(38, 35);
            this.folderSerachButton.TabIndex = 1;
            this.folderSerachButton.Click += new System.EventHandler(this.folderSearchButton_Click);
            // 
            // lblSystemTitle
            // 
            this.lblSystemTitle.Appearance.BackColor = System.Drawing.Color.SeaGreen;
            this.lblSystemTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSystemTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblSystemTitle.Appearance.Options.UseBackColor = true;
            this.lblSystemTitle.Appearance.Options.UseFont = true;
            this.lblSystemTitle.Appearance.Options.UseForeColor = true;
            this.lblSystemTitle.Appearance.Options.UseTextOptions = true;
            this.lblSystemTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSystemTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSystemTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystemTitle.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSystemTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSystemTitle.Name = "lblSystemTitle";
            this.lblSystemTitle.Size = new System.Drawing.Size(268, 35);
            this.lblSystemTitle.TabIndex = 0;
            this.lblSystemTitle.Text = "시스템 폴더";
            // 
            // tlData
            // 
            this.tlData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tlData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName,
            this.colVersion,
            this.colGrade});
            this.tlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlData.Location = new System.Drawing.Point(0, 85);
            this.tlData.Name = "tlData";
            this.tlData.Size = new System.Drawing.Size(824, 408);
            this.tlData.TabIndex = 1;
            // 
            // colName
            // 
            this.colName.AppearanceHeader.BackColor = System.Drawing.Color.White;
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colName.AppearanceHeader.Options.UseBackColor = true;
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.AppearanceHeader.Options.UseForeColor = true;
            this.colName.Caption = "자료명";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 300;
            // 
            // colVersion
            // 
            this.colVersion.AppearanceCell.Options.UseTextOptions = true;
            this.colVersion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVersion.AppearanceHeader.BackColor = System.Drawing.Color.White;
            this.colVersion.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colVersion.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colVersion.AppearanceHeader.Options.UseBackColor = true;
            this.colVersion.AppearanceHeader.Options.UseFont = true;
            this.colVersion.AppearanceHeader.Options.UseForeColor = true;
            this.colVersion.Caption = "버전";
            this.colVersion.FieldName = "Version";
            this.colVersion.Name = "colVersion";
            this.colVersion.Visible = true;
            this.colVersion.VisibleIndex = 1;
            this.colVersion.Width = 80;
            // 
            // colGrade
            // 
            this.colGrade.AppearanceCell.Options.UseTextOptions = true;
            this.colGrade.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGrade.AppearanceHeader.BackColor = System.Drawing.Color.White;
            this.colGrade.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colGrade.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colGrade.AppearanceHeader.Options.UseBackColor = true;
            this.colGrade.AppearanceHeader.Options.UseFont = true;
            this.colGrade.AppearanceHeader.Options.UseForeColor = true;
            this.colGrade.Caption = "등급";
            this.colGrade.FieldName = "Grade";
            this.colGrade.Name = "colGrade";
            this.colGrade.Visible = true;
            this.colGrade.VisibleIndex = 2;
            this.colGrade.Width = 80;
            // 
            // pnlTopMenu
            // 
            this.pnlTopMenu.Controls.Add(this.btnRun);
            this.pnlTopMenu.Controls.Add(this.btnCheckIn);
            this.pnlTopMenu.Controls.Add(this.btnRefresh);
            this.pnlTopMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlTopMenu.Name = "pnlTopMenu";
            this.pnlTopMenu.Size = new System.Drawing.Size(824, 85);
            this.pnlTopMenu.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnRun.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnRun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRun.ImageOptions.SvgImage")));
            this.btnRun.Location = new System.Drawing.Point(156, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnRun.Size = new System.Drawing.Size(70, 75);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "프로그램\r\n실행";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnCheckIn.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCheckIn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCheckIn.ImageOptions.SvgImage")));
            this.btnCheckIn.Location = new System.Drawing.Point(80, 4);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCheckIn.Size = new System.Drawing.Size(70, 75);
            this.btnCheckIn.TabIndex = 3;
            this.btnCheckIn.Text = "체크인";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnRefresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRefresh.ImageOptions.SvgImage")));
            this.btnRefresh.Location = new System.Drawing.Point(4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnRefresh.Size = new System.Drawing.Size(70, 75);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "새로\r\n고침";
            // 
            // tabFormPage2
            // 
            this.tabFormPage2.ContentContainer = this.tabFormContentContainer2;
            this.tabFormPage2.Name = "tabFormPage2";
            this.tabFormPage2.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.tabFormPage2.Text = "신규등록";
            // 
            // tabFormContentContainer2
            // 
            this.tabFormContentContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer2.Location = new System.Drawing.Point(0, 72);
            this.tabFormContentContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabFormContentContainer2.Name = "tabFormContentContainer2";
            this.tabFormContentContainer2.Size = new System.Drawing.Size(1106, 497);
            this.tabFormContentContainer2.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 569);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.tabFormContentContainer1);
            this.Controls.Add(this.tabFormControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Main.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Main";
            this.TabFormControl = this.tabFormControl1;
            this.Text = "TP_Connector";
            this.Activated += new System.EventHandler(this.Main_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormDefaultManager1)).EndInit();
            this.tabFormContentContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystemHeader)).EndInit();
            this.pnlSystemHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopMenu)).EndInit();
            this.pnlTopMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.TabFormControl tabFormControl1;
        private DevExpress.XtraBars.TabFormDefaultManager tabFormDefaultManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer1;
        private DevExpress.XtraBars.TabFormPage tabFormPage1;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer2;
        private DevExpress.XtraBars.TabFormPage tabFormPage2;

        // 새로 정의된 컨트롤
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;

        // 좌측 컨트롤 (버튼 -> 라벨 헤더로 변경)
        private DevExpress.XtraEditors.PanelControl pnlSystemHeader;
        private DevExpress.XtraEditors.LabelControl lblSystemTitle;

        // 우측 컨트롤 (리본 스타일 버튼 적용)
        private DevExpress.XtraEditors.PanelControl pnlTopMenu;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnCheckIn;
        private DevExpress.XtraEditors.SimpleButton btnRun;

        private DevExpress.XtraTreeList.TreeList tlData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colVersion;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGrade;
        private DevExpress.XtraTreeList.TreeList tlFolder;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFolder;
        private DevExpress.XtraBars.BarStaticItem barLabel;
        private DevExpress.XtraEditors.SimpleButton folderSerachButton;
    }
}