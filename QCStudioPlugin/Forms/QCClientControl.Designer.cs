namespace QuantConnect.QCStudioPlugin.Forms
{
    partial class QCClientControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.rchOutputWnd = new System.Windows.Forms.RichTextBox();
            this.mnBacktest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnRefreshBacktests = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLoadBacktest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDeleteBacktest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnRefreshProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCreateProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDeleteProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnUploadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDownloadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCompileProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnConnectProjectID = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDisconnectProjectID = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshBacktest = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupChart = new System.Windows.Forms.GroupBox();
            this.tabCharts = new System.Windows.Forms.TabControl();
            this.groupChartOptions = new System.Windows.Forms.GroupBox();
            this.dataGridViewStats = new System.Windows.Forms.DataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFooter = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgrBacktests = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgrProjects = new System.Windows.Forms.DataGridView();
            this.tabTrades = new System.Windows.Forms.TabPage();
            this.dataGridViewTrades = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabOutput.SuspendLayout();
            this.mnBacktest.SuspendLayout();
            this.mnProjects.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupChart.SuspendLayout();
            this.groupChartOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStats)).BeginInit();
            this.tabFooter.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrBacktests)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProjects)).BeginInit();
            this.tabTrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrades)).BeginInit();
            this.SuspendLayout();
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.rchOutputWnd);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(1386, 237);
            this.tabOutput.TabIndex = 3;
            this.tabOutput.Text = "Output Window";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // rchOutputWnd
            // 
            this.rchOutputWnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchOutputWnd.Location = new System.Drawing.Point(3, 3);
            this.rchOutputWnd.Name = "rchOutputWnd";
            this.rchOutputWnd.Size = new System.Drawing.Size(1380, 231);
            this.rchOutputWnd.TabIndex = 0;
            this.rchOutputWnd.Text = "";
            // 
            // mnBacktest
            // 
            this.mnBacktest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnRefreshBacktests,
            this.mnLoadBacktest,
            this.mnDeleteBacktest});
            this.mnBacktest.Name = "mnBacktest";
            this.mnBacktest.Size = new System.Drawing.Size(155, 70);
            this.mnBacktest.Opening += new System.ComponentModel.CancelEventHandler(this.mnBacktest_Opening);
            this.mnBacktest.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnBacktest_Click);
            // 
            // mnRefreshBacktests
            // 
            this.mnRefreshBacktests.Image = global::Company.QCStudioPlugin.Properties.Resources.Reload;
            this.mnRefreshBacktests.Name = "mnRefreshBacktests";
            this.mnRefreshBacktests.Size = new System.Drawing.Size(154, 22);
            this.mnRefreshBacktests.Tag = "1";
            this.mnRefreshBacktests.Text = "Refresh";
            // 
            // mnLoadBacktest
            // 
            this.mnLoadBacktest.Image = global::Company.QCStudioPlugin.Properties.Resources.Open;
            this.mnLoadBacktest.Name = "mnLoadBacktest";
            this.mnLoadBacktest.Size = new System.Drawing.Size(154, 22);
            this.mnLoadBacktest.Tag = "0";
            this.mnLoadBacktest.Text = "Load Backtest";
            // 
            // mnDeleteBacktest
            // 
            this.mnDeleteBacktest.Image = global::Company.QCStudioPlugin.Properties.Resources.Garbage_Closed;
            this.mnDeleteBacktest.Name = "mnDeleteBacktest";
            this.mnDeleteBacktest.Size = new System.Drawing.Size(154, 22);
            this.mnDeleteBacktest.Tag = "0";
            this.mnDeleteBacktest.Text = "Delete Backtest";
            // 
            // mnProjects
            // 
            this.mnProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnRefreshProjects,
            this.mnCreateProject,
            this.mnDeleteProject,
            this.mnUploadProject,
            this.mnDownloadProject,
            this.mnCompileProject,
            this.mnConnectProjectID,
            this.mnDisconnectProjectID,
            this.mnLogin,
            this.mnLogout});
            this.mnProjects.Name = "mnProjects";
            this.mnProjects.Size = new System.Drawing.Size(188, 246);
            this.mnProjects.Opening += new System.ComponentModel.CancelEventHandler(this.mnProjects_Opening);
            this.mnProjects.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnProjects_Click);
            // 
            // mnRefreshProjects
            // 
            this.mnRefreshProjects.Image = global::Company.QCStudioPlugin.Properties.Resources.Reload;
            this.mnRefreshProjects.Name = "mnRefreshProjects";
            this.mnRefreshProjects.Size = new System.Drawing.Size(187, 22);
            this.mnRefreshProjects.Tag = "1";
            this.mnRefreshProjects.Text = "Refresh";
            // 
            // mnCreateProject
            // 
            this.mnCreateProject.Image = global::Company.QCStudioPlugin.Properties.Resources.Open;
            this.mnCreateProject.Name = "mnCreateProject";
            this.mnCreateProject.Size = new System.Drawing.Size(187, 22);
            this.mnCreateProject.Tag = "1";
            this.mnCreateProject.Text = "Create Project";
            // 
            // mnDeleteProject
            // 
            this.mnDeleteProject.Image = global::Company.QCStudioPlugin.Properties.Resources.Garbage_Closed;
            this.mnDeleteProject.Name = "mnDeleteProject";
            this.mnDeleteProject.Size = new System.Drawing.Size(187, 22);
            this.mnDeleteProject.Tag = "0";
            this.mnDeleteProject.Text = "Delete Project";
            // 
            // mnUploadProject
            // 
            this.mnUploadProject.Image = global::Company.QCStudioPlugin.Properties.Resources.Save;
            this.mnUploadProject.Name = "mnUploadProject";
            this.mnUploadProject.Size = new System.Drawing.Size(187, 22);
            this.mnUploadProject.Tag = "0";
            this.mnUploadProject.Text = "Upload Project";
            // 
            // mnDownloadProject
            // 
            this.mnDownloadProject.Image = global::Company.QCStudioPlugin.Properties.Resources.Open;
            this.mnDownloadProject.Name = "mnDownloadProject";
            this.mnDownloadProject.Size = new System.Drawing.Size(187, 22);
            this.mnDownloadProject.Tag = "0";
            this.mnDownloadProject.Text = "Download Project";
            // 
            // mnCompileProject
            // 
            this.mnCompileProject.Image = global::Company.QCStudioPlugin.Properties.Resources.Graph_03;
            this.mnCompileProject.Name = "mnCompileProject";
            this.mnCompileProject.Size = new System.Drawing.Size(187, 22);
            this.mnCompileProject.Tag = "0";
            this.mnCompileProject.Text = "Build Project";
            // 
            // mnConnectProjectID
            // 
            this.mnConnectProjectID.Image = global::Company.QCStudioPlugin.Properties.Resources.Key;
            this.mnConnectProjectID.Name = "mnConnectProjectID";
            this.mnConnectProjectID.Size = new System.Drawing.Size(187, 22);
            this.mnConnectProjectID.Tag = "0";
            this.mnConnectProjectID.Text = "Connect Project ID";
            // 
            // mnDisconnectProjectID
            // 
            this.mnDisconnectProjectID.Image = global::Company.QCStudioPlugin.Properties.Resources.fork;
            this.mnDisconnectProjectID.Name = "mnDisconnectProjectID";
            this.mnDisconnectProjectID.Size = new System.Drawing.Size(187, 22);
            this.mnDisconnectProjectID.Tag = "1";
            this.mnDisconnectProjectID.Text = "Disconnect Project ID";
            // 
            // mnLogin
            // 
            this.mnLogin.Image = global::Company.QCStudioPlugin.Properties.Resources.User_Login;
            this.mnLogin.Name = "mnLogin";
            this.mnLogin.Size = new System.Drawing.Size(187, 22);
            this.mnLogin.Tag = "1";
            this.mnLogin.Text = "Login";
            // 
            // mnLogout
            // 
            this.mnLogout.Image = global::Company.QCStudioPlugin.Properties.Resources.Logout;
            this.mnLogout.Name = "mnLogout";
            this.mnLogout.Size = new System.Drawing.Size(187, 22);
            this.mnLogout.Tag = "1";
            this.mnLogout.Text = "Logout";
            // 
            // refreshBacktest
            // 
            this.refreshBacktest.Interval = 2000;
            this.refreshBacktest.Tick += new System.EventHandler(this.refreshBacktest_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 670);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1394, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(147, 17);
            this.statusLabel.Text = "Downloading first results...";
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.tabFooter);
            this.splitContainer2.Panel2MinSize = 250;
            this.splitContainer2.Size = new System.Drawing.Size(1394, 692);
            this.splitContainer2.SplitterDistance = 425;
            this.splitContainer2.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupChart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupChartOptions);
            this.splitContainer1.Size = new System.Drawing.Size(1394, 425);
            this.splitContainer1.SplitterDistance = 1100;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupChart
            // 
            this.groupChart.Controls.Add(this.tabCharts);
            this.groupChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChart.Location = new System.Drawing.Point(0, 0);
            this.groupChart.Name = "groupChart";
            this.groupChart.Size = new System.Drawing.Size(1100, 425);
            this.groupChart.TabIndex = 0;
            this.groupChart.TabStop = false;
            this.groupChart.Text = "Backtest Results";
            // 
            // tabCharts
            // 
            this.tabCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCharts.Location = new System.Drawing.Point(3, 16);
            this.tabCharts.Name = "tabCharts";
            this.tabCharts.SelectedIndex = 0;
            this.tabCharts.Size = new System.Drawing.Size(1094, 406);
            this.tabCharts.TabIndex = 0;
            // 
            // groupChartOptions
            // 
            this.groupChartOptions.Controls.Add(this.dataGridViewStats);
            this.groupChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChartOptions.Location = new System.Drawing.Point(0, 0);
            this.groupChartOptions.Name = "groupChartOptions";
            this.groupChartOptions.Size = new System.Drawing.Size(290, 425);
            this.groupChartOptions.TabIndex = 0;
            this.groupChartOptions.TabStop = false;
            // 
            // dataGridViewStats
            // 
            this.dataGridViewStats.AllowUserToAddRows = false;
            this.dataGridViewStats.AllowUserToDeleteRows = false;
            this.dataGridViewStats.AllowUserToOrderColumns = true;
            this.dataGridViewStats.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewStats.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewStats.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridViewStats.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName,
            this.columnValue});
            this.dataGridViewStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStats.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridViewStats.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewStats.Name = "dataGridViewStats";
            this.dataGridViewStats.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewStats.RowHeadersVisible = false;
            this.dataGridViewStats.Size = new System.Drawing.Size(284, 406);
            this.dataGridViewStats.TabIndex = 2;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnName.DefaultCellStyle = dataGridViewCellStyle3;
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            // 
            // columnValue
            // 
            this.columnValue.HeaderText = "Value";
            this.columnValue.Name = "columnValue";
            this.columnValue.Width = 90;
            // 
            // tabFooter
            // 
            this.tabFooter.Controls.Add(this.tabPage1);
            this.tabFooter.Controls.Add(this.tabTrades);
            this.tabFooter.Controls.Add(this.tabOutput);
            this.tabFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFooter.Location = new System.Drawing.Point(0, 0);
            this.tabFooter.Name = "tabFooter";
            this.tabFooter.SelectedIndex = 0;
            this.tabFooter.Size = new System.Drawing.Size(1394, 263);
            this.tabFooter.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1386, 237);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Projects/Backtests";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(1380, 231);
            this.splitContainer3.SplitterDistance = 762;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.dgrBacktests);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backtests";
            // 
            // dgrBacktests
            // 
            this.dgrBacktests.AllowUserToAddRows = false;
            this.dgrBacktests.AllowUserToDeleteRows = false;
            this.dgrBacktests.AllowUserToOrderColumns = true;
            this.dgrBacktests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrBacktests.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrBacktests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrBacktests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgrBacktests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrBacktests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrBacktests.ContextMenuStrip = this.mnBacktest;
            this.dgrBacktests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrBacktests.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrBacktests.Location = new System.Drawing.Point(3, 16);
            this.dgrBacktests.MultiSelect = false;
            this.dgrBacktests.Name = "dgrBacktests";
            this.dgrBacktests.ReadOnly = true;
            this.dgrBacktests.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrBacktests.RowHeadersVisible = false;
            this.dgrBacktests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrBacktests.Size = new System.Drawing.Size(756, 212);
            this.dgrBacktests.TabIndex = 4;
            this.dgrBacktests.SelectionChanged += new System.EventHandler(this.dgrBacktests_SelectionChanged);
            this.dgrBacktests.DoubleClick += new System.EventHandler(this.dgrBacktests_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.dgrProjects);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(614, 231);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Projects";
            // 
            // dgrProjects
            // 
            this.dgrProjects.AllowUserToAddRows = false;
            this.dgrProjects.AllowUserToDeleteRows = false;
            this.dgrProjects.AllowUserToOrderColumns = true;
            this.dgrProjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrProjects.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrProjects.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrProjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgrProjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrProjects.ContextMenuStrip = this.mnProjects;
            this.dgrProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrProjects.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrProjects.Location = new System.Drawing.Point(3, 16);
            this.dgrProjects.MultiSelect = false;
            this.dgrProjects.Name = "dgrProjects";
            this.dgrProjects.ReadOnly = true;
            this.dgrProjects.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrProjects.RowHeadersVisible = false;
            this.dgrProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrProjects.Size = new System.Drawing.Size(608, 212);
            this.dgrProjects.TabIndex = 7;
            this.dgrProjects.SelectionChanged += new System.EventHandler(this.dgrProjects_SelectionChanged);
            this.dgrProjects.DoubleClick += new System.EventHandler(this.dgrProjects_DoubleClick);
            // 
            // tabTrades
            // 
            this.tabTrades.Controls.Add(this.dataGridViewTrades);
            this.tabTrades.Location = new System.Drawing.Point(4, 22);
            this.tabTrades.Name = "tabTrades";
            this.tabTrades.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrades.Size = new System.Drawing.Size(1386, 237);
            this.tabTrades.TabIndex = 0;
            this.tabTrades.Text = "Trades";
            this.tabTrades.ToolTipText = "Backtest Trades";
            this.tabTrades.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTrades
            // 
            this.dataGridViewTrades.AllowUserToAddRows = false;
            this.dataGridViewTrades.AllowUserToDeleteRows = false;
            this.dataGridViewTrades.AllowUserToOrderColumns = true;
            this.dataGridViewTrades.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewTrades.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTrades.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridViewTrades.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewTrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTrades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Price,
            this.Type,
            this.Quantity,
            this.Operation,
            this.Status});
            this.dataGridViewTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTrades.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridViewTrades.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTrades.Name = "dataGridViewTrades";
            this.dataGridViewTrades.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewTrades.RowHeadersVisible = false;
            this.dataGridViewTrades.Size = new System.Drawing.Size(1380, 231);
            this.dataGridViewTrades.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "DateTime";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 110;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Symbol";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // Price
            // 
            this.Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            // 
            // Operation
            // 
            this.Operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Operation.HeaderText = "Operation";
            this.Operation.Name = "Operation";
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // QCClientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer2);
            this.Name = "QCClientControl";
            this.Size = new System.Drawing.Size(1394, 692);
            this.tabOutput.ResumeLayout(false);
            this.mnBacktest.ResumeLayout(false);
            this.mnProjects.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupChart.ResumeLayout(false);
            this.groupChartOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStats)).EndInit();
            this.tabFooter.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrBacktests)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrProjects)).EndInit();
            this.tabTrades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.ContextMenuStrip mnBacktest;
        private System.Windows.Forms.ToolStripMenuItem mnRefreshBacktests;
        private System.Windows.Forms.ToolStripMenuItem mnLoadBacktest;
        private System.Windows.Forms.ToolStripMenuItem mnDeleteBacktest;
        private System.Windows.Forms.ContextMenuStrip mnProjects;
        private System.Windows.Forms.ToolStripMenuItem mnRefreshProjects;
        private System.Windows.Forms.ToolStripMenuItem mnCreateProject;
        private System.Windows.Forms.ToolStripMenuItem mnDeleteProject;
        private System.Windows.Forms.ToolStripMenuItem mnUploadProject;
        private System.Windows.Forms.ToolStripMenuItem mnDownloadProject;
        private System.Windows.Forms.ToolStripMenuItem mnCompileProject;
        private System.Windows.Forms.ToolStripMenuItem mnConnectProjectID;
        private System.Windows.Forms.ToolStripMenuItem mnLogin;
        private System.Windows.Forms.ToolStripMenuItem mnLogout;
        private System.Windows.Forms.Timer refreshBacktest;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupChart;
        private System.Windows.Forms.TabControl tabCharts;
        private System.Windows.Forms.GroupBox groupChartOptions;
        private System.Windows.Forms.DataGridView dataGridViewStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
        private System.Windows.Forms.TabControl tabFooter;
        private System.Windows.Forms.TabPage tabTrades;
        private System.Windows.Forms.DataGridView dataGridViewTrades;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.RichTextBox rchOutputWnd;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgrBacktests;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgrProjects;
        private System.Windows.Forms.ToolStripMenuItem mnDisconnectProjectID;
    }
}
