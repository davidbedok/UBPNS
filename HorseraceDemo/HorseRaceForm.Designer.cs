namespace HorseraceDemo
{
    partial class HorseRaceForm
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
            this.bStart = new System.Windows.Forms.Button();
            this.lbResult = new System.Windows.Forms.ListBox();
            this.race = new System.ComponentModel.BackgroundWorker();
            this.bStartThread = new System.Windows.Forms.Button();
            this.lbResultThread = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(12, 12);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // lbResult
            // 
            this.lbResult.FormattingEnabled = true;
            this.lbResult.Location = new System.Drawing.Point(13, 41);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(427, 69);
            this.lbResult.TabIndex = 1;
            // 
            // race
            // 
            this.race.WorkerReportsProgress = true;
            this.race.WorkerSupportsCancellation = true;
            this.race.DoWork += new System.ComponentModel.DoWorkEventHandler(this.race_DoWork);
            this.race.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.race_ProgressChanged);
            this.race.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.race_RunWorkerCompleted);
            // 
            // bStartThread
            // 
            this.bStartThread.Location = new System.Drawing.Point(12, 116);
            this.bStartThread.Name = "bStartThread";
            this.bStartThread.Size = new System.Drawing.Size(75, 23);
            this.bStartThread.TabIndex = 2;
            this.bStartThread.Text = "Start thread";
            this.bStartThread.UseVisualStyleBackColor = true;
            this.bStartThread.Click += new System.EventHandler(this.bStartThread_Click);
            // 
            // lbResultThread
            // 
            this.lbResultThread.FormattingEnabled = true;
            this.lbResultThread.Location = new System.Drawing.Point(13, 145);
            this.lbResultThread.Name = "lbResultThread";
            this.lbResultThread.Size = new System.Drawing.Size(427, 69);
            this.lbResultThread.TabIndex = 3;
            // 
            // HorseRaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 227);
            this.Controls.Add(this.lbResultThread);
            this.Controls.Add(this.bStartThread);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.bStart);
            this.Name = "HorseRaceForm";
            this.Text = "Horserace";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.ListBox lbResult;
        private System.ComponentModel.BackgroundWorker race;
        private System.Windows.Forms.Button bStartThread;
        private System.Windows.Forms.ListBox lbResultThread;
    }
}

