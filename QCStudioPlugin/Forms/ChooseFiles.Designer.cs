namespace QuantConnect.QCStudioPlugin.Forms
{
    partial class ChooseFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseFiles));
            this.dgrFiles = new System.Windows.Forms.DataGridView();
            this.CloudFiles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkAction = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LocalFiles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrFiles
            // 
            this.dgrFiles.AllowUserToAddRows = false;
            this.dgrFiles.AllowUserToDeleteRows = false;
            this.dgrFiles.AllowUserToOrderColumns = true;
            this.dgrFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrFiles.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgrFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dgrFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CloudFiles,
            this.chkAction,
            this.LocalFiles});
            this.dgrFiles.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgrFiles.Location = new System.Drawing.Point(2, 1);
            this.dgrFiles.MultiSelect = false;
            this.dgrFiles.Name = "dgrFiles";
            this.dgrFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgrFiles.RowHeadersVisible = false;
            this.dgrFiles.Size = new System.Drawing.Size(473, 213);
            this.dgrFiles.TabIndex = 8;
            // 
            // CloudFiles
            // 
            this.CloudFiles.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CloudFiles.FillWeight = 111.9289F;
            this.CloudFiles.HeaderText = "Files in cloud project";
            this.CloudFiles.Name = "CloudFiles";
            // 
            // chkAction
            // 
            this.chkAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chkAction.HeaderText = "Upload";
            this.chkAction.Name = "chkAction";
            this.chkAction.Width = 50;
            // 
            // LocalFiles
            // 
            this.LocalFiles.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocalFiles.FillWeight = 111.9289F;
            this.LocalFiles.HeaderText = "Files in local project";
            this.LocalFiles.Name = "LocalFiles";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLogin.Location = new System.Drawing.Point(99, 216);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(90, 30);
            this.buttonLogin.TabIndex = 9;
            this.buttonLogin.Text = "&OK";
            this.buttonLogin.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(262, 216);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 30);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ChooseFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 246);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dgrFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseFiles";
            this.Text = "ChooseFiles";
            ((System.ComponentModel.ISupportInitialize)(this.dgrFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonCancel;
        internal System.Windows.Forms.DataGridView dgrFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn CloudFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalFiles;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn chkAction;

    }
}