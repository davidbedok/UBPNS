namespace PetriNetworkSimulator.Forms.Tools
{
    partial class StatisticsTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsTool));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbItems = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbAverage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bInit = new System.Windows.Forms.Button();
            this.tbMaximum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMinimum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bClearAllStatistics = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbItems);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(182, 217);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
            // 
            // lbItems
            // 
            this.lbItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbItems.FormattingEnabled = true;
            this.lbItems.Location = new System.Drawing.Point(5, 18);
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(172, 194);
            this.lbItems.TabIndex = 0;
            this.lbItems.SelectedIndexChanged += new System.EventHandler(this.lbItems_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbAverage);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.bInit);
            this.groupBox2.Controls.Add(this.tbMaximum);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbMinimum);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbValue);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbCount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(194, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 188);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statistics";
            // 
            // tbAverage
            // 
            this.tbAverage.Enabled = false;
            this.tbAverage.Location = new System.Drawing.Point(74, 129);
            this.tbAverage.Name = "tbAverage";
            this.tbAverage.Size = new System.Drawing.Size(65, 20);
            this.tbAverage.TabIndex = 10;
            this.tbAverage.TabStop = false;
            this.toolTipTool.SetToolTip(this.tbAverage, "Number ogf average token at the position");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Average:";
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(17, 159);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(122, 23);
            this.bInit.TabIndex = 8;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // tbMaximum
            // 
            this.tbMaximum.Enabled = false;
            this.tbMaximum.Location = new System.Drawing.Point(74, 103);
            this.tbMaximum.Name = "tbMaximum";
            this.tbMaximum.Size = new System.Drawing.Size(65, 20);
            this.tbMaximum.TabIndex = 7;
            this.tbMaximum.TabStop = false;
            this.toolTipTool.SetToolTip(this.tbMaximum, "Number of maximum token at the position");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Maximum:";
            // 
            // tbMinimum
            // 
            this.tbMinimum.Enabled = false;
            this.tbMinimum.Location = new System.Drawing.Point(74, 77);
            this.tbMinimum.Name = "tbMinimum";
            this.tbMinimum.Size = new System.Drawing.Size(65, 20);
            this.tbMinimum.TabIndex = 5;
            this.tbMinimum.TabStop = false;
            this.toolTipTool.SetToolTip(this.tbMinimum, "Number of minimum token at the position");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Minimum:";
            // 
            // tbValue
            // 
            this.tbValue.Enabled = false;
            this.tbValue.Location = new System.Drawing.Point(74, 51);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(65, 20);
            this.tbValue.TabIndex = 3;
            this.tbValue.TabStop = false;
            this.toolTipTool.SetToolTip(this.tbValue, "All tokens at the position");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value:";
            // 
            // tbCount
            // 
            this.tbCount.Enabled = false;
            this.tbCount.Location = new System.Drawing.Point(74, 25);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(65, 20);
            this.tbCount.TabIndex = 1;
            this.tbCount.TabStop = false;
            this.toolTipTool.SetToolTip(this.tbCount, "Number of activation");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Count:";
            // 
            // bClearAllStatistics
            // 
            this.bClearAllStatistics.Location = new System.Drawing.Point(194, 200);
            this.bClearAllStatistics.Name = "bClearAllStatistics";
            this.bClearAllStatistics.Size = new System.Drawing.Size(155, 23);
            this.bClearAllStatistics.TabIndex = 3;
            this.bClearAllStatistics.Text = "Clear all statistics";
            this.bClearAllStatistics.UseVisualStyleBackColor = true;
            this.bClearAllStatistics.Click += new System.EventHandler(this.bClearAllStatistics_Click);
            // 
            // StatisticsTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(355, 257);
            this.Controls.Add(this.bClearAllStatistics);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatisticsTool";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.bClearAllStatistics, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbItems;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMaximum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMinimum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.Button bClearAllStatistics;
        private System.Windows.Forms.TextBox tbAverage;
        private System.Windows.Forms.Label label5;

    }
}
