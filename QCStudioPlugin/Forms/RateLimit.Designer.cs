namespace QuantConnect.QCPlugin
{
    partial class RateLimit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RateLimit));
            this.labelThanks = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonUpgrade = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelThanks
            // 
            this.labelThanks.AutoSize = true;
            this.labelThanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThanks.Location = new System.Drawing.Point(53, 65);
            this.labelThanks.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.labelThanks.Name = "labelThanks";
            this.labelThanks.Size = new System.Drawing.Size(255, 20);
            this.labelThanks.TabIndex = 0;
            this.labelThanks.Text = "Thank you for using QuantConnect";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoEllipsis = true;
            this.labelMessage.Location = new System.Drawing.Point(12, 95);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(334, 37);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "You have reached the limit of 5 backtests per day via API on a free account. Plea" +
    "se upgrade your account to unlock unlimited backtests.";
            // 
            // buttonUpgrade
            // 
            this.buttonUpgrade.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonUpgrade.AutoSize = true;
            this.buttonUpgrade.Location = new System.Drawing.Point(78, 133);
            this.buttonUpgrade.Name = "buttonUpgrade";
            this.buttonUpgrade.Size = new System.Drawing.Size(90, 30);
            this.buttonUpgrade.TabIndex = 0;
            this.buttonUpgrade.Text = "&Upgrade";
            this.buttonUpgrade.UseVisualStyleBackColor = true;
            this.buttonUpgrade.Click += new System.EventHandler(this.UpgradeBtn_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(196, 133);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 30);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Cancel :(";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(152, 9);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 4;
            this.pictureBoxLogo.TabStop = false;
            // 
            // RateLimit
            // 
            this.AcceptButton = this.buttonUpgrade;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(358, 173);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonUpgrade);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelThanks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RateLimit";
            this.Text = "Rate Limit Reached";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelThanks;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonUpgrade;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}