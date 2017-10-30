namespace WearDataHelper
{
    partial class MainWindow
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
            this.gbAssetNum = new System.Windows.Forms.GroupBox();
            this.txtAssetNum = new System.Windows.Forms.TextBox();
            this.pgAttributes = new System.Windows.Forms.PropertyGrid();
            this.gbDateOH = new System.Windows.Forms.GroupBox();
            this.dtpOH = new System.Windows.Forms.DateTimePicker();
            this.btnRename = new System.Windows.Forms.Button();
            this.gbAssetNum.SuspendLayout();
            this.gbDateOH.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAssetNum
            // 
            this.gbAssetNum.Controls.Add(this.txtAssetNum);
            this.gbAssetNum.Location = new System.Drawing.Point(8, 8);
            this.gbAssetNum.Name = "gbAssetNum";
            this.gbAssetNum.Size = new System.Drawing.Size(168, 56);
            this.gbAssetNum.TabIndex = 1;
            this.gbAssetNum.TabStop = false;
            this.gbAssetNum.Text = "Asset #";
            // 
            // txtAssetNum
            // 
            this.txtAssetNum.Location = new System.Drawing.Point(8, 24);
            this.txtAssetNum.Name = "txtAssetNum";
            this.txtAssetNum.Size = new System.Drawing.Size(136, 20);
            this.txtAssetNum.TabIndex = 0;
            // 
            // pgAttributes
            // 
            this.pgAttributes.Location = new System.Drawing.Point(8, 312);
            this.pgAttributes.Name = "pgAttributes";
            this.pgAttributes.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgAttributes.Size = new System.Drawing.Size(640, 232);
            this.pgAttributes.TabIndex = 6;
            this.pgAttributes.ToolbarVisible = false;
            // 
            // gbDateOH
            // 
            this.gbDateOH.Controls.Add(this.dtpOH);
            this.gbDateOH.Location = new System.Drawing.Point(184, 8);
            this.gbDateOH.Name = "gbDateOH";
            this.gbDateOH.Size = new System.Drawing.Size(128, 56);
            this.gbDateOH.TabIndex = 2;
            this.gbDateOH.TabStop = false;
            this.gbDateOH.Text = "Date of O/H";
            // 
            // dtpOH
            // 
            this.dtpOH.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOH.Location = new System.Drawing.Point(8, 24);
            this.dtpOH.Name = "dtpOH";
            this.dtpOH.Size = new System.Drawing.Size(104, 20);
            this.dtpOH.TabIndex = 0;
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(576, 24);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 12;
            this.btnRename.Text = "&Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(663, 652);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.pgAttributes);
            this.Controls.Add(this.gbDateOH);
            this.Controls.Add(this.gbAssetNum);
            this.Name = "MainWindow";
            this.Text = "Wear Data Helper";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.gbAssetNum.ResumeLayout(false);
            this.gbAssetNum.PerformLayout();
            this.gbDateOH.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbAssetNum;
        private System.Windows.Forms.TextBox txtAssetNum;
        private System.Windows.Forms.PropertyGrid pgAttributes;
        private System.Windows.Forms.GroupBox gbDateOH;
        private System.Windows.Forms.DateTimePicker dtpOH;
        private System.Windows.Forms.Button btnRename;
    }
}

