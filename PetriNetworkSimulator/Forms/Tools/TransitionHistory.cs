using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.History;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Forms.Tools
{
    public delegate void TransitionHistoryHandler(Transition selectedTransition);

    public partial class TransitionHistory : PetriNetworkSimulator.Forms.Common.GeneralToolWindow
    {

        protected PetriNetwork network;
        public event TransitionHistoryHandler selectTransition;

        public TransitionHistory()
        {
            InitializeComponent();
        }

        public TransitionHistory(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();
            this.clear();
        }

        public void clear()
        {
            this.lbTransitionHistory.Items.Clear();
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    this.lbTransitionHistory.Items.Clear();
                    this.network = network;
                    this.lbTransitionHistory.Items.AddRange(this.network.TransitionHistory.ToArray());
                    if (this.lbTransitionHistory.Items.Count > 0)
                    {
                        this.lbTransitionHistory.SelectedIndexChanged -= new EventHandler(lbTransitionHistory_SelectedIndexChanged);
                        this.lbTransitionHistory.SetSelected(this.lbTransitionHistory.Items.Count - 1, true);
                        this.lbTransitionHistory.SetSelected(this.lbTransitionHistory.Items.Count - 1, false);
                        this.lbTransitionHistory.SelectedIndexChanged += new EventHandler(lbTransitionHistory_SelectedIndexChanged);
                    }

                }
                else
                {
                    this.clear();
                }
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                this.network.clearTransitionHistory();
                this.clear();
            }
        }

        private void lbTransitionHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.lbTransitionHistory.SelectedItem != null) && (this.network != null))
            {
                TransitionHistoryItem historyItem = (TransitionHistoryItem)this.lbTransitionHistory.SelectedItem;
                if (historyItem != null)
                {
                    if (this.selectTransition != null)
                    {
                        this.selectTransition(historyItem.Transition);
                    }
                }
            }
        }

    }
}
