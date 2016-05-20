namespace QuantConnect.QCStudioPlugin.Forms
{
    partial class ChartControl
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
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FormToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FormToolStripStatusStringLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatisticsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FormToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.groupLog = new System.Windows.Forms.GroupBox();
            this.refreshBacktest = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.Panel2.SuspendLayout();
            this.splitPanel.SuspendLayout();
            this.groupLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogTextBox
            // 
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Location = new System.Drawing.Point(2, 15);
            this.LogTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.Size = new System.Drawing.Size(1038, 100);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FormToolStripStatusLabel,
            this.FormToolStripStatusStringLabel,
            this.StatisticsToolStripStatusLabel,
            this.FormToolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1046, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FormToolStripStatusLabel
            // 
            this.FormToolStripStatusLabel.Name = "FormToolStripStatusLabel";
            this.FormToolStripStatusLabel.Size = new System.Drawing.Size(105, 17);
            this.FormToolStripStatusLabel.Text = "Loading Complete";
            // 
            // FormToolStripStatusStringLabel
            // 
            this.FormToolStripStatusStringLabel.Name = "FormToolStripStatusStringLabel";
            this.FormToolStripStatusStringLabel.Size = new System.Drawing.Size(739, 17);
            this.FormToolStripStatusStringLabel.Spring = true;
            // 
            // StatisticsToolStripStatusLabel
            // 
            this.StatisticsToolStripStatusLabel.Name = "StatisticsToolStripStatusLabel";
            this.StatisticsToolStripStatusLabel.Size = new System.Drawing.Size(136, 17);
            this.StatisticsToolStripStatusLabel.Text = "Statistics: CPU:    Ram:    ";
            // 
            // FormToolStripProgressBar
            // 
            this.FormToolStripProgressBar.Name = "FormToolStripProgressBar";
            this.FormToolStripProgressBar.Size = new System.Drawing.Size(55, 16);
            // 
            // splitPanel
            // 
            this.splitPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanel.Location = new System.Drawing.Point(0, 0);
            this.splitPanel.Margin = new System.Windows.Forms.Padding(2);
            this.splitPanel.Name = "splitPanel";
            this.splitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.Controls.Add(this.Browser);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Controls.Add(this.groupLog);
            this.splitPanel.Panel2MinSize = 100;
            this.splitPanel.Size = new System.Drawing.Size(1046, 559);
            this.splitPanel.SplitterDistance = 433;
            this.splitPanel.SplitterWidth = 5;
            this.splitPanel.TabIndex = 4;
            // 
            // Browser
            // 
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.Location = new System.Drawing.Point(0, 0);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(1042, 429);
            this.Browser.TabIndex = 0;
            this.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Browser_DocumentCompleted);
            // 
            // groupLog
            // 
            this.groupLog.Controls.Add(this.LogTextBox);
            this.groupLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupLog.Location = new System.Drawing.Point(0, 0);
            this.groupLog.Margin = new System.Windows.Forms.Padding(2);
            this.groupLog.Name = "groupLog";
            this.groupLog.Padding = new System.Windows.Forms.Padding(2);
            this.groupLog.Size = new System.Drawing.Size(1042, 117);
            this.groupLog.TabIndex = 0;
            this.groupLog.TabStop = false;
            this.groupLog.Text = "Log";
            // 
            // refreshBacktest
            // 
            this.refreshBacktest.Interval = 250;
            this.refreshBacktest.Tick += new System.EventHandler(this.refreshBacktest_Tick);
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitPanel);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(1046, 559);
            this.Load += new System.EventHandler(this.ChartControl_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitPanel.Panel1.ResumeLayout(false);
            this.splitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).EndInit();
            this.splitPanel.ResumeLayout(false);
            this.groupLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel FormToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel FormToolStripStatusStringLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatisticsToolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar FormToolStripProgressBar;
        private System.Windows.Forms.SplitContainer splitPanel;
        private System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.GroupBox groupLog;
        private System.Windows.Forms.Timer refreshBacktest;

    }
}
