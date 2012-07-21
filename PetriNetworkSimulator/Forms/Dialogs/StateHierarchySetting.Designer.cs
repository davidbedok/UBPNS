namespace PetriNetworkSimulator.Forms.Dialogs
{
    partial class StateHierarchySetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudStateHierarchyHeight = new System.Windows.Forms.NumericUpDown();
            this.nudStateHierarchyWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStateHierarchyHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStateHierarchyWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudStateHierarchyHeight);
            this.groupBox1.Controls.Add(this.nudStateHierarchyWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(10, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 101);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // nudStateHierarchyHeight
            // 
            this.nudStateHierarchyHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nudStateHierarchyHeight.Location = new System.Drawing.Point(136, 52);
            this.nudStateHierarchyHeight.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudStateHierarchyHeight.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudStateHierarchyHeight.Name = "nudStateHierarchyHeight";
            this.nudStateHierarchyHeight.Size = new System.Drawing.Size(135, 20);
            this.nudStateHierarchyHeight.TabIndex = 14;
            this.nudStateHierarchyHeight.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // nudStateHierarchyWidth
            // 
            this.nudStateHierarchyWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nudStateHierarchyWidth.Location = new System.Drawing.Point(136, 18);
            this.nudStateHierarchyWidth.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudStateHierarchyWidth.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudStateHierarchyWidth.Name = "nudStateHierarchyWidth";
            this.nudStateHierarchyWidth.Size = new System.Drawing.Size(135, 20);
            this.nudStateHierarchyWidth.TabIndex = 13;
            this.nudStateHierarchyWidth.Value = new decimal(new int[] {
            320,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "State hierarchy height:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "State hierarchy width:";
            // 
            // StateHierarchySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(335, 227);
            this.Controls.Add(this.groupBox1);
            this.Name = "StateHierarchySetting";
            this.Text = "State hierarchy settings";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStateHierarchyHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStateHierarchyWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudStateHierarchyHeight;
        private System.Windows.Forms.NumericUpDown nudStateHierarchyWidth;
    }
}
