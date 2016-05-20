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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgrBacktests = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgrProjects = new System.Windows.Forms.DataGridView();
            this.mnBacktest.SuspendLayout();
            this.mnProjects.SuspendLayout();
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
            this.mnRefreshBacktests.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Reload;
            this.mnRefreshBacktests.Name = "mnRefreshBacktests";
            this.mnRefreshBacktests.Size = new System.Drawing.Size(154, 22);
            this.mnRefreshBacktests.Tag = "1";
            this.mnRefreshBacktests.Text = "Refresh";
            // 
            // mnLoadBacktest
            // 
            this.mnLoadBacktest.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Open;
            this.mnLoadBacktest.Name = "mnLoadBacktest";
            this.mnLoadBacktest.Size = new System.Drawing.Size(154, 22);
            this.mnLoadBacktest.Tag = "0";
            this.mnLoadBacktest.Text = "Load Backtest";
            // 
            // mnDeleteBacktest
            // 
            this.mnDeleteBacktest.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Garbage_Closed;
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
            this.mnProjects.Size = new System.Drawing.Size(188, 224);
            this.mnProjects.Opening += new System.ComponentModel.CancelEventHandler(this.mnProjects_Opening);
            this.mnProjects.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnProjects_Click);
            // 
            // mnRefreshProjects
            // 
            this.mnRefreshProjects.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Reload;
            this.mnRefreshProjects.Name = "mnRefreshProjects";
            this.mnRefreshProjects.Size = new System.Drawing.Size(187, 22);
            this.mnRefreshProjects.Tag = "1";
            this.mnRefreshProjects.Text = "Refresh";
            // 
            // mnCreateProject
            // 
            this.mnCreateProject.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Open;
            this.mnCreateProject.Name = "mnCreateProject";
            this.mnCreateProject.Size = new System.Drawing.Size(187, 22);
            this.mnCreateProject.Tag = "1";
            this.mnCreateProject.Text = "Create Project";
            // 
            // mnDeleteProject
            // 
            this.mnDeleteProject.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Garbage_Closed;
            this.mnDeleteProject.Name = "mnDeleteProject";
            this.mnDeleteProject.Size = new System.Drawing.Size(187, 22);
            this.mnDeleteProject.Tag = "0";
            this.mnDeleteProject.Text = "Delete Project";
            // 
            // mnUploadProject
            // 
            this.mnUploadProject.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Save;
            this.mnUploadProject.Name = "mnUploadProject";
            this.mnUploadProject.Size = new System.Drawing.Size(187, 22);
            this.mnUploadProject.Tag = "0";
            this.mnUploadProject.Text = "Upload Project";
            // 
            // mnDownloadProject
            // 
            this.mnDownloadProject.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Open;
            this.mnDownloadProject.Name = "mnDownloadProject";
            this.mnDownloadProject.Size = new System.Drawing.Size(187, 22);
            this.mnDownloadProject.Tag = "0";
            this.mnDownloadProject.Text = "Download Project";
            // 
            // mnCompileProject
            // 
            this.mnCompileProject.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Graph_03;
            this.mnCompileProject.Name = "mnCompileProject";
            this.mnCompileProject.Size = new System.Drawing.Size(187, 22);
            this.mnCompileProject.Tag = "0";
            this.mnCompileProject.Text = "Build Project";
            // 
            // mnConnectProjectID
            // 
            this.mnConnectProjectID.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Key;
            this.mnConnectProjectID.Name = "mnConnectProjectID";
            this.mnConnectProjectID.Size = new System.Drawing.Size(187, 22);
            this.mnConnectProjectID.Tag = "0";
            this.mnConnectProjectID.Text = "Connect Project ID";
            // 
            // mnDisconnectProjectID
            // 
            this.mnDisconnectProjectID.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.fork;
            this.mnDisconnectProjectID.Name = "mnDisconnectProjectID";
            this.mnDisconnectProjectID.Size = new System.Drawing.Size(187, 22);
            this.mnDisconnectProjectID.Tag = "1";
            this.mnDisconnectProjectID.Text = "Disconnect Project ID";
            // 
            // mnLogin
            // 
            this.mnLogin.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.User_Login;
            this.mnLogin.Name = "mnLogin";
            this.mnLogin.Size = new System.Drawing.Size(187, 22);
            this.mnLogin.Tag = "1";
            this.mnLogin.Text = "Login";
            // 
            // mnLogout
            // 
            this.mnLogout.Image = global::QuantConnect.QCStudioPlugin.Properties.Resources.Logout;
            this.mnLogout.Name = "mnLogout";
            this.mnLogout.Size = new System.Drawing.Size(187, 22);
            this.mnLogout.Tag = "1";
            this.mnLogout.Text = "Logout";
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
            this.dgrBacktests.Size = new System.Drawing.Size(763, 273);
            this.dgrBacktests.TabIndex = 4;
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
            this.dgrProjects.Size = new System.Drawing.Size(615, 273);
            this.dgrProjects.TabIndex = 7;
            // 
            // AdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer3);
            this.Name = "AdminControl";
            this.Size = new System.Drawing.Size(1394, 292);
            this.mnBacktest.ResumeLayout(false);
            this.mnProjects.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem mnDisconnectProjectID;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgrBacktests;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgrProjects;
    }
}
