namespace PetriNetworkSimulator.Forms.Tools
{
    partial class StateMatrix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateMatrix));
            this.toolStripStateMatrix = new System.Windows.Forms.ToolStrip();
            this.tsbFire = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoFire = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.tsbRules = new System.Windows.Forms.ToolStripButton();
            this.tsbExportAsImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbText = new System.Windows.Forms.ToolStripButton();
            this.tsbTeX = new System.Windows.Forms.ToolStripButton();
            this.pbStateMatrix = new System.Windows.Forms.PictureBox();
            this.toolStripStateMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStateMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripStateMatrix
            // 
            this.toolStripStateMatrix.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFire,
            this.tsbAutoFire,
            this.tsbStop,
            this.tsbClear,
            this.tsbRules,
            this.tsbExportAsImage,
            this.toolStripSeparator1,
            this.tsbText,
            this.tsbTeX});
            this.toolStripStateMatrix.Location = new System.Drawing.Point(3, 3);
            this.toolStripStateMatrix.Name = "toolStripStateMatrix";
            this.toolStripStateMatrix.Size = new System.Drawing.Size(416, 25);
            this.toolStripStateMatrix.TabIndex = 2;
            this.toolStripStateMatrix.Text = "toolStrip1";
            // 
            // tsbFire
            // 
            this.tsbFire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFire.Image = global::PetriNetworkSimulator.Properties.Resources.fire;
            this.tsbFire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFire.Name = "tsbFire";
            this.tsbFire.Size = new System.Drawing.Size(23, 22);
            this.tsbFire.Text = "Fire";
            this.tsbFire.Click += new System.EventHandler(this.tsbActionCall_Click);
            // 
            // tsbAutoFire
            // 
            this.tsbAutoFire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutoFire.Image = global::PetriNetworkSimulator.Properties.Resources.play;
            this.tsbAutoFire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoFire.Name = "tsbAutoFire";
            this.tsbAutoFire.Size = new System.Drawing.Size(23, 22);
            this.tsbAutoFire.Text = "Auto fire";
            this.tsbAutoFire.ToolTipText = "Auto fire";
            this.tsbAutoFire.Click += new System.EventHandler(this.tsbActionCall_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Image = global::PetriNetworkSimulator.Properties.Resources.stop;
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "Stop auto fire";
            this.tsbStop.Click += new System.EventHandler(this.tsbActionCall_Click);
            // 
            // tsbClear
            // 
            this.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClear.Image = global::PetriNetworkSimulator.Properties.Resources.clear24;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(23, 22);
            this.tsbClear.Text = "Clear";
            this.tsbClear.Click += new System.EventHandler(this.tsbActionCall_Click);
            // 
            // tsbRules
            // 
            this.tsbRules.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRules.Image = global::PetriNetworkSimulator.Properties.Resources.settings;
            this.tsbRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRules.Name = "tsbRules";
            this.tsbRules.Size = new System.Drawing.Size(23, 22);
            this.tsbRules.Text = "Rules";
            this.tsbRules.Click += new System.EventHandler(this.tsbRules_Click);
            // 
            // tsbExportAsImage
            // 
            this.tsbExportAsImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportAsImage.Image = global::PetriNetworkSimulator.Properties.Resources.save;
            this.tsbExportAsImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportAsImage.Name = "tsbExportAsImage";
            this.tsbExportAsImage.Size = new System.Drawing.Size(23, 22);
            this.tsbExportAsImage.Text = "exportAsImage";
            this.tsbExportAsImage.Click += new System.EventHandler(this.tsbExportAsImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbText
            // 
            this.tsbText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbText.Image = global::PetriNetworkSimulator.Properties.Resources.plain_text;
            this.tsbText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbText.Name = "tsbText";
            this.tsbText.Size = new System.Drawing.Size(23, 22);
            this.tsbText.Text = "toolStripButton1";
            this.tsbText.Click += new System.EventHandler(this.tsbText_Click);
            // 
            // tsbTeX
            // 
            this.tsbTeX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTeX.Image = global::PetriNetworkSimulator.Properties.Resources.tex;
            this.tsbTeX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTeX.Name = "tsbTeX";
            this.tsbTeX.Size = new System.Drawing.Size(23, 22);
            this.tsbTeX.Text = "toolStripButton1";
            this.tsbTeX.Click += new System.EventHandler(this.tsbTeX_Click);
            // 
            // pbStateMatrix
            // 
            this.pbStateMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbStateMatrix.Location = new System.Drawing.Point(3, 31);
            this.pbStateMatrix.Name = "pbStateMatrix";
            this.pbStateMatrix.Size = new System.Drawing.Size(413, 87);
            this.pbStateMatrix.TabIndex = 3;
            this.pbStateMatrix.TabStop = false;
            this.pbStateMatrix.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbStateMatrix_MouseClick);
            // 
            // StateMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 146);
            this.Controls.Add(this.pbStateMatrix);
            this.Controls.Add(this.toolStripStateMatrix);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StateMatrix";
            this.Resize += new System.EventHandler(this.StateMatrix_Resize);
            this.Controls.SetChildIndex(this.toolStripStateMatrix, 0);
            this.Controls.SetChildIndex(this.pbStateMatrix, 0);
            this.toolStripStateMatrix.ResumeLayout(false);
            this.toolStripStateMatrix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStateMatrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripStateMatrix;
        private System.Windows.Forms.ToolStripButton tsbFire;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripButton tsbRules;
        private System.Windows.Forms.PictureBox pbStateMatrix;
        private System.Windows.Forms.ToolStripButton tsbExportAsImage;
        private System.Windows.Forms.ToolStripButton tsbAutoFire;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripButton tsbText;
        private System.Windows.Forms.ToolStripButton tsbTeX;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}