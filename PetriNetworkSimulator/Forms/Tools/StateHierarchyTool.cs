using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Forms.Dialogs;
using PetriNetworkSimulator.Entities.State.Hierarchy;
using PetriNetworkSimulator.Entities.State.Vector;

namespace PetriNetworkSimulator.Forms.Tools
{
    public partial class StateHierarchyTool : GeneralPictureToolWindow
    {
        private PointF startCoordinatesF;
        private PointF endCoordinatesF;
        private Point startCoordinates;
        private Point endCoordinates;
        private StateVector moveItem;
        private bool isStartMove;

        public event StateActionHandler callAction;
        public event StateVectorHandler callStateVector;

        public StateHierarchyTool()
        {
            InitializeComponent();
        }

        public StateHierarchyTool(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();

            this.pbStateHierarchy.SizeMode = PictureBoxSizeMode.StretchImage;

            this.init(this.pbStateHierarchy, StateHierarchy.DEF_SHT_WIDTH, StateHierarchy.DEF_SHT_HEIGHT);
            this.isStartMove = false;

            this.tsbClear.Tag = StateMatrixAction.CLEARALLSTATE;
            this.ToolInfo = "Use right click to edit states.";
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    if (!network.Equals(this.network))
                    {
                        this.network = network;
                        this.init(this.pbStateHierarchy, this.network.StateHierarchy.Width, this.network.StateHierarchy.Height);
                    }
                    this.toolStripStateHierarchy.Enabled = true;
                    Graphics g = Graphics.FromImage(this.pbStateHierarchy.Image);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.White);
                    network.StateHierarchy.draw(g, this.network.VisualSettings, this.network.ActualStateVector);
                }
                else
                {
                    this.toolStripStateHierarchy.Enabled = false;
                    this.clear(this.pbStateHierarchy);
                }
                this.pbStateHierarchy.Refresh();
            }
        }

        private void pbStateHierarchy_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.network != null)
            {
                PointF point = this.convertPixelToCoord(e.X, e.Y);
                StateVector actual = this.network.StateHierarchy.getVisualItemByCoordinates(point);
                if (actual != null)
                {
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        if (this.callStateVector != null)
                        {
                            this.callStateVector(actual);
                        }
                        this.draw(network);
                    }
                    else if (e.Button.Equals(MouseButtons.Right))
                    {
                        this.stateContextMenu.Tag = actual;
                        this.renameStateToolStripMenuItem.Text = "Rename " + actual.Name + " state";
                        this.stateContextMenu.Show(this.pbStateHierarchy.PointToScreen(new Point(e.X, e.Y)));
                    }
                }
            }
        }

        private void StateHierarchy_Resize(object sender, EventArgs e)
        {
            this.calculateStrech(this.pbStateHierarchy);
        }

        private void pbStateHierarchy_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.network != null)
            {
                if (e.Button.Equals(System.Windows.Forms.MouseButtons.Left))
                {
                    this.startCoordinatesF = this.convertPixelToCoord(e.X, e.Y);
                    this.startCoordinates = this.pbStateHierarchy.PointToScreen(new Point(e.X, e.Y));

                    StateVector item = this.network.StateHierarchy.getVisualItemByCoordinates(this.startCoordinatesF);
                    if ((item != null) && (item is StateVector))
                    {
                        this.moveItem = item;
                        this.endCoordinates = this.startCoordinates;
                        this.isStartMove = true;
                    }
                }
            }
        }

        private void pbStateHierarchy_MouseMove(object sender, MouseEventArgs e)
        {
            if ( (this.isStartMove) && ( this.network != null ) )
            {
                ControlPaint.DrawReversibleLine(this.startCoordinates, this.endCoordinates, this.BackColor);
                this.endCoordinates = this.pbStateHierarchy.PointToScreen(new Point(e.X, e.Y));
                ControlPaint.DrawReversibleLine(this.startCoordinates, this.endCoordinates, this.BackColor);
            }
        }

        private void pbStateHierarchy_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.network != null)
            {
                this.endCoordinatesF = this.convertPixelToCoord(e.X, e.Y);

                if (this.isStartMove)
                {
                    this.moveItem.Origo = this.endCoordinatesF;
                    this.clear(this.pbStateHierarchy);
                    this.draw(this.network);
                }

                this.isStartMove = false;
                this.moveItem = null;
            }
        }

        private void renameStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if ((this.stateContextMenu.Tag != null) && (this.stateContextMenu.Tag is StateVector))
                {
                    StateVector sv = (StateVector)this.stateContextMenu.Tag;

                    ChangeTextValueForm questionForm = new ChangeTextValueForm();
                    questionForm.Text = "Rename " + sv.Name + " state";
                    questionForm.QuestionLabel = "State name: ";
                    questionForm.AnswerValue = sv.Name;
                    if (questionForm.ShowDialog() == DialogResult.OK)
                    {
                        sv.Name = questionForm.AnswerValue;
                    }
                    this.parentForm.refreshToolWindowsPetriNetwork(this.network);
                }
            }
        }

        private void increaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if ((this.stateContextMenu.Tag != null) && (this.stateContextMenu.Tag is StateVector))
                {
                    StateVector sv = (StateVector)this.stateContextMenu.Tag;
                    sv.Radius = sv.Radius + StateVector.RADIUS_OFFSET;
                    this.draw(this.network);
                }
            }
        }

        private void decreaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if ((this.stateContextMenu.Tag != null) && (this.stateContextMenu.Tag is StateVector))
                {
                    StateVector sv = (StateVector)this.stateContextMenu.Tag;
                    sv.Radius = sv.Radius - StateVector.RADIUS_OFFSET;
                    this.draw(this.network);
                }
            }
        }

        private void tsbActionCall_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if (sender is ToolStripButton)
                {
                    ToolStripButton tsb = (ToolStripButton)sender;
                    if ((tsb.Tag != null) && (tsb.Tag is StateMatrixAction))
                    {
                        if (this.callAction != null)
                        {
                            this.callAction((StateMatrixAction)tsb.Tag, this.network);
                        }
                    }
                }
            }
        }

        private void tsbExportAsImage_Click(object sender, EventArgs e)
        {
            this.exportAsImage(this.pbStateHierarchy);
            this.parentForm.writeConsole("Export StateHierarchy of " + this.network.Name + " network as Image.");
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                StateHierarchySetting settings = new StateHierarchySetting();
                settings.StateHierarchyWidth = this.network.StateHierarchy.Width;
                settings.StateHierarchyHeight = this.network.StateHierarchy.Height;
                
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    this.network.StateHierarchy.Width = settings.StateHierarchyWidth;
                    this.network.StateHierarchy.Height = settings.StateHierarchyHeight;
                    this.init(this.pbStateHierarchy, this.network.StateHierarchy.Width, this.network.StateHierarchy.Height);
                    this.draw(this.network);
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if ((this.stateContextMenu.Tag != null) && (this.stateContextMenu.Tag is StateVector))
                {
                    StateVector sv = (StateVector)this.stateContextMenu.Tag;

                    StateVectorProperties questionForm = new StateVectorProperties();
                    questionForm.Text = "Properties of the " + sv.Name + " state";
                    questionForm.MaximumWidth = this.network.StateHierarchy.Width;
                    questionForm.MaximumHeight = this.network.StateHierarchy.Height;
                    questionForm.Input = sv;
                    if (questionForm.ShowDialog() == DialogResult.OK)
                    {
                        
                    }
                    this.parentForm.refreshToolWindowsPetriNetwork(this.network);
                }
            }
        }

        private void deleteStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if ((this.stateContextMenu.Tag != null) && (this.stateContextMenu.Tag is StateVector))
                {
                    StateVector sv = (StateVector)this.stateContextMenu.Tag;
                    this.network.StateHierarchy.removeState(sv);
                    this.draw(this.network);
                }
            }
        }

    }
}
