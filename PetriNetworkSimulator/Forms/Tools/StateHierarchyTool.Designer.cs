namespace PetriNetworkSimulator.Forms.Tools
{
    partial class StateHierarchyTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateHierarchyTool));
            this.pbStateHierarchy = new System.Windows.Forms.PictureBox();
            this.stateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStateHierarchy = new System.Windows.Forms.ToolStrip();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbExportAsImage = new System.Windows.Forms.ToolStripButton();
            this.deleteStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbStateHierarchy)).BeginInit();
            this.stateContextMenu.SuspendLayout();
            this.toolStripStateHierarchy.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbStateHierarchy
            // 
            this.pbStateHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbStateHierarchy.Location = new System.Drawing.Point(3, 31);
            this.pbStateHierarchy.Name = "pbStateHierarchy";
            this.pbStateHierarchy.Size = new System.Drawing.Size(302, 161);
            this.pbStateHierarchy.TabIndex = 1;
            this.pbStateHierarchy.TabStop = false;
            this.pbStateHierarchy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbStateHierarchy_MouseClick);
            this.pbStateHierarchy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbStateHierarchy_MouseDown);
            this.pbStateHierarchy.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbStateHierarchy_MouseMove);
            this.pbStateHierarchy.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbStateHierarchy_MouseUp);
            // 
            // stateContextMenu
            // 
            this.stateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameStateToolStripMenuItem,
            this.increaseSizeToolStripMenuItem,
            this.decreaseSizeToolStripMenuItem,
            this.deleteStateToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.stateContextMenu.Name = "contextMenuStrip1";
            this.stateContextMenu.Size = new System.Drawing.Size(153, 136);
            // 
            // renameStateToolStripMenuItem
            // 
            this.renameStateToolStripMenuItem.Name = "renameStateToolStripMenuItem";
            this.renameStateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameStateToolStripMenuItem.Text = "Rename state";
            this.renameStateToolStripMenuItem.Click += new System.EventHandler(this.renameStateToolStripMenuItem_Click);
            // 
            // increaseSizeToolStripMenuItem
            // 
            this.increaseSizeToolStripMenuItem.Name = "increaseSizeToolStripMenuItem";
            this.increaseSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.increaseSizeToolStripMenuItem.Text = "Increase size";
            this.increaseSizeToolStripMenuItem.Click += new System.EventHandler(this.increaseSizeToolStripMenuItem_Click);
            // 
            // decreaseSizeToolStripMenuItem
            // 
            this.decreaseSizeToolStripMenuItem.Name = "decreaseSizeToolStripMenuItem";
            this.decreaseSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.decreaseSizeToolStripMenuItem.Text = "Decrease size";
            this.decreaseSizeToolStripMenuItem.Click += new System.EventHandler(this.decreaseSizeToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.propertiesToolStripMenuItem.Text = "Properties..";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripStateHierarchy
            // 
            this.toolStripStateHierarchy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClear,
            this.tsbSettings,
            this.tsbExportAsImage});
            this.toolStripStateHierarchy.Location = new System.Drawing.Point(3, 3);
            this.toolStripStateHierarchy.Name = "toolStripStateHierarchy";
            this.toolStripStateHierarchy.Size = new System.Drawing.Size(302, 25);
            this.toolStripStateHierarchy.TabIndex = 2;
            this.toolStripStateHierarchy.Text = "toolStrip1";
            // 
            // tsbClear
            // 
            this.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClear.Image = global::PetriNetworkSimulator.Properties.Resources.clear24;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(23, 22);
            this.tsbClear.Text = "Clear states";
            this.tsbClear.Click += new System.EventHandler(this.tsbActionCall_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = global::PetriNetworkSimulator.Properties.Resources.settings;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(23, 22);
            this.tsbSettings.Text = "toolStripButton1";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
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
            // deleteStateToolStripMenuItem
            // 
            this.deleteStateToolStripMenuItem.Name = "deleteStateToolStripMenuItem";
            this.deleteStateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteStateToolStripMenuItem.Text = "Delete state";
            this.deleteStateToolStripMenuItem.Click += new System.EventHandler(this.deleteStateToolStripMenuItem_Click);
            // 
            // StateHierarchyTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(308, 220);
            this.Controls.Add(this.toolStripStateHierarchy);
            this.Controls.Add(this.pbStateHierarchy);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StateHierarchyTool";
            this.Resize += new System.EventHandler(this.StateHierarchy_Resize);
            this.Controls.SetChildIndex(this.pbStateHierarchy, 0);
            this.Controls.SetChildIndex(this.toolStripStateHierarchy, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbStateHierarchy)).EndInit();
            this.stateContextMenu.ResumeLayout(false);
            this.toolStripStateHierarchy.ResumeLayout(false);
            this.toolStripStateHierarchy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbStateHierarchy;
        private System.Windows.Forms.ContextMenuStrip stateContextMenu;
        private System.Windows.Forms.ToolStripMenuItem renameStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripStateHierarchy;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripButton tsbExportAsImage;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStateToolStripMenuItem;
    }
}
