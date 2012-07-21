namespace PetriNetworkSimulator.Controls
{
    partial class PropertyGroupMoveTool
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpFrame = new System.Windows.Forms.TableLayoutPanel();
            this.bBigMinus = new System.Windows.Forms.Button();
            this.bSmallPlus = new System.Windows.Forms.Button();
            this.bSmallMinus = new System.Windows.Forms.Button();
            this.bBigPlus = new System.Windows.Forms.Button();
            this.tlpFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpFrame
            // 
            this.tlpFrame.ColumnCount = 4;
            this.tlpFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFrame.Controls.Add(this.bBigMinus, 0, 0);
            this.tlpFrame.Controls.Add(this.bSmallPlus, 2, 0);
            this.tlpFrame.Controls.Add(this.bSmallMinus, 1, 0);
            this.tlpFrame.Controls.Add(this.bBigPlus, 3, 0);
            this.tlpFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFrame.Location = new System.Drawing.Point(0, 0);
            this.tlpFrame.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFrame.Name = "tlpFrame";
            this.tlpFrame.RowCount = 1;
            this.tlpFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpFrame.Size = new System.Drawing.Size(295, 30);
            this.tlpFrame.TabIndex = 2;
            // 
            // bBigMinus
            // 
            this.bBigMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.bBigMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bBigMinus.Location = new System.Drawing.Point(3, 3);
            this.bBigMinus.Name = "bBigMinus";
            this.bBigMinus.Size = new System.Drawing.Size(67, 23);
            this.bBigMinus.TabIndex = 2;
            this.bBigMinus.Text = "-";
            this.bBigMinus.UseVisualStyleBackColor = true;
            this.bBigMinus.Click += new System.EventHandler(this.bMinusMinus_Click);
            // 
            // bSmallPlus
            // 
            this.bSmallPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.bSmallPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bSmallPlus.Location = new System.Drawing.Point(149, 3);
            this.bSmallPlus.Name = "bSmallPlus";
            this.bSmallPlus.Size = new System.Drawing.Size(67, 23);
            this.bSmallPlus.TabIndex = 1;
            this.bSmallPlus.Text = "+";
            this.bSmallPlus.UseVisualStyleBackColor = true;
            // 
            // bSmallMinus
            // 
            this.bSmallMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.bSmallMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bSmallMinus.Location = new System.Drawing.Point(76, 3);
            this.bSmallMinus.Name = "bSmallMinus";
            this.bSmallMinus.Size = new System.Drawing.Size(67, 23);
            this.bSmallMinus.TabIndex = 0;
            this.bSmallMinus.Text = "-";
            this.bSmallMinus.UseVisualStyleBackColor = true;
            // 
            // bBigPlus
            // 
            this.bBigPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bBigPlus.Location = new System.Drawing.Point(222, 3);
            this.bBigPlus.Name = "bBigPlus";
            this.bBigPlus.Size = new System.Drawing.Size(70, 23);
            this.bBigPlus.TabIndex = 3;
            this.bBigPlus.Text = "+";
            this.bBigPlus.UseVisualStyleBackColor = true;
            this.bBigPlus.Click += new System.EventHandler(this.bPlusPlus_Click);
            // 
            // PropertyGroupMoveTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpFrame);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PropertyGroupMoveTool";
            this.Size = new System.Drawing.Size(295, 30);
            this.tlpFrame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpFrame;
        private System.Windows.Forms.Button bSmallMinus;
        private System.Windows.Forms.Button bSmallPlus;
        private System.Windows.Forms.Button bBigMinus;
        private System.Windows.Forms.Button bBigPlus;
    }
}
