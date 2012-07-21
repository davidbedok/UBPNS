using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Controls
{
    public delegate void PropertyGroupMoveHandler(AbstractNetwork network, NetworkProperty networkProperty, float value);

    public partial class PropertyGroupMoveTool : UserControl
    {
        public event PropertyGroupMoveHandler valueChanged;

        private AbstractNetwork network;
        private NetworkProperty networkProperty;

        private float smallStep;
        private float longStep;

        public AbstractNetwork Network
        {
            get { return this.network; }
            set { this.network = value; }
        }

        public NetworkProperty Property
        {
            get { return this.networkProperty; }
            set { this.networkProperty = value; }
        }

        public AnchorStyles FrameAnchor
        {
            get { return this.Anchor; }
            set { this.Anchor = value; }
        }

        public float SmallStep
        {
            get { return this.smallStep; }
            set { 
                this.smallStep = value;
                this.bSmallMinus.Text = "-" + this.smallStep;
                this.bSmallPlus.Text = "+" + this.smallStep;
            }
        }

        public float LongStep
        {
            get { return this.longStep; }
            set { 
                this.longStep = value;
                this.bBigMinus.Text = "-" + this.longStep;
                this.bBigPlus.Text = "+" + this.longStep;
            }
        }
        public PropertyGroupMoveTool()
        {
            InitializeComponent();
            this.bSmallPlus.Click += new EventHandler(bPlus_Click);
            this.bSmallMinus.Click += new EventHandler(bMinus_Click);
            this.SmallStep = 1;
            this.LongStep = 10;
        }

        private void bMinus_Click(object sender, EventArgs e)
        {
            if (this.valueChanged != null)
            {
                this.valueChanged(this.network, this.networkProperty, -1 * this.smallStep);
            }
        }

        private void bPlus_Click(object sender, EventArgs e)
        {
            if (this.valueChanged != null)
            {
                this.valueChanged(this.network, this.networkProperty, this.smallStep);
            }
        }

        private void bMinusMinus_Click(object sender, EventArgs e)
        {
            if (this.valueChanged != null)
            {
                this.valueChanged(this.network, this.networkProperty, -1 * this.longStep);
            }
        }

        private void bPlusPlus_Click(object sender, EventArgs e)
        {
            if (this.valueChanged != null)
            {
                this.valueChanged(this.network, this.networkProperty, this.longStep);
            }
        }

    }
}
