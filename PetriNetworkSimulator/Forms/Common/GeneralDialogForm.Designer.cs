namespace PetriNetworkSimulator.Forms.Common
{
    partial class GeneralDialogForm
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
            System.Windows.Forms.TableLayoutPanel tlpButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralDialogForm));
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.tlpControlButton = new System.Windows.Forms.TableLayoutPanel();
            this.gbInformation = new System.Windows.Forms.GroupBox();
            this.rtbInformation = new System.Windows.Forms.RichTextBox();
            tlpButton = new System.Windows.Forms.TableLayoutPanel();
            tlpButton.SuspendLayout();
            this.tlpControlButton.SuspendLayout();
            this.gbInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpButton
            // 
            tlpButton.ColumnCount = 2;
            tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpButton.Controls.Add(this.bCancel, 0, 0);
            tlpButton.Controls.Add(this.bOK, 1, 0);
            tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpButton.Location = new System.Drawing.Point(30, 3);
            tlpButton.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            tlpButton.Name = "tlpButton";
            tlpButton.RowCount = 1;
            tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpButton.Size = new System.Drawing.Size(567, 39);
            tlpButton.TabIndex = 1;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(3, 8);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.TabStop = false;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(489, 8);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // tlpControlButton
            // 
            this.tlpControlButton.ColumnCount = 1;
            this.tlpControlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControlButton.Controls.Add(tlpButton, 0, 0);
            this.tlpControlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpControlButton.Location = new System.Drawing.Point(10, 297);
            this.tlpControlButton.Name = "tlpControlButton";
            this.tlpControlButton.RowCount = 1;
            this.tlpControlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControlButton.Size = new System.Drawing.Size(627, 45);
            this.tlpControlButton.TabIndex = 0;
            // 
            // gbInformation
            // 
            this.gbInformation.Controls.Add(this.rtbInformation);
            this.gbInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbInformation.Location = new System.Drawing.Point(10, 0);
            this.gbInformation.Name = "gbInformation";
            this.gbInformation.Size = new System.Drawing.Size(627, 64);
            this.gbInformation.TabIndex = 3;
            this.gbInformation.TabStop = false;
            this.gbInformation.Text = "Information";
            // 
            // rtbInformation
            // 
            this.rtbInformation.BackColor = System.Drawing.SystemColors.Control;
            this.rtbInformation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbInformation.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtbInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbInformation.Location = new System.Drawing.Point(3, 16);
            this.rtbInformation.Name = "rtbInformation";
            this.rtbInformation.ReadOnly = true;
            this.rtbInformation.ShowSelectionMargin = true;
            this.rtbInformation.Size = new System.Drawing.Size(621, 45);
            this.rtbInformation.TabIndex = 0;
            this.rtbInformation.Text = resources.GetString("rtbInformation.Text");
            // 
            // GeneralDialogForm
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(647, 342);
            this.ControlBox = false;
            this.Controls.Add(this.gbInformation);
            this.Controls.Add(this.tlpControlButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GeneralDialogForm";
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ShowInTaskbar = false;
            this.Text = "GeneralDialogForm";
            tlpButton.ResumeLayout(false);
            this.tlpControlButton.ResumeLayout(false);
            this.gbInformation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpControlButton;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.RichTextBox rtbInformation;
        private System.Windows.Forms.GroupBox gbInformation;
        private System.Windows.Forms.Button bCancel;
    }
}