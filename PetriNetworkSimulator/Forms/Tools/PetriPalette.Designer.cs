namespace PetriNetworkSimulator.Forms.Tools
{
    partial class PetriPalette
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PetriPalette));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbSelect = new System.Windows.Forms.RadioButton();
            this.rbSingleSelect = new System.Windows.Forms.RadioButton();
            this.rbSelectEdge = new System.Windows.Forms.RadioButton();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbDeleteToken = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bSelectAll = new System.Windows.Forms.Button();
            this.bReverseSelection = new System.Windows.Forms.Button();
            this.bClearSelection = new System.Windows.Forms.Button();
            this.bDeleteSelected = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPosition = new System.Windows.Forms.RadioButton();
            this.rbTransition = new System.Windows.Forms.RadioButton();
            this.rbEdge = new System.Windows.Forms.RadioButton();
            this.rbToken = new System.Windows.Forms.RadioButton();
            this.rbNote = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.bClear);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.rbSelect);
            this.flowLayoutPanel1.Controls.Add(this.rbSingleSelect);
            this.flowLayoutPanel1.Controls.Add(this.rbSelectEdge);
            this.flowLayoutPanel1.Controls.Add(this.rbMove);
            this.flowLayoutPanel1.Controls.Add(this.rbDelete);
            this.flowLayoutPanel1.Controls.Add(this.rbDeleteToken);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.bSelectAll);
            this.flowLayoutPanel1.Controls.Add(this.bReverseSelection);
            this.flowLayoutPanel1.Controls.Add(this.bClearSelection);
            this.flowLayoutPanel1.Controls.Add(this.bDeleteSelected);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.rbPosition);
            this.flowLayoutPanel1.Controls.Add(this.rbTransition);
            this.flowLayoutPanel1.Controls.Add(this.rbEdge);
            this.flowLayoutPanel1.Controls.Add(this.rbToken);
            this.flowLayoutPanel1.Controls.Add(this.rbNote);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(755, 39);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // bClear
            // 
            this.bClear.Image = global::PetriNetworkSimulator.Properties.Resources.clear24;
            this.bClear.Location = new System.Drawing.Point(3, 3);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(30, 30);
            this.bClear.TabIndex = 4;
            this.toolTipTool.SetToolTip(this.bClear, "Clear the Petri Network");
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.commonButtonClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(39, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 30);
            this.panel2.TabIndex = 20;
            // 
            // rbSelect
            // 
            this.rbSelect.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSelect.AutoSize = true;
            this.rbSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbSelect.Image = global::PetriNetworkSimulator.Properties.Resources.select;
            this.rbSelect.Location = new System.Drawing.Point(50, 3);
            this.rbSelect.Name = "rbSelect";
            this.rbSelect.Size = new System.Drawing.Size(30, 30);
            this.rbSelect.TabIndex = 0;
            this.toolTipTool.SetToolTip(this.rbSelect, "Multi select");
            this.rbSelect.UseVisualStyleBackColor = true;
            this.rbSelect.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbSingleSelect
            // 
            this.rbSingleSelect.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSingleSelect.AutoSize = true;
            this.rbSingleSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbSingleSelect.Image = global::PetriNetworkSimulator.Properties.Resources.pointer;
            this.rbSingleSelect.Location = new System.Drawing.Point(86, 3);
            this.rbSingleSelect.Name = "rbSingleSelect";
            this.rbSingleSelect.Size = new System.Drawing.Size(30, 30);
            this.rbSingleSelect.TabIndex = 12;
            this.toolTipTool.SetToolTip(this.rbSingleSelect, "Single select");
            this.rbSingleSelect.UseVisualStyleBackColor = true;
            this.rbSingleSelect.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbSelectEdge
            // 
            this.rbSelectEdge.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSelectEdge.AutoSize = true;
            this.rbSelectEdge.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbSelectEdge.Image = global::PetriNetworkSimulator.Properties.Resources.selectedge;
            this.rbSelectEdge.Location = new System.Drawing.Point(122, 3);
            this.rbSelectEdge.Name = "rbSelectEdge";
            this.rbSelectEdge.Size = new System.Drawing.Size(30, 30);
            this.rbSelectEdge.TabIndex = 16;
            this.toolTipTool.SetToolTip(this.rbSelectEdge, "Select edge");
            this.rbSelectEdge.UseVisualStyleBackColor = true;
            this.rbSelectEdge.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbMove
            // 
            this.rbMove.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbMove.AutoSize = true;
            this.rbMove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbMove.Image = global::PetriNetworkSimulator.Properties.Resources.move;
            this.rbMove.Location = new System.Drawing.Point(158, 3);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(30, 30);
            this.rbMove.TabIndex = 13;
            this.toolTipTool.SetToolTip(this.rbMove, "Move and resize");
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbDelete
            // 
            this.rbDelete.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDelete.AutoSize = true;
            this.rbDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbDelete.Image = global::PetriNetworkSimulator.Properties.Resources.trash24;
            this.rbDelete.Location = new System.Drawing.Point(194, 3);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(30, 30);
            this.rbDelete.TabIndex = 14;
            this.toolTipTool.SetToolTip(this.rbDelete, "Delete");
            this.rbDelete.UseVisualStyleBackColor = true;
            this.rbDelete.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbDeleteToken
            // 
            this.rbDeleteToken.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDeleteToken.AutoSize = true;
            this.rbDeleteToken.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbDeleteToken.Image = global::PetriNetworkSimulator.Properties.Resources.trashtoken;
            this.rbDeleteToken.Location = new System.Drawing.Point(230, 3);
            this.rbDeleteToken.Name = "rbDeleteToken";
            this.rbDeleteToken.Size = new System.Drawing.Size(30, 30);
            this.rbDeleteToken.TabIndex = 17;
            this.toolTipTool.SetToolTip(this.rbDeleteToken, "Delete token");
            this.rbDeleteToken.UseVisualStyleBackColor = true;
            this.rbDeleteToken.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(266, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 30);
            this.panel3.TabIndex = 21;
            // 
            // bSelectAll
            // 
            this.bSelectAll.Image = global::PetriNetworkSimulator.Properties.Resources.select;
            this.bSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSelectAll.Location = new System.Drawing.Point(277, 3);
            this.bSelectAll.Name = "bSelectAll";
            this.bSelectAll.Size = new System.Drawing.Size(45, 30);
            this.bSelectAll.TabIndex = 9;
            this.bSelectAll.Text = "All";
            this.bSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipTool.SetToolTip(this.bSelectAll, "Select all");
            this.bSelectAll.UseVisualStyleBackColor = true;
            this.bSelectAll.Click += new System.EventHandler(this.commonButtonClick);
            // 
            // bReverseSelection
            // 
            this.bReverseSelection.Image = global::PetriNetworkSimulator.Properties.Resources.select;
            this.bReverseSelection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bReverseSelection.Location = new System.Drawing.Point(328, 3);
            this.bReverseSelection.Name = "bReverseSelection";
            this.bReverseSelection.Size = new System.Drawing.Size(76, 30);
            this.bReverseSelection.TabIndex = 10;
            this.bReverseSelection.Text = "Reverse";
            this.bReverseSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipTool.SetToolTip(this.bReverseSelection, "Reverse selection");
            this.bReverseSelection.UseVisualStyleBackColor = true;
            this.bReverseSelection.Click += new System.EventHandler(this.commonButtonClick);
            // 
            // bClearSelection
            // 
            this.bClearSelection.Image = global::PetriNetworkSimulator.Properties.Resources.select;
            this.bClearSelection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bClearSelection.Location = new System.Drawing.Point(410, 3);
            this.bClearSelection.Name = "bClearSelection";
            this.bClearSelection.Size = new System.Drawing.Size(59, 30);
            this.bClearSelection.TabIndex = 11;
            this.bClearSelection.Text = "Clear";
            this.bClearSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipTool.SetToolTip(this.bClearSelection, "Clear selection");
            this.bClearSelection.UseVisualStyleBackColor = true;
            this.bClearSelection.Click += new System.EventHandler(this.commonButtonClick);
            // 
            // bDeleteSelected
            // 
            this.bDeleteSelected.Image = global::PetriNetworkSimulator.Properties.Resources.trash24;
            this.bDeleteSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDeleteSelected.Location = new System.Drawing.Point(475, 3);
            this.bDeleteSelected.Name = "bDeleteSelected";
            this.bDeleteSelected.Size = new System.Drawing.Size(82, 30);
            this.bDeleteSelected.TabIndex = 15;
            this.bDeleteSelected.Text = "Selected";
            this.bDeleteSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipTool.SetToolTip(this.bDeleteSelected, "Delete selected");
            this.bDeleteSelected.UseVisualStyleBackColor = true;
            this.bDeleteSelected.Click += new System.EventHandler(this.commonButtonClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(563, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 30);
            this.panel1.TabIndex = 19;
            // 
            // rbPosition
            // 
            this.rbPosition.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPosition.AutoSize = true;
            this.rbPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbPosition.Image = global::PetriNetworkSimulator.Properties.Resources.position;
            this.rbPosition.Location = new System.Drawing.Point(574, 3);
            this.rbPosition.Name = "rbPosition";
            this.rbPosition.Size = new System.Drawing.Size(30, 30);
            this.rbPosition.TabIndex = 1;
            this.toolTipTool.SetToolTip(this.rbPosition, "Position");
            this.rbPosition.UseVisualStyleBackColor = true;
            this.rbPosition.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbTransition
            // 
            this.rbTransition.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbTransition.AutoSize = true;
            this.rbTransition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbTransition.Image = global::PetriNetworkSimulator.Properties.Resources.transition;
            this.rbTransition.Location = new System.Drawing.Point(610, 3);
            this.rbTransition.Name = "rbTransition";
            this.rbTransition.Size = new System.Drawing.Size(30, 30);
            this.rbTransition.TabIndex = 2;
            this.toolTipTool.SetToolTip(this.rbTransition, "Transition");
            this.rbTransition.UseVisualStyleBackColor = true;
            this.rbTransition.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbEdge
            // 
            this.rbEdge.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbEdge.AutoSize = true;
            this.rbEdge.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbEdge.Image = ((System.Drawing.Image)(resources.GetObject("rbEdge.Image")));
            this.rbEdge.Location = new System.Drawing.Point(646, 3);
            this.rbEdge.Name = "rbEdge";
            this.rbEdge.Size = new System.Drawing.Size(30, 30);
            this.rbEdge.TabIndex = 3;
            this.toolTipTool.SetToolTip(this.rbEdge, "Edge");
            this.rbEdge.UseVisualStyleBackColor = true;
            this.rbEdge.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbToken
            // 
            this.rbToken.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToken.AutoSize = true;
            this.rbToken.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbToken.Image = global::PetriNetworkSimulator.Properties.Resources.token;
            this.rbToken.Location = new System.Drawing.Point(682, 3);
            this.rbToken.Name = "rbToken";
            this.rbToken.Size = new System.Drawing.Size(30, 30);
            this.rbToken.TabIndex = 5;
            this.toolTipTool.SetToolTip(this.rbToken, "Token");
            this.rbToken.UseVisualStyleBackColor = true;
            this.rbToken.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // rbNote
            // 
            this.rbNote.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbNote.AutoSize = true;
            this.rbNote.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbNote.Image = global::PetriNetworkSimulator.Properties.Resources.note;
            this.rbNote.Location = new System.Drawing.Point(718, 3);
            this.rbNote.Name = "rbNote";
            this.rbNote.Size = new System.Drawing.Size(30, 30);
            this.rbNote.TabIndex = 18;
            this.toolTipTool.SetToolTip(this.rbNote, "Note");
            this.rbNote.UseVisualStyleBackColor = true;
            this.rbNote.CheckedChanged += new System.EventHandler(this.commonRadioButtonCheckedChanged);
            // 
            // PetriPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(761, 67);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PetriPalette";
            this.Opacity = 1D;
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbSelect;
        private System.Windows.Forms.RadioButton rbPosition;
        private System.Windows.Forms.RadioButton rbTransition;
        private System.Windows.Forms.RadioButton rbEdge;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.RadioButton rbToken;
        private System.Windows.Forms.Button bReverseSelection;
        private System.Windows.Forms.Button bSelectAll;
        private System.Windows.Forms.Button bClearSelection;
        private System.Windows.Forms.RadioButton rbSingleSelect;
        private System.Windows.Forms.RadioButton rbMove;
        private System.Windows.Forms.RadioButton rbDelete;
        private System.Windows.Forms.Button bDeleteSelected;
        private System.Windows.Forms.RadioButton rbSelectEdge;
        private System.Windows.Forms.RadioButton rbDeleteToken;
        private System.Windows.Forms.RadioButton rbNote;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
    }
}
