namespace QuantConnect.QCPlugin
{
    partial class StartBacktest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartBacktest));
            this.labelMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureError = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureCheck = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(11, 9);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(435, 28);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "Building Project in Visual Studio...";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 39);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(435, 19);
            this.progressBar.TabIndex = 2;
            // 
            // pictureError
            // 
            this.pictureError.Image = ((System.Drawing.Image)(resources.GetObject("pictureError.Image")));
            this.pictureError.Location = new System.Drawing.Point(430, 18);
            this.pictureError.Name = "pictureError";
            this.pictureError.Size = new System.Drawing.Size(16, 16);
            this.pictureError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureError.TabIndex = 3;
            this.pictureError.TabStop = false;
            this.pictureError.Visible = false;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 30;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureCheck
            // 
            this.pictureCheck.Image = ((System.Drawing.Image)(resources.GetObject("pictureCheck.Image")));
            this.pictureCheck.Location = new System.Drawing.Point(430, 18);
            this.pictureCheck.Name = "pictureCheck";
            this.pictureCheck.Size = new System.Drawing.Size(16, 16);
            this.pictureCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureCheck.TabIndex = 4;
            this.pictureCheck.TabStop = false;
            this.pictureCheck.Visible = false;
            // 
            // StartBacktest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 72);
            this.Controls.Add(this.pictureCheck);
            this.Controls.Add(this.pictureError);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartBacktest";
            this.Text = "Backtesting Project";
            this.Load += new System.EventHandler(this.StartBacktest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureError;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureCheck;
    }
}