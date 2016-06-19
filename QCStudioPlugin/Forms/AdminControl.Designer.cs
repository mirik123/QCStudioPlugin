namespace QuantConnect.QCStudioPlugin.Forms
{
    partial class AdminControl
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
            this.BacktestMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RefreshBacktestsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadBacktestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteBacktestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveLocallyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RefreshProjectsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CompileProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectProjectIDMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectProjectIDMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgrBacktests = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgrProjects = new System.Windows.Forms.DataGridView();
            this.BacktestMenu.SuspendLayout();
            this.ProjectsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrBacktests)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // BacktestMenu
            // 
            this.BacktestMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshBacktestsMenu,
            this.LoadBacktestMenu,
            this.DeleteBacktestMenu,
            this.SaveLocallyMenu});
            this.BacktestMenu.Name = "BacktestMenu";
            this.BacktestMenu.Size = new System.Drawing.Size(188, 114);
            this.BacktestMenu.Opening += new System.ComponentModel.CancelEventHandler(this.BacktestMenu_Opening);
            this.BacktestMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.BacktestMenu_Click);
            // 
            // RefreshBacktestsMenu
            // 
            this.RefreshBacktestsMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Reload;
            this.RefreshBacktestsMenu.Name = "RefreshBacktestsMenu";
            this.RefreshBacktestsMenu.Size = new System.Drawing.Size(187, 22);
            this.RefreshBacktestsMenu.Tag = "1";
            this.RefreshBacktestsMenu.Text = "Refresh";
            // 
            // LoadBacktestMenu
            // 
            this.LoadBacktestMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Open;
            this.LoadBacktestMenu.Name = "LoadBacktestMenu";
            this.LoadBacktestMenu.Size = new System.Drawing.Size(187, 22);
            this.LoadBacktestMenu.Tag = "0";
            this.LoadBacktestMenu.Text = "Load Backtest Results";
            // 
            // DeleteBacktestMenu
            // 
            this.DeleteBacktestMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Garbage_Closed;
            this.DeleteBacktestMenu.Name = "DeleteBacktestMenu";
            this.DeleteBacktestMenu.Size = new System.Drawing.Size(187, 22);
            this.DeleteBacktestMenu.Tag = "0";
            this.DeleteBacktestMenu.Text = "Delete Backtest";
            // 
            // SaveLocallyMenu
            // 
            this.SaveLocallyMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Save;
            this.SaveLocallyMenu.Name = "SaveLocallyMenu";
            this.SaveLocallyMenu.Size = new System.Drawing.Size(187, 22);
            this.SaveLocallyMenu.Tag = "0";
            this.SaveLocallyMenu.Text = "Save Locally";
            // 
            // ProjectsMenu
            // 
            this.ProjectsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshProjectsMenu,
            this.CreateProjectMenu,
            this.DeleteProjectMenu,
            this.UploadProjectMenu,
            this.DownloadProjectMenu,
            this.CompileProjectMenu,
            this.ConnectProjectIDMenu,
            this.DisconnectProjectIDMenu,
            this.LoginMenu,
            this.LogoutMenu});
            this.ProjectsMenu.Name = "ProjectsMenu";
            this.ProjectsMenu.Size = new System.Drawing.Size(188, 224);
            this.ProjectsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ProjectsMenu_Opening);
            this.ProjectsMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ProjectsMenu_Click);
            // 
            // RefreshProjectsMenu
            // 
            this.RefreshProjectsMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Reload;
            this.RefreshProjectsMenu.Name = "RefreshProjectsMenu";
            this.RefreshProjectsMenu.Size = new System.Drawing.Size(187, 22);
            this.RefreshProjectsMenu.Tag = "1";
            this.RefreshProjectsMenu.Text = "Refresh";
            // 
            // CreateProjectMenu
            // 
            this.CreateProjectMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Open;
            this.CreateProjectMenu.Name = "CreateProjectMenu";
            this.CreateProjectMenu.Size = new System.Drawing.Size(187, 22);
            this.CreateProjectMenu.Tag = "1";
            this.CreateProjectMenu.Text = "Create Project";
            // 
            // DeleteProjectMenu
            // 
            this.DeleteProjectMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Garbage_Closed;
            this.DeleteProjectMenu.Name = "DeleteProjectMenu";
            this.DeleteProjectMenu.Size = new System.Drawing.Size(187, 22);
            this.DeleteProjectMenu.Tag = "0";
            this.DeleteProjectMenu.Text = "Delete Project";
            // 
            // UploadProjectMenu
            // 
            this.UploadProjectMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Save;
            this.UploadProjectMenu.Name = "UploadProjectMenu";
            this.UploadProjectMenu.Size = new System.Drawing.Size(187, 22);
            this.UploadProjectMenu.Tag = "0";
            this.UploadProjectMenu.Text = "Upload Project";
            // 
            // DownloadProjectMenu
            // 
            this.DownloadProjectMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Open;
            this.DownloadProjectMenu.Name = "DownloadProjectMenu";
            this.DownloadProjectMenu.Size = new System.Drawing.Size(187, 22);
            this.DownloadProjectMenu.Tag = "0";
            this.DownloadProjectMenu.Text = "Download Project";
            // 
            // CompileProjectMenu
            // 
            this.CompileProjectMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Graph_03;
            this.CompileProjectMenu.Name = "CompileProjectMenu";
            this.CompileProjectMenu.Size = new System.Drawing.Size(187, 22);
            this.CompileProjectMenu.Tag = "0";
            this.CompileProjectMenu.Text = "Build Project";
            // 
            // ConnectProjectIDMenu
            // 
            this.ConnectProjectIDMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Key;
            this.ConnectProjectIDMenu.Name = "ConnectProjectIDMenu";
            this.ConnectProjectIDMenu.Size = new System.Drawing.Size(187, 22);
            this.ConnectProjectIDMenu.Tag = "0";
            this.ConnectProjectIDMenu.Text = "Connect Project ID";
            // 
            // DisconnectProjectIDMenu
            // 
            this.DisconnectProjectIDMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.fork;
            this.DisconnectProjectIDMenu.Name = "DisconnectProjectIDMenu";
            this.DisconnectProjectIDMenu.Size = new System.Drawing.Size(187, 22);
            this.DisconnectProjectIDMenu.Tag = "1";
            this.DisconnectProjectIDMenu.Text = "Disconnect Project ID";
            // 
            // LoginMenu
            // 
            this.LoginMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.User_Login;
            this.LoginMenu.Name = "LoginMenu";
            this.LoginMenu.Size = new System.Drawing.Size(187, 22);
            this.LoginMenu.Tag = "1";
            this.LoginMenu.Text = "Login";
            // 
            // LogoutMenu
            // 
            this.LogoutMenu.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Logout;
            this.LogoutMenu.Name = "LogoutMenu";
            this.LogoutMenu.Size = new System.Drawing.Size(187, 22);
            this.LogoutMenu.Tag = "1";
            this.LogoutMenu.Text = "Logout";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(1394, 292);
            this.splitContainer3.SplitterDistance = 769;
            this.splitContainer3.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.dgrBacktests);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(769, 292);
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
            this.dgrBacktests.ContextMenuStrip = this.BacktestMenu;
            this.dgrBacktests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrBacktests.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrBacktests.Location = new System.Drawing.Point(3, 16);
            this.dgrBacktests.MultiSelect = false;
            this.dgrBacktests.Name = "dgrBacktests";
            this.dgrBacktests.ReadOnly = true;
            this.dgrBacktests.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrBacktests.RowHeadersVisible = false;
            this.dgrBacktests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrBacktests.Size = new System.Drawing.Size(763, 273);
            this.dgrBacktests.TabIndex = 4;
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
            this.groupBox2.Size = new System.Drawing.Size(621, 292);
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
            this.dgrProjects.ContextMenuStrip = this.ProjectsMenu;
            this.dgrProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrProjects.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrProjects.Location = new System.Drawing.Point(3, 16);
            this.dgrProjects.MultiSelect = false;
            this.dgrProjects.Name = "dgrProjects";
            this.dgrProjects.ReadOnly = true;
            this.dgrProjects.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrProjects.RowHeadersVisible = false;
            this.dgrProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrProjects.Size = new System.Drawing.Size(615, 273);
            this.dgrProjects.TabIndex = 7;
            this.dgrProjects.DoubleClick += new System.EventHandler(this.dgrProjects_DoubleClick);
            // 
            // AdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer3);
            this.Name = "AdminControl";
            this.Size = new System.Drawing.Size(1394, 292);
            this.BacktestMenu.ResumeLayout(false);
            this.ProjectsMenu.ResumeLayout(false);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip BacktestMenu;
        private System.Windows.Forms.ToolStripMenuItem RefreshBacktestsMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteBacktestMenu;
        private System.Windows.Forms.ContextMenuStrip ProjectsMenu;
        private System.Windows.Forms.ToolStripMenuItem RefreshProjectsMenu;
        private System.Windows.Forms.ToolStripMenuItem CreateProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem UploadProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem DownloadProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem CompileProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem ConnectProjectIDMenu;
        private System.Windows.Forms.ToolStripMenuItem LoginMenu;
        private System.Windows.Forms.ToolStripMenuItem LogoutMenu;
        private System.Windows.Forms.ToolStripMenuItem DisconnectProjectIDMenu;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgrBacktests;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgrProjects;
        private System.Windows.Forms.ToolStripMenuItem LoadBacktestMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveLocallyMenu;
    }
}
