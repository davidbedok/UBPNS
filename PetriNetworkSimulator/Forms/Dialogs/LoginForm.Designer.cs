namespace PetriNetworkSimulator.Forms.Dialogs
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bEnter = new System.Windows.Forms.Button();
            this.bNoKey = new System.Windows.Forms.Button();
            this.bBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStorePassword = new System.Windows.Forms.TextBox();
            this.tbStore = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openKeyStoreDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbInformation = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bEnter);
            this.groupBox1.Controls.Add(this.bNoKey);
            this.groupBox1.Controls.Add(this.bBrowse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbStorePassword);
            this.groupBox1.Controls.Add(this.tbStore);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(140, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PrivateKey";
            // 
            // bEnter
            // 
            this.bEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bEnter.Location = new System.Drawing.Point(159, 97);
            this.bEnter.Name = "bEnter";
            this.bEnter.Size = new System.Drawing.Size(90, 23);
            this.bEnter.TabIndex = 5;
            this.bEnter.Text = "Enter";
            this.bEnter.UseVisualStyleBackColor = true;
            this.bEnter.Click += new System.EventHandler(this.bEnter_Click);
            // 
            // bNoKey
            // 
            this.bNoKey.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bNoKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNoKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bNoKey.Location = new System.Drawing.Point(19, 97);
            this.bNoKey.Name = "bNoKey";
            this.bNoKey.Size = new System.Drawing.Size(134, 23);
            this.bNoKey.TabIndex = 1;
            this.bNoKey.Text = "I haven\'t got key";
            this.bNoKey.UseVisualStyleBackColor = true;
            // 
            // bBrowse
            // 
            this.bBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bBrowse.Location = new System.Drawing.Point(211, 28);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(38, 20);
            this.bBrowse.TabIndex = 4;
            this.bBrowse.Text = "..";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Store password:";
            // 
            // tbStorePassword
            // 
            this.tbStorePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbStorePassword.Location = new System.Drawing.Point(105, 54);
            this.tbStorePassword.Name = "tbStorePassword";
            this.tbStorePassword.PasswordChar = '*';
            this.tbStorePassword.Size = new System.Drawing.Size(144, 20);
            this.tbStorePassword.TabIndex = 2;
            // 
            // tbStore
            // 
            this.tbStore.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbStore.Location = new System.Drawing.Point(105, 28);
            this.tbStore.Name = "tbStore";
            this.tbStore.ReadOnly = true;
            this.tbStore.Size = new System.Drawing.Size(99, 20);
            this.tbStore.TabIndex = 1;
            this.tbStore.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PKCS12 store:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PetriNetworkSimulator.Properties.Resources.petricup;
            this.pictureBox1.Location = new System.Drawing.Point(10, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // openKeyStoreDialog
            // 
            this.openKeyStoreDialog.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbInformation);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 73);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
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
            this.rtbInformation.Size = new System.Drawing.Size(381, 51);
            this.rtbInformation.TabIndex = 0;
            this.rtbInformation.Text = resources.GetString("rtbInformation.Text");
            // 
            // LoginForm
            // 
            this.AcceptButton = this.bEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bNoKey;
            this.ClientSize = new System.Drawing.Size(416, 225);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petri Network Simulator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bNoKey;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bEnter;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStorePassword;
        private System.Windows.Forms.TextBox tbStore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openKeyStoreDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbInformation;
    }
}