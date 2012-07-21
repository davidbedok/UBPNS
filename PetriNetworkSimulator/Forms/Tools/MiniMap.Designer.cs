namespace PetriNetworkSimulator.Forms.Tools
{
    partial class MiniMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniMap));
            this.pbMiniMap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMiniMap)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMiniMap
            // 
            this.pbMiniMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMiniMap.Location = new System.Drawing.Point(3, 3);
            this.pbMiniMap.Name = "pbMiniMap";
            this.pbMiniMap.Size = new System.Drawing.Size(244, 175);
            this.pbMiniMap.TabIndex = 1;
            this.pbMiniMap.TabStop = false;
            // 
            // MiniMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(250, 203);
            this.Controls.Add(this.pbMiniMap);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MiniMap";
            this.Controls.SetChildIndex(this.pbMiniMap, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbMiniMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMiniMap;
    }
}
