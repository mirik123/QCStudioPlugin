namespace ZedGraphUIControl
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupChart = new System.Windows.Forms.GroupBox();
            this.tabCharts = new System.Windows.Forms.TabControl();
            this.groupChartOptions = new System.Windows.Forms.GroupBox();
            this.dataGridViewStats = new System.Windows.Forms.DataGridView();
            this.dataGridViewTrades = new System.Windows.Forms.DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrades)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer2.Panel2.Controls.Add(this.dataGridViewTrades);
            this.splitContainer2.Panel2MinSize = 100;
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
            this.groupChart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.groupChartOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.dataGridViewStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStats.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewStats.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewStats.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridViewStats.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStats.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridViewStats.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewStats.Name = "dataGridViewStats";
            this.dataGridViewStats.ReadOnly = true;
            this.dataGridViewStats.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewStats.RowHeadersVisible = false;
            this.dataGridViewStats.Size = new System.Drawing.Size(284, 406);
            this.dataGridViewStats.TabIndex = 2;
            // 
            // dataGridViewTrades
            // 
            this.dataGridViewTrades.AllowUserToAddRows = false;
            this.dataGridViewTrades.AllowUserToDeleteRows = false;
            this.dataGridViewTrades.AllowUserToOrderColumns = true;
            this.dataGridViewTrades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTrades.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewTrades.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTrades.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridViewTrades.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewTrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTrades.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridViewTrades.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTrades.Name = "dataGridViewTrades";
            this.dataGridViewTrades.ReadOnly = true;
            this.dataGridViewTrades.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewTrades.RowHeadersVisible = false;
            this.dataGridViewTrades.Size = new System.Drawing.Size(1394, 263);
            this.dataGridViewTrades.TabIndex = 2;
            // 
            // QCClientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "QCClientControl";
            this.Size = new System.Drawing.Size(1394, 692);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupChart;
        private System.Windows.Forms.TabControl tabCharts;
        private System.Windows.Forms.GroupBox groupChartOptions;
        private System.Windows.Forms.DataGridView dataGridViewStats;
        private System.Windows.Forms.DataGridView dataGridViewTrades;
    }
}
