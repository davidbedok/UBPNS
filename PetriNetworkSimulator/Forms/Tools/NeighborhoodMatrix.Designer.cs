namespace PetriNetworkSimulator.Forms.Tools
{
    partial class NeighborhoodMatrix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeighborhoodMatrix));
            this.toolStripNeighborhoodMatrix = new System.Windows.Forms.ToolStrip();
            this.tsbExportAsImage = new System.Windows.Forms.ToolStripButton();
            this.tsbExportAsTeX = new System.Windows.Forms.ToolStripButton();
            this.tsbExportAsText = new System.Windows.Forms.ToolStripButton();
            this.tcNeighborhoodMatrix = new System.Windows.Forms.TabControl();
            this.tpMinusWeight = new System.Windows.Forms.TabPage();
            this.pbMinusWeight = new System.Windows.Forms.PictureBox();
            this.tpPlusWeight = new System.Windows.Forms.TabPage();
            this.pbPlusWeight = new System.Windows.Forms.PictureBox();
            this.tpNeighborhoodMatrix = new System.Windows.Forms.TabPage();
            this.pbNeighborhoodMatrix = new System.Windows.Forms.PictureBox();
            this.toolStripNeighborhoodMatrix.SuspendLayout();
            this.tcNeighborhoodMatrix.SuspendLayout();
            this.tpMinusWeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinusWeight)).BeginInit();
            this.tpPlusWeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlusWeight)).BeginInit();
            this.tpNeighborhoodMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNeighborhoodMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripNeighborhoodMatrix
            // 
            this.toolStripNeighborhoodMatrix.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExportAsImage,
            this.tsbExportAsTeX,
            this.tsbExportAsText});
            this.toolStripNeighborhoodMatrix.Location = new System.Drawing.Point(3, 3);
            this.toolStripNeighborhoodMatrix.Name = "toolStripNeighborhoodMatrix";
            this.toolStripNeighborhoodMatrix.Size = new System.Drawing.Size(496, 25);
            this.toolStripNeighborhoodMatrix.TabIndex = 1;
            this.toolStripNeighborhoodMatrix.Text = "toolStrip1";
            // 
            // tsbExportAsImage
            // 
            this.tsbExportAsImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportAsImage.Image = global::PetriNetworkSimulator.Properties.Resources.save;
            this.tsbExportAsImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportAsImage.Name = "tsbExportAsImage";
            this.tsbExportAsImage.Size = new System.Drawing.Size(23, 22);
            this.tsbExportAsImage.Text = "Export as Image";
            this.tsbExportAsImage.ToolTipText = "Export As Image";
            this.tsbExportAsImage.Click += new System.EventHandler(this.exportAsImage_Click);
            // 
            // tsbExportAsTeX
            // 
            this.tsbExportAsTeX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportAsTeX.Image = global::PetriNetworkSimulator.Properties.Resources.tex;
            this.tsbExportAsTeX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportAsTeX.Name = "tsbExportAsTeX";
            this.tsbExportAsTeX.Size = new System.Drawing.Size(23, 22);
            this.tsbExportAsTeX.Text = "Export as TeX";
            this.tsbExportAsTeX.ToolTipText = "Export As TeX";
            this.tsbExportAsTeX.Click += new System.EventHandler(this.tsbTeX_Click);
            // 
            // tsbExportAsText
            // 
            this.tsbExportAsText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportAsText.Image = global::PetriNetworkSimulator.Properties.Resources.plain_text;
            this.tsbExportAsText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportAsText.Name = "tsbExportAsText";
            this.tsbExportAsText.Size = new System.Drawing.Size(23, 22);
            this.tsbExportAsText.Text = "Export as Text";
            this.tsbExportAsText.ToolTipText = "Export As Text";
            this.tsbExportAsText.Click += new System.EventHandler(this.tsbText_Click);
            // 
            // tcNeighborhoodMatrix
            // 
            this.tcNeighborhoodMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcNeighborhoodMatrix.Controls.Add(this.tpMinusWeight);
            this.tcNeighborhoodMatrix.Controls.Add(this.tpPlusWeight);
            this.tcNeighborhoodMatrix.Controls.Add(this.tpNeighborhoodMatrix);
            this.tcNeighborhoodMatrix.Location = new System.Drawing.Point(6, 31);
            this.tcNeighborhoodMatrix.Name = "tcNeighborhoodMatrix";
            this.tcNeighborhoodMatrix.SelectedIndex = 0;
            this.tcNeighborhoodMatrix.Size = new System.Drawing.Size(490, 292);
            this.tcNeighborhoodMatrix.TabIndex = 3;
            // 
            // tpMinusWeight
            // 
            this.tpMinusWeight.Controls.Add(this.pbMinusWeight);
            this.tpMinusWeight.Location = new System.Drawing.Point(4, 22);
            this.tpMinusWeight.Name = "tpMinusWeight";
            this.tpMinusWeight.Padding = new System.Windows.Forms.Padding(3);
            this.tpMinusWeight.Size = new System.Drawing.Size(482, 266);
            this.tpMinusWeight.TabIndex = 0;
            this.tpMinusWeight.Text = "Weight -";
            this.tpMinusWeight.UseVisualStyleBackColor = true;
            // 
            // pbMinusWeight
            // 
            this.pbMinusWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMinusWeight.Location = new System.Drawing.Point(3, 3);
            this.pbMinusWeight.Name = "pbMinusWeight";
            this.pbMinusWeight.Size = new System.Drawing.Size(476, 260);
            this.pbMinusWeight.TabIndex = 0;
            this.pbMinusWeight.TabStop = false;
            // 
            // tpPlusWeight
            // 
            this.tpPlusWeight.Controls.Add(this.pbPlusWeight);
            this.tpPlusWeight.Location = new System.Drawing.Point(4, 22);
            this.tpPlusWeight.Name = "tpPlusWeight";
            this.tpPlusWeight.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlusWeight.Size = new System.Drawing.Size(482, 266);
            this.tpPlusWeight.TabIndex = 1;
            this.tpPlusWeight.Text = "Weight +";
            this.tpPlusWeight.UseVisualStyleBackColor = true;
            // 
            // pbPlusWeight
            // 
            this.pbPlusWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPlusWeight.Location = new System.Drawing.Point(3, 3);
            this.pbPlusWeight.Name = "pbPlusWeight";
            this.pbPlusWeight.Size = new System.Drawing.Size(476, 260);
            this.pbPlusWeight.TabIndex = 0;
            this.pbPlusWeight.TabStop = false;
            // 
            // tpNeighborhoodMatrix
            // 
            this.tpNeighborhoodMatrix.Controls.Add(this.pbNeighborhoodMatrix);
            this.tpNeighborhoodMatrix.Location = new System.Drawing.Point(4, 22);
            this.tpNeighborhoodMatrix.Name = "tpNeighborhoodMatrix";
            this.tpNeighborhoodMatrix.Padding = new System.Windows.Forms.Padding(3);
            this.tpNeighborhoodMatrix.Size = new System.Drawing.Size(482, 266);
            this.tpNeighborhoodMatrix.TabIndex = 2;
            this.tpNeighborhoodMatrix.Text = "Neighborhood Matrix";
            this.tpNeighborhoodMatrix.UseVisualStyleBackColor = true;
            // 
            // pbNeighborhoodMatrix
            // 
            this.pbNeighborhoodMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbNeighborhoodMatrix.Location = new System.Drawing.Point(3, 3);
            this.pbNeighborhoodMatrix.Name = "pbNeighborhoodMatrix";
            this.pbNeighborhoodMatrix.Size = new System.Drawing.Size(476, 260);
            this.pbNeighborhoodMatrix.TabIndex = 0;
            this.pbNeighborhoodMatrix.TabStop = false;
            // 
            // NeighborhoodMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 351);
            this.Controls.Add(this.tcNeighborhoodMatrix);
            this.Controls.Add(this.toolStripNeighborhoodMatrix);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NeighborhoodMatrix";
            this.Resize += new System.EventHandler(this.NeighborhoodMatrix_Resize);
            this.Controls.SetChildIndex(this.toolStripNeighborhoodMatrix, 0);
            this.Controls.SetChildIndex(this.tcNeighborhoodMatrix, 0);
            this.toolStripNeighborhoodMatrix.ResumeLayout(false);
            this.toolStripNeighborhoodMatrix.PerformLayout();
            this.tcNeighborhoodMatrix.ResumeLayout(false);
            this.tpMinusWeight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMinusWeight)).EndInit();
            this.tpPlusWeight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPlusWeight)).EndInit();
            this.tpNeighborhoodMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbNeighborhoodMatrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripNeighborhoodMatrix;
        private System.Windows.Forms.ToolStripButton tsbExportAsImage;
        private System.Windows.Forms.ToolStripButton tsbExportAsTeX;
        private System.Windows.Forms.ToolStripButton tsbExportAsText;
        private System.Windows.Forms.TabControl tcNeighborhoodMatrix;
        private System.Windows.Forms.TabPage tpMinusWeight;
        private System.Windows.Forms.PictureBox pbMinusWeight;
        private System.Windows.Forms.TabPage tpPlusWeight;
        private System.Windows.Forms.PictureBox pbPlusWeight;
        private System.Windows.Forms.TabPage tpNeighborhoodMatrix;
        private System.Windows.Forms.PictureBox pbNeighborhoodMatrix;
    }
}