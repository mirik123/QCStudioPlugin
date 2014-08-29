namespace QuantConnect.QCPlugin
{
    partial class OpenProjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenProjects));
            this.listViewProjects = new System.Windows.Forms.ListView();
            this.idHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.projectNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateModifiedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelInstructions = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewProjects
            // 
            this.listViewProjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idHeader,
            this.projectNameHeader,
            this.dateModifiedHeader});
            this.listViewProjects.FullRowSelect = true;
            this.listViewProjects.Location = new System.Drawing.Point(7, 39);
            this.listViewProjects.MultiSelect = false;
            this.listViewProjects.Name = "listViewProjects";
            this.listViewProjects.Size = new System.Drawing.Size(518, 274);
            this.listViewProjects.TabIndex = 0;
            this.listViewProjects.UseCompatibleStateImageBehavior = false;
            this.listViewProjects.View = System.Windows.Forms.View.Details;
            this.listViewProjects.DoubleClick += new System.EventHandler(this.OpenBtn_Click);
            // 
            // idHeader
            // 
            this.idHeader.Text = "ID";
            // 
            // projectNameHeader
            // 
            this.projectNameHeader.Text = "Project Name";
            this.projectNameHeader.Width = 310;
            // 
            // dateModifiedHeader
            // 
            this.dateModifiedHeader.Text = "Date Modified";
            this.dateModifiedHeader.Width = 140;
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Location = new System.Drawing.Point(4, 23);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(148, 13);
            this.labelInstructions.TabIndex = 1;
            this.labelInstructions.Text = "Please select a project below:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Command-Refresh.png");
            this.imageList1.Images.SetKeyName(1, "fork.png");
            this.imageList1.Images.SetKeyName(2, "Garbage-Closed.png");
            this.imageList1.Images.SetKeyName(3, "Graph-03.png");
            this.imageList1.Images.SetKeyName(4, "Key.png");
            this.imageList1.Images.SetKeyName(5, "Logout.png");
            this.imageList1.Images.SetKeyName(6, "Media-Pause.png");
            this.imageList1.Images.SetKeyName(7, "Media-Play.png");
            this.imageList1.Images.SetKeyName(8, "Media-Stop.png");
            this.imageList1.Images.SetKeyName(9, "Open.png");
            this.imageList1.Images.SetKeyName(10, "Reload.png");
            this.imageList1.Images.SetKeyName(11, "Save.png");
            this.imageList1.Images.SetKeyName(12, "User-Login.png");
            this.imageList1.Images.SetKeyName(13, "Command-Refresh-Gray.png");
            this.imageList1.Images.SetKeyName(14, "Open-Gray.png");
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.Location = new System.Drawing.Point(435, 319);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(90, 35);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "&Open ";
            this.buttonOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // OpenProjects
            // 
            this.AcceptButton = this.buttonOpen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 364);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.listViewProjects);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(551, 403);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(551, 403);
            this.Name = "OpenProjects";
            this.Text = "QuantConnect Projects";
            this.Load += new System.EventHandler(this.OpenProjects_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewProjects;
        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader projectNameHeader;
        private System.Windows.Forms.ColumnHeader dateModifiedHeader;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonOpen;

    }
}