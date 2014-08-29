namespace QuantConnect.QCPlugin
{
    partial class BacktestChartForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BacktestChartForm));
            this.timerDownloadResults = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupChart = new System.Windows.Forms.GroupBox();
            this.tabCharts = new System.Windows.Forms.TabControl();
            this.groupChartOptions = new System.Windows.Forms.GroupBox();
            this.dataGridViewStats = new System.Windows.Forms.DataGridView();
            this.tabFooter = new System.Windows.Forms.TabControl();
            this.tabTrades = new System.Windows.Forms.TabPage();
            this.dataGridViewTrades = new System.Windows.Forms.DataGridView();
            this.timerDrawChart = new System.Windows.Forms.Timer(this.components);
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.SuspendLayout();
            // 
            // timerDownloadResults
            // 
            this.timerDownloadResults.Enabled = true;
            this.timerDownloadResults.Interval = 5000;
            this.timerDownloadResults.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 519);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(958, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
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
            this.splitContainer2.Size = new System.Drawing.Size(958, 519);
            this.splitContainer2.SplitterDistance = 391;
            this.splitContainer2.TabIndex = 2;
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
            this.splitContainer1.Size = new System.Drawing.Size(958, 391);
            this.splitContainer1.SplitterDistance = 758;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupChart
            // 
            this.groupChart.Controls.Add(this.tabCharts);
            this.groupChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChart.Location = new System.Drawing.Point(0, 0);
            this.groupChart.Name = "groupChart";
            this.groupChart.Size = new System.Drawing.Size(758, 391);
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
            this.tabCharts.Size = new System.Drawing.Size(752, 372);
            this.tabCharts.TabIndex = 0;
            // 
            // groupChartOptions
            // 
            this.groupChartOptions.Controls.Add(this.dataGridViewStats);
            this.groupChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChartOptions.Location = new System.Drawing.Point(0, 0);
            this.groupChartOptions.Name = "groupChartOptions";
            this.groupChartOptions.Size = new System.Drawing.Size(196, 391);
            this.groupChartOptions.TabIndex = 0;
            this.groupChartOptions.TabStop = false;
            this.groupChartOptions.Text = "Statistics";
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
            this.dataGridViewStats.Size = new System.Drawing.Size(190, 372);
            this.dataGridViewStats.TabIndex = 0;
            // 
            // tabFooter
            // 
            this.tabFooter.Controls.Add(this.tabTrades);
            this.tabFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFooter.Location = new System.Drawing.Point(0, 0);
            this.tabFooter.Name = "tabFooter";
            this.tabFooter.SelectedIndex = 0;
            this.tabFooter.Size = new System.Drawing.Size(958, 250);
            this.tabFooter.TabIndex = 0;
            // 
            // tabTrades
            // 
            this.tabTrades.Controls.Add(this.dataGridViewTrades);
            this.tabTrades.Location = new System.Drawing.Point(4, 22);
            this.tabTrades.Name = "tabTrades";
            this.tabTrades.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrades.Size = new System.Drawing.Size(950, 224);
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
            this.dataGridViewTrades.Size = new System.Drawing.Size(944, 218);
            this.dataGridViewTrades.TabIndex = 1;
            // 
            // timerDrawChart
            // 
            this.timerDrawChart.Enabled = true;
            this.timerDrawChart.Interval = 50;
            this.timerDrawChart.Tick += new System.EventHandler(this.timerDrawChart_Tick);
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnName.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            // 
            // columnValue
            // 
            this.columnValue.HeaderText = "Value";
            this.columnValue.Name = "columnValue";
            this.columnValue.Width = 90;
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
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
            // BacktestChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 541);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BacktestChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuantConnect Backtest Result";
            this.Load += new System.EventHandler(this.BacktestChart_Load);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerDownloadResults;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupChart;
        private System.Windows.Forms.GroupBox groupChartOptions;
        private System.Windows.Forms.TabControl tabFooter;
        private System.Windows.Forms.TabControl tabCharts;
        private System.Windows.Forms.TabPage tabTrades;
        private System.Windows.Forms.Timer timerDrawChart;
        private System.Windows.Forms.DataGridView dataGridViewStats;
        private System.Windows.Forms.DataGridView dataGridViewTrades;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}