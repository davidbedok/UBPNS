namespace PetriNetworkSimulator.Forms.Main
{
    partial class PetriNetworkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PetriNetworkForm));
            this.tcPetriNetwork = new System.Windows.Forms.TabControl();
            this.tpDesign = new System.Windows.Forms.TabPage();
            this.pbPetriNetwork = new System.Windows.Forms.PictureBox();
            this.tpSource = new System.Windows.Forms.TabPage();
            this.bColoring = new System.Windows.Forms.Button();
            this.rtbSource = new System.Windows.Forms.RichTextBox();
            this.tpDescription = new System.Windows.Forms.TabPage();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.autoPlayWorker = new System.ComponentModel.BackgroundWorker();
            this.ssChildForm = new System.Windows.Forms.StatusStrip();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcPetriNetwork.SuspendLayout();
            this.tpDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPetriNetwork)).BeginInit();
            this.tpSource.SuspendLayout();
            this.tpDescription.SuspendLayout();
            this.ssChildForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcPetriNetwork
            // 
            this.tcPetriNetwork.Controls.Add(this.tpDesign);
            this.tcPetriNetwork.Controls.Add(this.tpSource);
            this.tcPetriNetwork.Controls.Add(this.tpDescription);
            this.tcPetriNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPetriNetwork.Location = new System.Drawing.Point(0, 0);
            this.tcPetriNetwork.Name = "tcPetriNetwork";
            this.tcPetriNetwork.SelectedIndex = 0;
            this.tcPetriNetwork.Size = new System.Drawing.Size(743, 352);
            this.tcPetriNetwork.TabIndex = 0;
            this.tcPetriNetwork.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcPetriNetwork_Selected);
            // 
            // tpDesign
            // 
            this.tpDesign.AutoScroll = true;
            this.tpDesign.Controls.Add(this.pbPetriNetwork);
            this.tpDesign.Location = new System.Drawing.Point(4, 22);
            this.tpDesign.Name = "tpDesign";
            this.tpDesign.Padding = new System.Windows.Forms.Padding(3);
            this.tpDesign.Size = new System.Drawing.Size(735, 326);
            this.tpDesign.TabIndex = 0;
            this.tpDesign.Text = "Design";
            this.tpDesign.UseVisualStyleBackColor = true;
            // 
            // pbPetriNetwork
            // 
            this.pbPetriNetwork.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbPetriNetwork.Location = new System.Drawing.Point(0, 0);
            this.pbPetriNetwork.Name = "pbPetriNetwork";
            this.pbPetriNetwork.Size = new System.Drawing.Size(566, 261);
            this.pbPetriNetwork.TabIndex = 0;
            this.pbPetriNetwork.TabStop = false;
            this.pbPetriNetwork.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPetriNetwork_MouseClick);
            this.pbPetriNetwork.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbPetriNetwork_MouseDoubleClick);
            this.pbPetriNetwork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPetriNetwork_MouseDown);
            this.pbPetriNetwork.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbPetriNetwork_MouseMove);
            this.pbPetriNetwork.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbPetriNetwork_MouseUp);
            // 
            // tpSource
            // 
            this.tpSource.Controls.Add(this.bColoring);
            this.tpSource.Controls.Add(this.rtbSource);
            this.tpSource.Location = new System.Drawing.Point(4, 22);
            this.tpSource.Name = "tpSource";
            this.tpSource.Padding = new System.Windows.Forms.Padding(3);
            this.tpSource.Size = new System.Drawing.Size(735, 326);
            this.tpSource.TabIndex = 1;
            this.tpSource.Text = "Source";
            this.tpSource.UseVisualStyleBackColor = true;
            // 
            // bColoring
            // 
            this.bColoring.Location = new System.Drawing.Point(8, 9);
            this.bColoring.Name = "bColoring";
            this.bColoring.Size = new System.Drawing.Size(75, 23);
            this.bColoring.TabIndex = 1;
            this.bColoring.Text = "Coloring";
            this.bColoring.UseVisualStyleBackColor = true;
            this.bColoring.Click += new System.EventHandler(this.bColoring_Click);
            // 
            // rtbSource
            // 
            this.rtbSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSource.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSource.Location = new System.Drawing.Point(3, 38);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.ReadOnly = true;
            this.rtbSource.ShowSelectionMargin = true;
            this.rtbSource.Size = new System.Drawing.Size(729, 307);
            this.rtbSource.TabIndex = 0;
            this.rtbSource.Text = "";
            this.rtbSource.WordWrap = false;
            // 
            // tpDescription
            // 
            this.tpDescription.Controls.Add(this.rtbDescription);
            this.tpDescription.Location = new System.Drawing.Point(4, 22);
            this.tpDescription.Name = "tpDescription";
            this.tpDescription.Size = new System.Drawing.Size(735, 326);
            this.tpDescription.TabIndex = 2;
            this.tpDescription.Text = "Description";
            this.tpDescription.UseVisualStyleBackColor = true;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDescription.Location = new System.Drawing.Point(0, 0);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(735, 348);
            this.rtbDescription.TabIndex = 0;
            this.rtbDescription.Text = "";
            this.rtbDescription.TextChanged += new System.EventHandler(this.rtbDescription_TextChanged);
            // 
            // autoPlayWorker
            // 
            this.autoPlayWorker.WorkerReportsProgress = true;
            this.autoPlayWorker.WorkerSupportsCancellation = true;
            this.autoPlayWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.autoPlayWorker_DoWork);
            this.autoPlayWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.autoPlayWorker_ProgressChanged);
            this.autoPlayWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.autoPlayWorker_RunWorkerCompleted);
            // 
            // ssChildForm
            // 
            this.ssChildForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslInfo});
            this.ssChildForm.Location = new System.Drawing.Point(0, 352);
            this.ssChildForm.Name = "ssChildForm";
            this.ssChildForm.Size = new System.Drawing.Size(743, 22);
            this.ssChildForm.TabIndex = 1;
            this.ssChildForm.Text = "statusStrip1";
            // 
            // tsslInfo
            // 
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(45, 17);
            this.tsslInfo.Text = "tsslInfo";
            // 
            // PetriNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 374);
            this.Controls.Add(this.tcPetriNetwork);
            this.Controls.Add(this.ssChildForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PetriNetworkForm";
            this.Text = "PetriNetworkForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PetriNetworkForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PetriNetworkForm_FormClosed);
            this.tcPetriNetwork.ResumeLayout(false);
            this.tpDesign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPetriNetwork)).EndInit();
            this.tpSource.ResumeLayout(false);
            this.tpDescription.ResumeLayout(false);
            this.ssChildForm.ResumeLayout(false);
            this.ssChildForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcPetriNetwork;
        private System.Windows.Forms.TabPage tpDesign;
        private System.Windows.Forms.PictureBox pbPetriNetwork;
        private System.Windows.Forms.TabPage tpSource;
        private System.Windows.Forms.TabPage tpDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.RichTextBox rtbSource;
        private System.Windows.Forms.Button bColoring;
        private System.ComponentModel.BackgroundWorker autoPlayWorker;
        private System.Windows.Forms.StatusStrip ssChildForm;
        private System.Windows.Forms.ToolStripStatusLabel tsslInfo;
    }
}