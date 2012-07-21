using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Forms.Dialogs;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Exceptions;

namespace PetriNetworkSimulator.Forms.Tools
{

    public delegate void StateVectorHandler(StateVector stateVector);
    public delegate void StateMatrixSettingsHandler(FireRule fireRule, int millisecondsTimeoutForSimulation);
    public delegate void StateActionHandler(StateMatrixAction action, PetriNetwork network);

    public partial class StateMatrix : GeneralPictureToolWindow
    {        
        private const float MARGIN = 5;

        public event StateActionHandler callAction;
        public event StateVectorHandler callStateVector;
        public event StateMatrixSettingsHandler callStateMatrixSettings;

        public StateMatrix()
        {
            InitializeComponent();
        }

        public StateMatrix(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();
            this.width = 300;
            this.height = 200;
            this.pbStateMatrix.Image = new Bitmap(this.width, this.height);
            this.pbStateMatrix.SizeMode = PictureBoxSizeMode.StretchImage;

            this.calculateStrech(this.pbStateMatrix);

            this.tsbFire.Tag = StateMatrixAction.FIRE;
            this.tsbAutoFire.Tag = StateMatrixAction.AUTOFIRE;
            this.tsbStop.Tag = StateMatrixAction.STOPAUTOFIRE;
            this.tsbClear.Tag = StateMatrixAction.CLEAR;

            this.tsbStop.Enabled = false;
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    this.toolStripStateMatrix.Enabled = true;
                    this.network = network;

                    this.tsbFire.Enabled = !this.network.ActiveSimulation;
                    this.tsbAutoFire.Enabled = !this.network.ActiveSimulation;
                    this.tsbStop.Enabled = this.network.ActiveSimulation;

                    List<Position> positions = network.Positions;
                    List<StateVector> stateMatrix = network.StateMatrix;

                    Graphics g = Graphics.FromImage(this.pbStateMatrix.Image);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    Font font = new Font("Arial", 10);

                    Font fontBold = new Font("Arial", 10, FontStyle.Bold);


                    float headerPosWidth = 100;
                    float rowHeight = 15;
                    float maxWidth = 0;
                    float maxHeight = 0;
                    foreach (Position position in positions)
                    {
                        SizeF textSize = g.MeasureString(position.Name, font);
                        if (textSize.Width > maxWidth)
                        {
                            maxWidth = textSize.Width;
                        }
                        if (textSize.Height > maxHeight)
                        {
                            maxHeight = textSize.Height;
                        }
                    }
                    if (maxWidth > 0)
                    {
                        headerPosWidth = maxWidth + StateMatrix.MARGIN * 2;
                        rowHeight = maxHeight;
                    }
                    float stateWidth = 40;
                    float headerHeight = 25;
                    maxWidth = 0;
                    maxHeight = 0;
                    foreach (StateVector stateVector in network.StateMatrix)
                    {
                        SizeF textSize = g.MeasureString(stateVector.Name, (stateVector.Equals(network.ActualStateVector) ? fontBold : font));
                        if (textSize.Width > maxWidth)
                        {
                            maxWidth = textSize.Width;
                        }
                        if (textSize.Height > maxHeight)
                        {
                            maxHeight = textSize.Height;
                        }
                    }
                    if (maxWidth > 0)
                    {
                        stateWidth = maxWidth + StateMatrix.MARGIN * 2;
                        headerHeight = maxHeight + StateMatrix.MARGIN * 2;
                    }

                    this.init(this.pbStateMatrix, (int)(headerPosWidth + network.StateMatrix.Count * stateWidth), (int)(positions.Count * rowHeight + headerHeight));
                    g = Graphics.FromImage(this.pbStateMatrix.Image);
                    g.Clear(Color.White);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                    Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0));
                    Brush brushSelected = new SolidBrush(Color.FromArgb(0, 0, 200));
                    g.DrawRectangle(pen, 0, 0, this.width, this.height);

                    g.DrawLine(pen, headerPosWidth, 0, headerPosWidth, this.height);
                    g.DrawLine(pen, 0, headerHeight, this.width, headerHeight);

                    g.DrawString("M", font, brush, new RectangleF(StateMatrix.MARGIN, StateMatrix.MARGIN, headerPosWidth - 2 * StateMatrix.MARGIN, headerHeight - 2 * StateMatrix.MARGIN));
                    int i = 0;
                    foreach (Position position in positions)
                    {
                        g.DrawString(position.Name, font, brush, new RectangleF(StateMatrix.MARGIN, headerHeight + i * rowHeight, headerPosWidth - 2 * StateMatrix.MARGIN, rowHeight));
                        i++;
                    }

                    int j = 0;
                    foreach (StateVector stateVector in network.StateMatrix)
                    {
                        RectangleF rect = new RectangleF(headerPosWidth + StateMatrix.MARGIN + j * stateWidth, StateMatrix.MARGIN, stateWidth - 2 * StateMatrix.MARGIN, headerHeight - 2 * StateMatrix.MARGIN);
                        stateVector.Map = rect;
                        if (stateVector.Equals(network.ActualStateVector))
                        {
                            g.DrawString(stateVector.Name, fontBold, brushSelected, rect);
                        }
                        else
                        {
                            g.DrawString(stateVector.Name, font, brush, rect);
                        }

                        i = 0;
                        foreach (Position position in positions)
                        {
                            int tokenCount = stateVector.getTokenCountByPositionUnid(position.Unid);
                            g.DrawString(tokenCount.ToString(), font, brush, new RectangleF(headerPosWidth + StateMatrix.MARGIN + (j * stateWidth), headerHeight + i * rowHeight, stateWidth - 2 * StateMatrix.MARGIN, rowHeight));
                            i++;
                        }
                        j++;
                    }
                    i++;


                }
                else
                {
                    this.toolStripStateMatrix.Enabled = false;
                    Graphics g = Graphics.FromImage(this.pbStateMatrix.Image);
                    g.Clear(Color.White);

                    this.tsbFire.Enabled = true;
                    this.tsbAutoFire.Enabled = true;
                    this.tsbStop.Enabled = false;
                }
                this.pbStateMatrix.Refresh();
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
                        StateMatrixAction sma = (StateMatrixAction)tsb.Tag;
                        if (StateMatrixAction.AUTOFIRE.Equals(sma))
                        {
                            this.tsbFire.Enabled = false;
                            this.tsbAutoFire.Enabled = false;
                            this.tsbStop.Enabled = true;
                        }
                        else if (StateMatrixAction.STOPAUTOFIRE.Equals(sma))
                        {
                            this.tsbFire.Enabled = true;
                            this.tsbAutoFire.Enabled = true;
                            this.tsbStop.Enabled = false;
                        }
                        if (this.callAction != null)
                        {
                            this.callAction(sma, this.network);
                        }
                    }
                }
            }
        }

        private void pbStateMatrix_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.network != null)
            {
                PointF point = this.convertPixelToCoord(e.X, e.Y);
                StateVector actual = null;
                foreach (StateVector stateVector in network.StateMatrix)
                {
                    if (stateVector.isInside(point))
                    {
                        actual = stateVector;
                        break;
                    }
                }
                if (actual != null)
                {
                    if (this.callStateVector != null)
                    {
                        this.callStateVector(actual);
                    }
                    this.draw(network);
                }
            }
        }

        private void StateMatrix_Resize(object sender, EventArgs e)
        {
            this.calculateStrech(this.pbStateMatrix);
        }

        private void tsbRules_Click(object sender, EventArgs e)
        {
            if ( network != null ) {
                try
                {
                    StateMatrixSetting settings = new StateMatrixSetting();
                    settings.SelectedFireRule = network.ActualFireRule;
                    settings.SimulationTimeout = network.SimulationTimeout;
                    if (settings.ShowDialog() == DialogResult.OK)
                    {
                        FireRule fireRule = settings.SelectedFireRule;
                        if (this.callStateMatrixSettings != null)
                        {
                            this.callStateMatrixSettings(fireRule, settings.SimulationTimeout);
                        }
                    }
                }
                catch (SimApplicationException ex)
                {
                    this.parentForm.writeConsole("An error happened. " + ex.Message);
                }
            }
        }

        private void tsbExportAsImage_Click(object sender, EventArgs e)
        {
            this.exportAsImage(this.pbStateMatrix);
            this.parentForm.writeConsole("Export StateMatrix of " + this.network.Name + " network as Image.");
        }

        private string[] getStateMatrixText()
        {
            string[] lines = null;
            if (this.network != null)
            {
                List<Position> positions = network.Positions;
                List<StateVector> stateMatrix = network.StateMatrix;
                lines = new string[positions.Count];

                int max = 0;
                foreach (Position position in positions)
                {
                    if (position.Name.Length > max)
                    {
                        max = position.Name.Length;
                    }
                }
                int i = 0;

                foreach (Position position in positions)
                {
                    lines[i] = position.Name.PadRight(max + 1);

                    foreach (StateVector stateVector in stateMatrix)
                    {
                        lines[i] += stateVector.getTokenCountByPositionUnid(position.Unid).ToString().PadRight(3);
                    }

                    i++;
                }
            }
            return lines;
        }

        private string[] getStateMatrixTeX()
        {
            string[] lines = null;
            if (this.network != null)
            {
                List<Position> positions = network.Positions;
                List<StateVector> stateMatrix = network.StateMatrix;
                lines = new string[positions.Count + 5];

                lines[0] = @"\[";
                lines[1] = @"M =";
                lines[2] = @"\left[ {\begin{array}{" + "".PadLeft(stateMatrix.Count, 'c') + "}";
                int i = 3;
                foreach (Position position in positions)
                {
                    int j = 1;
                    foreach (StateVector stateVector in stateMatrix)
                    {
                        lines[i] += " " + stateVector.getTokenCountByPositionUnid(position.Unid).ToString() + (j == stateMatrix.Count ? @" \\" : @" &");
                        j++;
                    }
                    i++;
                }
                lines[i] = @" \end{array} } \right]";
                lines[i + 1] = @"\]";
            }
            return lines;
        }

        private void tsbText_Click(object sender, EventArgs e)
        {
            this.exportAsTextOrTeX(this.getStateMatrixText());
            this.parentForm.writeConsole("Export StateMatrix of " + this.network.Name + " network as Text.");
        }

        private void tsbTeX_Click(object sender, EventArgs e)
        {
            this.exportAsTextOrTeX(this.getStateMatrixTeX());
            this.parentForm.writeConsole("Export StateMatrix of " + this.network.Name + " network as TeX.");
        }

        public void fire(PetriNetwork network)
        {
            this.network = network;
            this.tsbFire.PerformClick();
        }

        public void startSimulation(PetriNetwork network)
        {
            this.network = network;
            this.tsbAutoFire.PerformClick();
        }

        public void stopSimulation(PetriNetwork network)
        {
            this.network = network;
            this.tsbStop.PerformClick();
        }



    }
}
