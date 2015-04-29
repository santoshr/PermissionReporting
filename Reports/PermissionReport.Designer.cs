namespace PermissionReporting
{
    partial class PermissionReport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionReport));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleReprotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.picFilterButton = new System.Windows.Forms.PictureBox();
            this.tvSiteTree = new System.Windows.Forms.TreeView();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiLoadPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiLoadFirstLevelItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadAllItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadReport = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageOutputLog = new System.Windows.Forms.TabPage();
            this.txtOutputLog = new System.Windows.Forms.TextBox();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvMembership = new System.Windows.Forms.TreeView();
            this.tvPermission = new System.Windows.Forms.TreeView();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxListItemLevel = new System.Windows.Forms.GroupBox();
            this.rdoLoadAllItems = new System.Windows.Forms.RadioButton();
            this.rdoLoadFirstLevel = new System.Windows.Forms.RadioButton();
            this.groupBoxLoadDepth = new System.Windows.Forms.GroupBox();
            this.rdoLoadStructureFull = new System.Windows.Forms.RadioButton();
            this.rdoLoadStructurePartial = new System.Windows.Forms.RadioButton();
            this.btnPopulateSiteStructure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSiteCollectionUrl = new System.Windows.Forms.TextBox();
            this.groupBoxListItemProcessing = new System.Windows.Forms.GroupBox();
            this.rdoItemLevelFalse = new System.Windows.Forms.RadioButton();
            this.rdoItemLevelTrue = new System.Windows.Forms.RadioButton();
            this.bgwSiteDataWorker = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bgwPermissionWorker = new System.ComponentModel.BackgroundWorker();
            this.bgwLoadItemWorker = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilterButton)).BeginInit();
            this.contextMenuTreeView.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageOutputLog.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxListItemLevel.SuspendLayout();
            this.groupBoxLoadDepth.SuspendLayout();
            this.groupBoxListItemProcessing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scheduleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(705, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadReportToolStripMenuItem,
            this.saveReportToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadReportToolStripMenuItem
            // 
            this.loadReportToolStripMenuItem.Name = "loadReportToolStripMenuItem";
            this.loadReportToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.loadReportToolStripMenuItem.Text = "Load Report";
            this.loadReportToolStripMenuItem.Click += new System.EventHandler(this.loadReportToolStripMenuItem_Click);
            // 
            // saveReportToolStripMenuItem
            // 
            this.saveReportToolStripMenuItem.Name = "saveReportToolStripMenuItem";
            this.saveReportToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.saveReportToolStripMenuItem.Text = "Save Report";
            this.saveReportToolStripMenuItem.Click += new System.EventHandler(this.saveReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // scheduleToolStripMenuItem
            // 
            this.scheduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleReprotsToolStripMenuItem});
            this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
            this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.scheduleToolStripMenuItem.Text = "Options";
            // 
            // scheduleReprotsToolStripMenuItem
            // 
            this.scheduleReprotsToolStripMenuItem.Name = "scheduleReprotsToolStripMenuItem";
            this.scheduleReprotsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.scheduleReprotsToolStripMenuItem.Text = "Schedule Reports";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 196);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.picFilterButton);
            this.splitContainer1.Panel1.Controls.Add(this.tvSiteTree);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.chkAutoScroll);
            this.splitContainer1.Size = new System.Drawing.Size(681, 517);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // picFilterButton
            // 
            this.picFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picFilterButton.ErrorImage = null;
            this.picFilterButton.Image = global::PermissionReporting.Properties.Resources.FILTER;
            this.picFilterButton.InitialImage = null;
            this.picFilterButton.Location = new System.Drawing.Point(207, 0);
            this.picFilterButton.Name = "picFilterButton";
            this.picFilterButton.Size = new System.Drawing.Size(17, 17);
            this.picFilterButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picFilterButton.TabIndex = 1;
            this.picFilterButton.TabStop = false;
            this.toolTip1.SetToolTip(this.picFilterButton, "Click to show only unique permission nodes");
            this.picFilterButton.Click += new System.EventHandler(this.picFilterButton_Click);
            // 
            // tvSiteTree
            // 
            this.tvSiteTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSiteTree.ContextMenuStrip = this.contextMenuTreeView;
            this.tvSiteTree.ImageIndex = 0;
            this.tvSiteTree.ImageList = this.imageList;
            this.tvSiteTree.Location = new System.Drawing.Point(0, 23);
            this.tvSiteTree.Name = "tvSiteTree";
            this.tvSiteTree.SelectedImageIndex = 0;
            this.tvSiteTree.Size = new System.Drawing.Size(227, 494);
            this.tvSiteTree.TabIndex = 0;
            this.tvSiteTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSiteTree_AfterSelect);
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadPermission,
            this.toolStripSeparator2,
            this.tsmiLoadFirstLevelItems,
            this.tsmiLoadAllItems,
            this.toolStripSeparator1,
            this.tsmiSaveReport,
            this.tsmiLoadReport});
            this.contextMenuTreeView.Name = "contextMenuTreeView";
            this.contextMenuTreeView.Size = new System.Drawing.Size(180, 126);
            this.contextMenuTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuTreeView_Opening);
            this.contextMenuTreeView.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuTreeView_ItemClicked);
            // 
            // tsmiLoadPermission
            // 
            this.tsmiLoadPermission.Name = "tsmiLoadPermission";
            this.tsmiLoadPermission.Size = new System.Drawing.Size(179, 22);
            this.tsmiLoadPermission.Text = "Load Permissions";
            this.tsmiLoadPermission.ToolTipText = "Loads permission from the site";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // tsmiLoadFirstLevelItems
            // 
            this.tsmiLoadFirstLevelItems.Name = "tsmiLoadFirstLevelItems";
            this.tsmiLoadFirstLevelItems.Size = new System.Drawing.Size(179, 22);
            this.tsmiLoadFirstLevelItems.Text = "Load First Level Items";
            // 
            // tsmiLoadAllItems
            // 
            this.tsmiLoadAllItems.Name = "tsmiLoadAllItems";
            this.tsmiLoadAllItems.Size = new System.Drawing.Size(179, 22);
            this.tsmiLoadAllItems.Text = "Load All Items";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // tsmiSaveReport
            // 
            this.tsmiSaveReport.Name = "tsmiSaveReport";
            this.tsmiSaveReport.Size = new System.Drawing.Size(179, 22);
            this.tsmiSaveReport.Text = "Save Report";
            this.tsmiSaveReport.ToolTipText = "Saves the current report to a file";
            // 
            // tsmiLoadReport
            // 
            this.tsmiLoadReport.Name = "tsmiLoadReport";
            this.tsmiLoadReport.Size = new System.Drawing.Size(179, 22);
            this.tsmiLoadReport.Text = "Load From Report";
            this.tsmiLoadReport.ToolTipText = "Loads a previously run report from file";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Site");
            this.imageList.Images.SetKeyName(1, "SecureSite");
            this.imageList.Images.SetKeyName(2, "Library");
            this.imageList.Images.SetKeyName(3, "SecureLibrary");
            this.imageList.Images.SetKeyName(4, "List");
            this.imageList.Images.SetKeyName(5, "SecureList");
            this.imageList.Images.SetKeyName(6, "Folder");
            this.imageList.Images.SetKeyName(7, "SecureFolder");
            this.imageList.Images.SetKeyName(8, "ListItem");
            this.imageList.Images.SetKeyName(9, "SecureListItem");
            this.imageList.Images.SetKeyName(10, "File");
            this.imageList.Images.SetKeyName(11, "SecureFile");
            this.imageList.Images.SetKeyName(12, "PermissionDetails");
            this.imageList.Images.SetKeyName(13, "BasePermissions");
            this.imageList.Images.SetKeyName(14, "BasePermission");
            this.imageList.Images.SetKeyName(15, "Group");
            this.imageList.Images.SetKeyName(16, "Users");
            this.imageList.Images.SetKeyName(17, "User");
            this.imageList.Images.SetKeyName(18, "ExportExcel");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageOutputLog);
            this.tabControl1.Controls.Add(this.tabPageDetails);
            this.tabControl1.Location = new System.Drawing.Point(0, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(448, 501);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageOutputLog
            // 
            this.tabPageOutputLog.Controls.Add(this.txtOutputLog);
            this.tabPageOutputLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutputLog.Name = "tabPageOutputLog";
            this.tabPageOutputLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutputLog.Size = new System.Drawing.Size(440, 475);
            this.tabPageOutputLog.TabIndex = 0;
            this.tabPageOutputLog.Text = "Output Log";
            this.tabPageOutputLog.UseVisualStyleBackColor = true;
            // 
            // txtOutputLog
            // 
            this.txtOutputLog.BackColor = System.Drawing.SystemColors.Control;
            this.txtOutputLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputLog.Location = new System.Drawing.Point(3, 3);
            this.txtOutputLog.Multiline = true;
            this.txtOutputLog.Name = "txtOutputLog";
            this.txtOutputLog.ReadOnly = true;
            this.txtOutputLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutputLog.Size = new System.Drawing.Size(434, 469);
            this.txtOutputLog.TabIndex = 0;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.splitContainer2);
            this.tabPageDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(440, 475);
            this.tabPageDetails.TabIndex = 1;
            this.tabPageDetails.Text = "Details";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tvMembership);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvPermission);
            this.splitContainer2.Size = new System.Drawing.Size(434, 469);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.TabIndex = 0;
            // 
            // tvMembership
            // 
            this.tvMembership.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMembership.ImageIndex = 0;
            this.tvMembership.ImageList = this.imageList;
            this.tvMembership.Location = new System.Drawing.Point(0, 0);
            this.tvMembership.Name = "tvMembership";
            this.tvMembership.SelectedImageIndex = 0;
            this.tvMembership.Size = new System.Drawing.Size(223, 469);
            this.tvMembership.TabIndex = 0;
            this.tvMembership.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMembership_AfterSelect);
            // 
            // tvPermission
            // 
            this.tvPermission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPermission.ImageIndex = 0;
            this.tvPermission.ImageList = this.imageList;
            this.tvPermission.Location = new System.Drawing.Point(0, 0);
            this.tvPermission.Name = "tvPermission";
            this.tvPermission.SelectedImageIndex = 0;
            this.tvPermission.Size = new System.Drawing.Size(207, 469);
            this.tvPermission.TabIndex = 0;
            // 
            // chkAutoScroll
            // 
            this.chkAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(294, 3);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(125, 17);
            this.chkAutoScroll.TabIndex = 1;
            this.chkAutoScroll.Text = "Auto scroll output log";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 729);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(705, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(400, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBoxListItemLevel);
            this.groupBox1.Controls.Add(this.groupBoxLoadDepth);
            this.groupBox1.Controls.Add(this.btnPopulateSiteStructure);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSiteCollectionUrl);
            this.groupBox1.Controls.Add(this.groupBoxListItemProcessing);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 141);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // groupBoxListItemLevel
            // 
            this.groupBoxListItemLevel.Controls.Add(this.rdoLoadAllItems);
            this.groupBoxListItemLevel.Controls.Add(this.rdoLoadFirstLevel);
            this.groupBoxListItemLevel.Enabled = false;
            this.groupBoxListItemLevel.Location = new System.Drawing.Point(400, 52);
            this.groupBoxListItemLevel.Name = "groupBoxListItemLevel";
            this.groupBoxListItemLevel.Size = new System.Drawing.Size(163, 71);
            this.groupBoxListItemLevel.TabIndex = 2;
            this.groupBoxListItemLevel.TabStop = false;
            this.groupBoxListItemLevel.Text = "List Item Level";
            // 
            // rdoLoadAllItems
            // 
            this.rdoLoadAllItems.AutoSize = true;
            this.rdoLoadAllItems.Location = new System.Drawing.Point(18, 43);
            this.rdoLoadAllItems.Name = "rdoLoadAllItems";
            this.rdoLoadAllItems.Size = new System.Drawing.Size(89, 17);
            this.rdoLoadAllItems.TabIndex = 1;
            this.rdoLoadAllItems.Text = "Load all items";
            this.rdoLoadAllItems.UseVisualStyleBackColor = true;
            // 
            // rdoLoadFirstLevel
            // 
            this.rdoLoadFirstLevel.AutoSize = true;
            this.rdoLoadFirstLevel.Checked = true;
            this.rdoLoadFirstLevel.Location = new System.Drawing.Point(18, 19);
            this.rdoLoadFirstLevel.Name = "rdoLoadFirstLevel";
            this.rdoLoadFirstLevel.Size = new System.Drawing.Size(120, 17);
            this.rdoLoadFirstLevel.TabIndex = 0;
            this.rdoLoadFirstLevel.TabStop = true;
            this.rdoLoadFirstLevel.Text = "Load first level items";
            this.rdoLoadFirstLevel.UseVisualStyleBackColor = true;
            // 
            // groupBoxLoadDepth
            // 
            this.groupBoxLoadDepth.Controls.Add(this.rdoLoadStructureFull);
            this.groupBoxLoadDepth.Controls.Add(this.rdoLoadStructurePartial);
            this.groupBoxLoadDepth.Location = new System.Drawing.Point(25, 52);
            this.groupBoxLoadDepth.Name = "groupBoxLoadDepth";
            this.groupBoxLoadDepth.Size = new System.Drawing.Size(178, 71);
            this.groupBoxLoadDepth.TabIndex = 2;
            this.groupBoxLoadDepth.TabStop = false;
            this.groupBoxLoadDepth.Text = "Site Permissions";
            // 
            // rdoLoadStructureFull
            // 
            this.rdoLoadStructureFull.AutoSize = true;
            this.rdoLoadStructureFull.Checked = true;
            this.rdoLoadStructureFull.Location = new System.Drawing.Point(18, 19);
            this.rdoLoadStructureFull.Name = "rdoLoadStructureFull";
            this.rdoLoadStructureFull.Size = new System.Drawing.Size(107, 17);
            this.rdoLoadStructureFull.TabIndex = 1;
            this.rdoLoadStructureFull.TabStop = true;
            this.rdoLoadStructureFull.Text = "Load Permissions";
            this.rdoLoadStructureFull.UseVisualStyleBackColor = true;
            this.rdoLoadStructureFull.CheckedChanged += new System.EventHandler(this.rdoLoadStructureFull_CheckedChanged);
            // 
            // rdoLoadStructurePartial
            // 
            this.rdoLoadStructurePartial.AutoSize = true;
            this.rdoLoadStructurePartial.Location = new System.Drawing.Point(18, 43);
            this.rdoLoadStructurePartial.Name = "rdoLoadStructurePartial";
            this.rdoLoadStructurePartial.Size = new System.Drawing.Size(137, 17);
            this.rdoLoadStructurePartial.TabIndex = 0;
            this.rdoLoadStructurePartial.Text = "Do not load permissions";
            this.rdoLoadStructurePartial.UseVisualStyleBackColor = true;
            // 
            // btnPopulateSiteStructure
            // 
            this.btnPopulateSiteStructure.Location = new System.Drawing.Point(599, 99);
            this.btnPopulateSiteStructure.Name = "btnPopulateSiteStructure";
            this.btnPopulateSiteStructure.Size = new System.Drawing.Size(64, 23);
            this.btnPopulateSiteStructure.TabIndex = 4;
            this.btnPopulateSiteStructure.Text = "Go";
            this.btnPopulateSiteStructure.UseVisualStyleBackColor = true;
            this.btnPopulateSiteStructure.Click += new System.EventHandler(this.btnPopulateSiteStructure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Site Link";
            // 
            // txtSiteCollectionUrl
            // 
            this.txtSiteCollectionUrl.Location = new System.Drawing.Point(112, 23);
            this.txtSiteCollectionUrl.Name = "txtSiteCollectionUrl";
            this.txtSiteCollectionUrl.Size = new System.Drawing.Size(551, 20);
            this.txtSiteCollectionUrl.TabIndex = 2;
            // 
            // groupBoxListItemProcessing
            // 
            this.groupBoxListItemProcessing.Controls.Add(this.rdoItemLevelFalse);
            this.groupBoxListItemProcessing.Controls.Add(this.rdoItemLevelTrue);
            this.groupBoxListItemProcessing.Location = new System.Drawing.Point(220, 52);
            this.groupBoxListItemProcessing.Name = "groupBoxListItemProcessing";
            this.groupBoxListItemProcessing.Size = new System.Drawing.Size(163, 71);
            this.groupBoxListItemProcessing.TabIndex = 0;
            this.groupBoxListItemProcessing.TabStop = false;
            this.groupBoxListItemProcessing.Text = "List Item Processing";
            // 
            // rdoItemLevelFalse
            // 
            this.rdoItemLevelFalse.AutoSize = true;
            this.rdoItemLevelFalse.Checked = true;
            this.rdoItemLevelFalse.Location = new System.Drawing.Point(18, 43);
            this.rdoItemLevelFalse.Name = "rdoItemLevelFalse";
            this.rdoItemLevelFalse.Size = new System.Drawing.Size(122, 17);
            this.rdoItemLevelFalse.TabIndex = 1;
            this.rdoItemLevelFalse.TabStop = true;
            this.rdoItemLevelFalse.Text = "Do not load list items";
            this.rdoItemLevelFalse.UseVisualStyleBackColor = true;
            // 
            // rdoItemLevelTrue
            // 
            this.rdoItemLevelTrue.AutoSize = true;
            this.rdoItemLevelTrue.Location = new System.Drawing.Point(18, 19);
            this.rdoItemLevelTrue.Name = "rdoItemLevelTrue";
            this.rdoItemLevelTrue.Size = new System.Drawing.Size(91, 17);
            this.rdoItemLevelTrue.TabIndex = 0;
            this.rdoItemLevelTrue.Text = "Load list items";
            this.rdoItemLevelTrue.UseVisualStyleBackColor = true;
            this.rdoItemLevelTrue.CheckedChanged += new System.EventHandler(this.rdoItemLevelTrue_CheckedChanged);
            // 
            // bgwSiteDataWorker
            // 
            this.bgwSiteDataWorker.WorkerReportsProgress = true;
            this.bgwSiteDataWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSiteDataWorker_DoWork);
            this.bgwSiteDataWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSiteDataWorker_ProgressChanged);
            this.bgwSiteDataWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSiteDataWorker_RunWorkerCompleted);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "CSV Files|*.csv";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bgwPermissionWorker
            // 
            this.bgwPermissionWorker.WorkerReportsProgress = true;
            this.bgwPermissionWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPermissionWorker_DoWork);
            this.bgwPermissionWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSiteDataWorker_ProgressChanged);
            this.bgwPermissionWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwPermissionWorker_RunWorkerCompleted);
            // 
            // bgwLoadItemWorker
            // 
            this.bgwLoadItemWorker.WorkerReportsProgress = true;
            this.bgwLoadItemWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadItemWorker_DoWork);
            this.bgwLoadItemWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSiteDataWorker_ProgressChanged);
            this.bgwLoadItemWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoadItemWorker_RunWorkerCompleted);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::PermissionReporting.Properties.Resources.DELETE;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(425, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Clear log");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // PermissionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 751);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PermissionReport";
            this.Text = "Permission Reporting";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFilterButton)).EndInit();
            this.contextMenuTreeView.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageOutputLog.ResumeLayout(false);
            this.tabPageOutputLog.PerformLayout();
            this.tabPageDetails.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxListItemLevel.ResumeLayout(false);
            this.groupBoxListItemLevel.PerformLayout();
            this.groupBoxLoadDepth.ResumeLayout(false);
            this.groupBoxLoadDepth.PerformLayout();
            this.groupBoxListItemProcessing.ResumeLayout(false);
            this.groupBoxListItemProcessing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TreeView tvSiteTree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxListItemProcessing;
        private System.Windows.Forms.RadioButton rdoItemLevelFalse;
        private System.Windows.Forms.RadioButton rdoItemLevelTrue;
        private System.Windows.Forms.Button btnPopulateSiteStructure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSiteCollectionUrl;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleReprotsToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwSiteDataWorker;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadPermission;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.TabPage tabPageOutputLog;
        private System.Windows.Forms.TextBox txtOutputLog;
        private System.Windows.Forms.GroupBox groupBoxLoadDepth;
        private System.Windows.Forms.RadioButton rdoLoadStructureFull;
        private System.Windows.Forms.RadioButton rdoLoadStructurePartial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveReport;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadReport;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvMembership;
        private System.Windows.Forms.TreeView tvPermission;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.ComponentModel.BackgroundWorker bgwPermissionWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadFirstLevelItems;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadAllItems;
        private System.Windows.Forms.PictureBox picFilterButton;
        private System.Windows.Forms.GroupBox groupBoxListItemLevel;
        private System.Windows.Forms.RadioButton rdoLoadAllItems;
        private System.Windows.Forms.RadioButton rdoLoadFirstLevel;
        private System.ComponentModel.BackgroundWorker bgwLoadItemWorker;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}