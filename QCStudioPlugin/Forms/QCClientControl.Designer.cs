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
            this.Processing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Requested = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SparkLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnBacktest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnRefreshBacktests = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLoadBacktest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDeleteBacktest = new System.Windows.Forms.ToolStripMenuItem();
            this.tabProjects = new System.Windows.Forms.TabPage();
            this.dgrProjects = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CloudProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mnProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnRefreshProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCreateProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDeleteProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnUploadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDownloadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCompileProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnUseProjectID = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.Progress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refreshBacktest = new System.Windows.Forms.Timer(this.components);
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgrBacktests = new System.Windows.Forms.DataGridView();
            this.BacktestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BacktestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tabTrades = new System.Windows.Forms.TabPage();
            this.dataGridViewTrades = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabBacktests = new System.Windows.Forms.TabPage();
            this.tabOutput.SuspendLayout();
            this.mnBacktest.SuspendLayout();
            this.tabProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProjects)).BeginInit();
            this.mnProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrBacktests)).BeginInit();
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
            this.tabTrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrades)).BeginInit();
            this.tabBacktests.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.rchOutputWnd);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(1151, 237);
            this.tabOutput.TabIndex = 3;
            this.tabOutput.Text = "Output Window";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // rchOutputWnd
            // 
            this.rchOutputWnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchOutputWnd.Location = new System.Drawing.Point(3, 3);
            this.rchOutputWnd.Name = "rchOutputWnd";
            this.rchOutputWnd.Size = new System.Drawing.Size(1145, 231);
            this.rchOutputWnd.TabIndex = 0;
            this.rchOutputWnd.Text = "";
            // 
            // Processing
            // 
            this.Processing.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Processing.HeaderText = "Processing Time";
            this.Processing.Name = "Processing";
            this.Processing.ReadOnly = true;
            // 
            // Requested
            // 
            this.Requested.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Requested.HeaderText = "Last Requested";
            this.Requested.Name = "Requested";
            this.Requested.ReadOnly = true;
            // 
            // SparkLine
            // 
            this.SparkLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SparkLine.HeaderText = "Spark Line";
            this.SparkLine.Name = "SparkLine";
            this.SparkLine.ReadOnly = true;
            // 
            // mnBacktest
            // 
            this.mnBacktest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnRefreshBacktests,
            this.mnLoadBacktest,
            this.mnDeleteBacktest});
            this.mnBacktest.Name = "mnBacktest";
            this.mnBacktest.Size = new System.Drawing.Size(155, 70);
            this.mnBacktest.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnBacktest_Click);
            // 
            // mnRefreshBacktests
            // 
            this.mnRefreshBacktests.Name = "mnRefreshBacktests";
            this.mnRefreshBacktests.Size = new System.Drawing.Size(154, 22);
            this.mnRefreshBacktests.Text = "Refresh";
            // 
            // mnLoadBacktest
            // 
            this.mnLoadBacktest.Name = "mnLoadBacktest";
            this.mnLoadBacktest.Size = new System.Drawing.Size(154, 22);
            this.mnLoadBacktest.Text = "Load Backtest";
            // 
            // mnDeleteBacktest
            // 
            this.mnDeleteBacktest.Name = "mnDeleteBacktest";
            this.mnDeleteBacktest.Size = new System.Drawing.Size(154, 22);
            this.mnDeleteBacktest.Text = "Delete Backtest";
            // 
            // tabProjects
            // 
            this.tabProjects.Controls.Add(this.dgrProjects);
            this.tabProjects.Location = new System.Drawing.Point(4, 22);
            this.tabProjects.Name = "tabProjects";
            this.tabProjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabProjects.Size = new System.Drawing.Size(1151, 237);
            this.tabProjects.TabIndex = 2;
            this.tabProjects.Text = "Projects";
            this.tabProjects.UseVisualStyleBackColor = true;
            // 
            // dgrProjects
            // 
            this.dgrProjects.AllowUserToAddRows = false;
            this.dgrProjects.AllowUserToDeleteRows = false;
            this.dgrProjects.AllowUserToOrderColumns = true;
            this.dgrProjects.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrProjects.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrProjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgrProjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.CloudProjectName,
            this.Modified,
            this.LocalProjectName});
            this.dgrProjects.ContextMenuStrip = this.mnProjects;
            this.dgrProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrProjects.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrProjects.Location = new System.Drawing.Point(3, 3);
            this.dgrProjects.MultiSelect = false;
            this.dgrProjects.Name = "dgrProjects";
            this.dgrProjects.ReadOnly = true;
            this.dgrProjects.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrProjects.RowHeadersVisible = false;
            this.dgrProjects.RowTemplate.ContextMenuStrip = this.mnProjects;
            this.dgrProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrProjects.Size = new System.Drawing.Size(1145, 231);
            this.dgrProjects.TabIndex = 4;
            this.dgrProjects.DoubleClick += new System.EventHandler(this.dgrProjects_DoubleClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // CloudProjectName
            // 
            this.CloudProjectName.HeaderText = "Cloud Project Name";
            this.CloudProjectName.Name = "CloudProjectName";
            this.CloudProjectName.ReadOnly = true;
            this.CloudProjectName.Width = 200;
            // 
            // Modified
            // 
            this.Modified.HeaderText = "Modified";
            this.Modified.Name = "Modified";
            this.Modified.ReadOnly = true;
            // 
            // LocalProjectName
            // 
            this.LocalProjectName.HeaderText = "Local Project Name";
            this.LocalProjectName.Name = "LocalProjectName";
            this.LocalProjectName.ReadOnly = true;
            this.LocalProjectName.Width = 200;
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
            this.mnUseProjectID,
            this.mnLogin,
            this.mnLogout});
            this.mnProjects.Name = "mnProjects";
            this.mnProjects.Size = new System.Drawing.Size(169, 224);
            this.mnProjects.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnProjects_Click);
            // 
            // mnRefreshProjects
            // 
            this.mnRefreshProjects.Name = "mnRefreshProjects";
            this.mnRefreshProjects.Size = new System.Drawing.Size(168, 22);
            this.mnRefreshProjects.Text = "Refresh";
            // 
            // mnCreateProject
            // 
            this.mnCreateProject.Name = "mnCreateProject";
            this.mnCreateProject.Size = new System.Drawing.Size(168, 22);
            this.mnCreateProject.Text = "Create Project";
            // 
            // mnDeleteProject
            // 
            this.mnDeleteProject.Name = "mnDeleteProject";
            this.mnDeleteProject.Size = new System.Drawing.Size(168, 22);
            this.mnDeleteProject.Text = "Delete Project";
            // 
            // mnUploadProject
            // 
            this.mnUploadProject.Name = "mnUploadProject";
            this.mnUploadProject.Size = new System.Drawing.Size(168, 22);
            this.mnUploadProject.Text = "Upload Project";
            // 
            // mnDownloadProject
            // 
            this.mnDownloadProject.Name = "mnDownloadProject";
            this.mnDownloadProject.Size = new System.Drawing.Size(168, 22);
            this.mnDownloadProject.Text = "Download Project";
            // 
            // mnCompileProject
            // 
            this.mnCompileProject.Name = "mnCompileProject";
            this.mnCompileProject.Size = new System.Drawing.Size(168, 22);
            this.mnCompileProject.Text = "Build Project";
            // 
            // mnUseProjectID
            // 
            this.mnUseProjectID.Name = "mnUseProjectID";
            this.mnUseProjectID.Size = new System.Drawing.Size(168, 22);
            this.mnUseProjectID.Text = "Use Project ID";
            // 
            // mnLogin
            // 
            this.mnLogin.Name = "mnLogin";
            this.mnLogin.Size = new System.Drawing.Size(168, 22);
            this.mnLogin.Text = "Login";
            // 
            // mnLogout
            // 
            this.mnLogout.Name = "mnLogout";
            this.mnLogout.Size = new System.Drawing.Size(168, 22);
            this.mnLogout.Text = "Logout";
            // 
            // Progress
            // 
            this.Progress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Progress.HeaderText = "Progress";
            this.Progress.Name = "Progress";
            this.Progress.ReadOnly = true;
            // 
            // refreshBacktest
            // 
            this.refreshBacktest.Interval = 2000;
            this.refreshBacktest.Tick += new System.EventHandler(this.refreshBacktest_Tick);
            // 
            // EndDate
            // 
            this.EndDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EndDate.HeaderText = "End Date";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            // 
            // dgrBacktests
            // 
            this.dgrBacktests.AllowUserToAddRows = false;
            this.dgrBacktests.AllowUserToDeleteRows = false;
            this.dgrBacktests.AllowUserToOrderColumns = true;
            this.dgrBacktests.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrBacktests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrBacktests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgrBacktests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrBacktests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrBacktests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BacktestId,
            this.BacktestName,
            this.StartDate,
            this.EndDate,
            this.Progress,
            this.Processing,
            this.Requested,
            this.SparkLine});
            this.dgrBacktests.ContextMenuStrip = this.mnBacktest;
            this.dgrBacktests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrBacktests.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrBacktests.Location = new System.Drawing.Point(3, 3);
            this.dgrBacktests.MultiSelect = false;
            this.dgrBacktests.Name = "dgrBacktests";
            this.dgrBacktests.ReadOnly = true;
            this.dgrBacktests.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrBacktests.RowHeadersVisible = false;
            this.dgrBacktests.RowTemplate.ContextMenuStrip = this.mnBacktest;
            this.dgrBacktests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrBacktests.Size = new System.Drawing.Size(1145, 231);
            this.dgrBacktests.TabIndex = 3;
            this.dgrBacktests.DoubleClick += new System.EventHandler(this.dgrBacktests_DoubleClick);
            // 
            // BacktestId
            // 
            this.BacktestId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BacktestId.HeaderText = "BacktestId";
            this.BacktestId.Name = "BacktestId";
            this.BacktestId.ReadOnly = true;
            this.BacktestId.Visible = false;
            // 
            // BacktestName
            // 
            this.BacktestName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BacktestName.HeaderText = "Name";
            this.BacktestName.Name = "BacktestName";
            this.BacktestName.ReadOnly = true;
            // 
            // StartDate
            // 
            this.StartDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StartDate.HeaderText = "Start Date";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 670);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1159, 22);
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
            this.splitContainer2.Size = new System.Drawing.Size(1159, 692);
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
            this.splitContainer1.Size = new System.Drawing.Size(1159, 425);
            this.splitContainer1.SplitterDistance = 915;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupChart
            // 
            this.groupChart.Controls.Add(this.tabCharts);
            this.groupChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChart.Location = new System.Drawing.Point(0, 0);
            this.groupChart.Name = "groupChart";
            this.groupChart.Size = new System.Drawing.Size(915, 425);
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
            this.tabCharts.Size = new System.Drawing.Size(909, 406);
            this.tabCharts.TabIndex = 0;
            // 
            // groupChartOptions
            // 
            this.groupChartOptions.Controls.Add(this.dataGridViewStats);
            this.groupChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChartOptions.Location = new System.Drawing.Point(0, 0);
            this.groupChartOptions.Name = "groupChartOptions";
            this.groupChartOptions.Size = new System.Drawing.Size(240, 425);
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
            this.dataGridViewStats.Size = new System.Drawing.Size(234, 406);
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
            this.tabFooter.Controls.Add(this.tabTrades);
            this.tabFooter.Controls.Add(this.tabBacktests);
            this.tabFooter.Controls.Add(this.tabProjects);
            this.tabFooter.Controls.Add(this.tabOutput);
            this.tabFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFooter.Location = new System.Drawing.Point(0, 0);
            this.tabFooter.Name = "tabFooter";
            this.tabFooter.SelectedIndex = 0;
            this.tabFooter.Size = new System.Drawing.Size(1159, 263);
            this.tabFooter.TabIndex = 0;
            // 
            // tabTrades
            // 
            this.tabTrades.Controls.Add(this.dataGridViewTrades);
            this.tabTrades.Location = new System.Drawing.Point(4, 22);
            this.tabTrades.Name = "tabTrades";
            this.tabTrades.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrades.Size = new System.Drawing.Size(1151, 237);
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
            this.dataGridViewTrades.Size = new System.Drawing.Size(1145, 231);
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
            // tabBacktests
            // 
            this.tabBacktests.Controls.Add(this.dgrBacktests);
            this.tabBacktests.Location = new System.Drawing.Point(4, 22);
            this.tabBacktests.Name = "tabBacktests";
            this.tabBacktests.Padding = new System.Windows.Forms.Padding(3);
            this.tabBacktests.Size = new System.Drawing.Size(1151, 237);
            this.tabBacktests.TabIndex = 1;
            this.tabBacktests.Text = "Backtests";
            this.tabBacktests.UseVisualStyleBackColor = true;
            // 
            // QCClientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer2);
            this.Name = "QCClientControl";
            this.Size = new System.Drawing.Size(1159, 692);
            this.tabOutput.ResumeLayout(false);
            this.mnBacktest.ResumeLayout(false);
            this.tabProjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrProjects)).EndInit();
            this.mnProjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrBacktests)).EndInit();
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
            this.tabTrades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrades)).EndInit();
            this.tabBacktests.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn Processing;
        private System.Windows.Forms.DataGridViewTextBoxColumn Requested;
        private System.Windows.Forms.DataGridViewTextBoxColumn SparkLine;
        private System.Windows.Forms.ContextMenuStrip mnBacktest;
        private System.Windows.Forms.ToolStripMenuItem mnRefreshBacktests;
        private System.Windows.Forms.ToolStripMenuItem mnLoadBacktest;
        private System.Windows.Forms.ToolStripMenuItem mnDeleteBacktest;
        private System.Windows.Forms.TabPage tabProjects;
        private System.Windows.Forms.DataGridView dgrProjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.ContextMenuStrip mnProjects;
        private System.Windows.Forms.ToolStripMenuItem mnRefreshProjects;
        private System.Windows.Forms.ToolStripMenuItem mnCreateProject;
        private System.Windows.Forms.ToolStripMenuItem mnDeleteProject;
        private System.Windows.Forms.ToolStripMenuItem mnUploadProject;
        private System.Windows.Forms.ToolStripMenuItem mnDownloadProject;
        private System.Windows.Forms.ToolStripMenuItem mnCompileProject;
        private System.Windows.Forms.ToolStripMenuItem mnUseProjectID;
        private System.Windows.Forms.ToolStripMenuItem mnLogin;
        private System.Windows.Forms.ToolStripMenuItem mnLogout;
        private System.Windows.Forms.DataGridViewTextBoxColumn Progress;
        private System.Windows.Forms.Timer refreshBacktest;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridView dgrBacktests;
        private System.Windows.Forms.DataGridViewTextBoxColumn BacktestId;
        private System.Windows.Forms.DataGridViewTextBoxColumn BacktestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
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
        private System.Windows.Forms.TabPage tabBacktests;
        private System.Windows.Forms.RichTextBox rchOutputWnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CloudProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modified;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalProjectName;
    }
}
