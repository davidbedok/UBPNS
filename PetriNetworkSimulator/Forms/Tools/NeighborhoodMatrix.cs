using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.State.Hierarchy;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Forms.Tools
{
    public partial class NeighborhoodMatrix : GeneralPictureToolWindow
    {
        private const float MARGIN = 2;

        public NeighborhoodMatrix(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();

            this.pbMinusWeight.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pbPlusWeight.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pbNeighborhoodMatrix.SizeMode = PictureBoxSizeMode.StretchImage;

            this.initNeighborhoodMatrix(StateHierarchy.DEF_SHT_WIDTH, StateHierarchy.DEF_SHT_HEIGHT);

            this.tpMinusWeight.Tag = this.pbMinusWeight;
            this.tpPlusWeight.Tag = this.pbPlusWeight;
            this.tpNeighborhoodMatrix.Tag = this.pbNeighborhoodMatrix;
        }

        private void initNeighborhoodMatrix(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.pbMinusWeight.Image = new Bitmap(this.width, this.height);
            this.pbPlusWeight.Image = new Bitmap(this.width, this.height);
            this.pbNeighborhoodMatrix.Image = new Bitmap(this.width, this.height);
            this.calculateStrech(this.pbMinusWeight);
        }

        public void sideDraw(Graphics g, Pen pen, float traWidth, float rowHeight, string corner, Font font, Brush brush, StringFormat drawFormat, List<Transition> transitions)
        {
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(pen, traWidth, 0, traWidth, this.height);
            g.DrawLine(pen, 0, rowHeight, this.width, rowHeight);
            g.DrawString(corner, font, brush, new RectangleF(0, 0, traWidth, rowHeight), drawFormat);
            int i = 0;
            foreach (Transition transition in transitions)
            {
                g.DrawString(transition.Name, font, brush, new RectangleF(0, rowHeight + i * rowHeight, traWidth, rowHeight), drawFormat);
                i++;
            }
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    this.toolStripNeighborhoodMatrix.Enabled = true;
                    this.network = network;

                    List<Position> positions = network.Positions;
                    List<Transition> transitions = network.Transitions;

                    if ((positions.Count > 0) && (transitions.Count > 0))
                    {

                        Graphics gmw = Graphics.FromImage(this.pbMinusWeight.Image);
                        Font font = new Font("Arial", 10);
                        Font fontBold = new Font("Arial", 10, FontStyle.Bold);

                        float posWidth = 0;
                        float rowHeight = 15;
                        float maxWidth = 0;
                        float maxHeight = 0;
                        foreach (Position position in positions)
                        {
                            SizeF textSize = gmw.MeasureString(position.Name, font);
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
                            posWidth = maxWidth + NeighborhoodMatrix.MARGIN * 2;
                            rowHeight = maxHeight + NeighborhoodMatrix.MARGIN * 2;
                        }

                        float traWidth = 0;
                        maxWidth = 0;
                        maxHeight = 0;
                        foreach (Transition transition in transitions)
                        {
                            SizeF textSize = gmw.MeasureString(transition.Name, font);
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
                            traWidth = maxWidth + NeighborhoodMatrix.MARGIN * 2;
                        }

                        this.initNeighborhoodMatrix((int)(posWidth * positions.Count + traWidth), (int)((transitions.Count + 1) * rowHeight));
                        gmw = Graphics.FromImage(this.pbMinusWeight.Image);
                        Graphics gpw = Graphics.FromImage(this.pbPlusWeight.Image);
                        Graphics gnm = Graphics.FromImage(this.pbNeighborhoodMatrix.Image);
                        Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                        Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0));
                        Brush brushSelected = new SolidBrush(Color.FromArgb(0, 0, 200));
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;
                        drawFormat.LineAlignment = StringAlignment.Center;
                        this.sideDraw(gmw, pen, traWidth, rowHeight, "W-", font, brush, drawFormat, transitions);
                        this.sideDraw(gpw, pen, traWidth, rowHeight, "W+", font, brush, drawFormat, transitions);
                        this.sideDraw(gnm, pen, traWidth, rowHeight, "W", font, brush, drawFormat, transitions);

                        int i = 0;
                        foreach (Position position in positions)
                        {
                            RectangleF prect = new RectangleF(traWidth + i * posWidth, 0, posWidth, rowHeight);
                            gmw.DrawString(position.Name, font, brush, prect, drawFormat);
                            gpw.DrawString(position.Name, font, brush, prect, drawFormat);
                            gnm.DrawString(position.Name, font, brush, prect, drawFormat);

                            int j = 0;
                            foreach (Transition transition in transitions)
                            {
                                int minusWeight = this.network.getMinusWeight(position, transition);
                                int plusWeight = this.network.getPlusWeight(position, transition);
                                RectangleF rect = new RectangleF(traWidth + (i * posWidth), rowHeight + j * rowHeight, posWidth, rowHeight);
                                gmw.DrawString(minusWeight.ToString(), font, brush, rect, drawFormat);
                                gpw.DrawString(plusWeight.ToString(), font, brush, rect, drawFormat);
                                gnm.DrawString((plusWeight - minusWeight).ToString(), font, brush, rect, drawFormat);
                                j++;
                            }

                            i++;
                        }

                    }
                    else
                    {
                        this.clear();
                    }
                }
                else
                {
                    this.toolStripNeighborhoodMatrix.Enabled = false;
                    this.clear();
                }
                this.pbMinusWeight.Refresh();
            }
        }

        private void clear()
        {
            Graphics g = Graphics.FromImage(this.pbMinusWeight.Image);
            g.Clear(Color.White);
            g = Graphics.FromImage(this.pbPlusWeight.Image);
            g.Clear(Color.White);
            g = Graphics.FromImage(this.pbNeighborhoodMatrix.Image);
            g.Clear(Color.White);
        }

        private void NeighborhoodMatrix_Resize(object sender, EventArgs e)
        {
            this.calculateStrech(this.pbMinusWeight);
        }

        private void exportAsImage_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                if (this.tcNeighborhoodMatrix.SelectedTab.Tag is PictureBox)
                {
                    this.exportAsImage((PictureBox)this.tcNeighborhoodMatrix.SelectedTab.Tag);
                    this.parentForm.writeConsole("Export NeighborhoodMatrix of " + this.network.Name + " network as Image.");
                }
            }
        }

        private string[] getNeighborhoodMatrixText()
        {
            string[] lines = null;
            if (this.network != null)
            {
                int type = 0;
                string typeStr = "W";
                if (this.tcNeighborhoodMatrix.SelectedTab.Equals(tpMinusWeight))
                {
                    type = -1;
                    typeStr = "W-";
                }
                else if (this.tcNeighborhoodMatrix.SelectedTab.Equals(tpPlusWeight))
                {
                    type = 1;
                    typeStr = "W+";
                }
                else if (this.tcNeighborhoodMatrix.SelectedTab.Equals(tpNeighborhoodMatrix))
                {
                    type = 0;
                    typeStr = "W";
                }


                List<Position> positions = network.Positions;
                List<Transition> transitions = network.Transitions;
                lines = new string[transitions.Count + 1];

                int maxPosLength = 0;
                foreach (Position position in positions)
                {
                    if (position.Name.Length > maxPosLength)
                    {
                        maxPosLength = position.Name.Length;
                    }
                }
                int maxTraLength = 0;
                foreach (Transition transition in transitions)
                {
                    if (transition.Name.Length > maxTraLength)
                    {
                        maxTraLength = transition.Name.Length;
                    }
                }

                int i = 0;

                lines[i] = typeStr.PadRight(maxTraLength + 1);
                foreach (Position position in positions)
                {
                    lines[i] += position.Name.PadRight(maxPosLength + 1);
                }
                i++;

                foreach (Transition transition in transitions)
                {
                    lines[i] = transition.Name.PadRight(maxTraLength + 1);
                    foreach (Position position in positions)
                    {
                        int weight = 0;
                        switch (type){
                            case -1:
                                weight = this.network.getMinusWeight(position, transition);
                                break;
                            case 0:
                                weight = this.network.getPlusWeight(position, transition) - this.network.getMinusWeight(position, transition);
                                break;
                            case 1:
                                weight = this.network.getPlusWeight(position, transition);
                                break;
                        }
                        lines[i] += weight.ToString().PadRight(maxPosLength + 1);
                    }

                    i++;
                }
            }
            return lines;
        }

        private string[] getNeighborhoodMatrixTeX()
        {
            string[] lines = null;
            if (this.network != null)
            {
                int type = 0;
                string typeStr = "W";
                if (this.tcNeighborhoodMatrix.SelectedTab.Equals(tpMinusWeight))
                {
                    type = -1;
                    typeStr = "W-";
                }
                else if (this.tcNeighborhoodMatrix.SelectedTab.Equals(tpPlusWeight))
                {
                    type = 1;
                    typeStr = "W+";
                }
                else if (this.tcNeighborhoodMatrix.SelectedTab.Equals(tpNeighborhoodMatrix))
                {
                    type = 0;
                    typeStr = "W";
                }


                List<Position> positions = network.Positions;
                List<Transition> transitions = network.Transitions;
                lines = new string[transitions.Count + 6];

                lines[0] = @"\[";
                lines[1] = @"M =";
                lines[2] = @"\left[ {\begin{array}{" + "".PadLeft(positions.Count + 1, 'c') + "}";

                int maxPosLength = 0;
                foreach (Position position in positions)
                {
                    if (position.Name.Length > maxPosLength)
                    {
                        maxPosLength = position.Name.Length;
                    }
                }
                int maxTraLength = 0;
                foreach (Transition transition in transitions)
                {
                    if (transition.Name.Length > maxTraLength)
                    {
                        maxTraLength = transition.Name.Length;
                    }
                }

                int i = 3;

                lines[i] = typeStr.PadRight(maxTraLength + 1) + @" &";
                int j = 1;
                foreach (Position position in positions)
                {
                    lines[i] += " " + position.Name.PadRight(maxPosLength + 1) + (j == (positions.Count) ? @" \\" : @" &");
                    j++;
                }
                i++;

                foreach (Transition transition in transitions)
                {
                    lines[i] = transition.Name.PadRight(maxTraLength + 1) + @" &";
                    j = 1;
                    foreach (Position position in positions)
                    {
                        int weight = 0;
                        switch (type)
                        {
                            case -1:
                                weight = this.network.getMinusWeight(position, transition);
                                break;
                            case 0:
                                weight = this.network.getPlusWeight(position, transition) - this.network.getMinusWeight(position, transition);
                                break;
                            case 1:
                                weight = this.network.getPlusWeight(position, transition);
                                break;
                        }
                        lines[i] += " " + weight.ToString() + (j == (positions.Count) ? @" \\" : @" &");
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
            this.exportAsTextOrTeX(this.getNeighborhoodMatrixText());
            this.parentForm.writeConsole("Export NeighborhoodMatrix of " + this.network.Name + " network as Text.");
        }

        private void tsbTeX_Click(object sender, EventArgs e)
        {
            this.exportAsTextOrTeX(this.getNeighborhoodMatrixTeX());
            this.parentForm.writeConsole("Export NeighborhoodMatrix of " + this.network.Name + " network as TeX.");
        }

    }
}
