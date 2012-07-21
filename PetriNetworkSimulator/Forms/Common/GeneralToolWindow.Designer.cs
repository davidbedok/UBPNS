namespace PetriNetworkSimulator.Forms.Common
{
    partial class GeneralToolWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralToolWindow));
            this.ssToolWindow = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsddbOpacity = new System.Windows.Forms.ToolStripDropDownButton();
            this.ttsmO20 = new System.Windows.Forms.ToolStripMenuItem();
            this.ttsmO40 = new System.Windows.Forms.ToolStripMenuItem();
            this.ttsmO60 = new System.Windows.Forms.ToolStripMenuItem();
            this.ttsmO80 = new System.Windows.Forms.ToolStripMenuItem();
            this.ttsmO100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipTool = new System.Windows.Forms.ToolTip(this.components);
            this.ssToolWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssToolWindow
            // 
            this.ssToolWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.tsddbOpacity,
            this.tsslInfo});
            this.ssToolWindow.Location = new System.Drawing.Point(3, 244);
            this.ssToolWindow.Name = "ssToolWindow";
            this.ssToolWindow.ShowItemToolTips = true;
            this.ssToolWindow.Size = new System.Drawing.Size(344, 22);
            this.ssToolWindow.SizingGrip = false;
            this.ssToolWindow.TabIndex = 0;
            this.ssToolWindow.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // tsddbOpacity
            // 
            this.tsddbOpacity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbOpacity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ttsmO20,
            this.ttsmO40,
            this.ttsmO60,
            this.ttsmO80,
            this.ttsmO100});
            this.tsddbOpacity.Image = ((System.Drawing.Image)(resources.GetObject("tsddbOpacity.Image")));
            this.tsddbOpacity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbOpacity.Name = "tsddbOpacity";
            this.tsddbOpacity.Size = new System.Drawing.Size(61, 20);
            this.tsddbOpacity.Text = "Opacity";
            // 
            // ttsmO20
            // 
            this.ttsmO20.CheckOnClick = true;
            this.ttsmO20.Name = "ttsmO20";
            this.ttsmO20.Size = new System.Drawing.Size(102, 22);
            this.ttsmO20.Text = "20%";
            this.ttsmO20.Click += new System.EventHandler(this.changeOpacity);
            // 
            // ttsmO40
            // 
            this.ttsmO40.CheckOnClick = true;
            this.ttsmO40.Name = "ttsmO40";
            this.ttsmO40.Size = new System.Drawing.Size(102, 22);
            this.ttsmO40.Text = "40%";
            this.ttsmO40.Click += new System.EventHandler(this.changeOpacity);
            // 
            // ttsmO60
            // 
            this.ttsmO60.CheckOnClick = true;
            this.ttsmO60.Name = "ttsmO60";
            this.ttsmO60.Size = new System.Drawing.Size(102, 22);
            this.ttsmO60.Text = "60%";
            this.ttsmO60.Click += new System.EventHandler(this.changeOpacity);
            // 
            // ttsmO80
            // 
            this.ttsmO80.CheckOnClick = true;
            this.ttsmO80.Name = "ttsmO80";
            this.ttsmO80.Size = new System.Drawing.Size(102, 22);
            this.ttsmO80.Text = "80%";
            this.ttsmO80.Click += new System.EventHandler(this.changeOpacity);
            // 
            // ttsmO100
            // 
            this.ttsmO100.CheckOnClick = true;
            this.ttsmO100.Name = "ttsmO100";
            this.ttsmO100.Size = new System.Drawing.Size(102, 22);
            this.ttsmO100.Text = "100%";
            this.ttsmO100.Click += new System.EventHandler(this.changeOpacity);
            // 
            // tsslInfo
            // 
            this.tsslInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tsslInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // GeneralToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 269);
            this.Controls.Add(this.ssToolWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneralToolWindow";
            this.Opacity = 0.7D;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.Text = "GeneralToolWindow";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneralToolWindow_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.GeneralToolWindow_VisibleChanged);
            this.ssToolWindow.ResumeLayout(false);
            this.ssToolWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolTip toolTipTool;
        protected System.Windows.Forms.StatusStrip ssToolWindow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripDropDownButton tsddbOpacity;
        private System.Windows.Forms.ToolStripMenuItem ttsmO20;
        private System.Windows.Forms.ToolStripMenuItem ttsmO40;
        private System.Windows.Forms.ToolStripMenuItem ttsmO60;
        private System.Windows.Forms.ToolStripMenuItem ttsmO80;
        private System.Windows.Forms.ToolStripMenuItem ttsmO100;
        private System.Windows.Forms.ToolStripStatusLabel tsslInfo;
    }
}