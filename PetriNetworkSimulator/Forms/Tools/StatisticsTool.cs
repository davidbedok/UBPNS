using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Forms.Tools
{
    public partial class StatisticsTool : PetriNetworkSimulator.Forms.Common.GeneralToolWindow
    {

        protected PetriNetwork network;

        public StatisticsTool()
        {
            InitializeComponent();
        }

        public StatisticsTool(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();
            this.clear();
        }

        public void clear()
        {
            this.tbCount.Text = "";
            this.tbValue.Text = "";
            this.tbMinimum.Text = "";
            this.tbMaximum.Text = "";
            this.tbAverage.Text = "";
            this.bInit.Tag = null;
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    this.network = network;
                    List<Position> positions = this.network.Positions;
                    List<Transition> transitions = this.network.Transitions;
                    List<StateVector> states = this.network.StateHierarchy.States;

                    this.lbItems.Items.Clear();
                    this.lbItems.Items.AddRange(positions.ToArray());
                    this.lbItems.Items.AddRange(transitions.ToArray());
                    this.lbItems.Items.AddRange(states.ToArray());
                }
                else
                {
                    this.lbItems.Items.Clear();
                }
            }
        }

        private void showStatistics()
        {
            if (this.lbItems.SelectedItem != null)
            {
                this.clear();
                GeneralStatistics statistics = null;
                bool isPosition = false;
                if (this.lbItems.SelectedItem is Position)
                {
                    Position position = (Position)this.lbItems.SelectedItem;
                    statistics = position.Statistics;
                    this.bInit.Tag = position;
                    isPosition = true;
                }
                else if (this.lbItems.SelectedItem is Transition)
                {
                    Transition transition = (Transition)this.lbItems.SelectedItem;
                    statistics = transition.Statistics;
                    this.bInit.Tag = transition;
                }
                else if (this.lbItems.SelectedItem is StateVector)
                {
                    StateVector state = (StateVector)this.lbItems.SelectedItem;
                    statistics = state.Statistics;
                    this.bInit.Tag = state;
                }
                if (statistics != null)
                {
                    this.tbCount.Text = statistics.Count.ToString();
                    if (isPosition)
                    {
                        this.tbValue.Text = statistics.Value.ToString();
                        this.tbMinimum.Text = statistics.Minimum.ToString();
                        this.tbMaximum.Text = statistics.Maximum.ToString();
                        this.tbAverage.Text = statistics.Average.ToString();
                    }
                }
            }
        }

        private void lbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.showStatistics();   
        }

        private void bInit_Click(object sender, EventArgs e)
        {
            if (this.bInit.Tag != null)
            {
                GeneralStatistics statistics = null;
                if (this.bInit.Tag is Position)
                {
                    Position position = (Position)this.bInit.Tag;
                    statistics = position.Statistics;
                }
                else if (this.bInit.Tag is Transition)
                {
                    Transition transition = (Transition)this.bInit.Tag;
                    statistics = transition.Statistics;
                }
                else if (this.bInit.Tag is StateVector)
                {
                    StateVector state = (StateVector)this.bInit.Tag;
                    statistics = state.Statistics;
                }
                statistics.init();
                this.showStatistics();
            }
            this.lbItems.Focus();
        }

        private void bClearAllStatistics_Click(object sender, EventArgs e)
        {
            if (this.network != null)
            {
                this.network.initStatistics();
                this.showStatistics();
                this.lbItems.Focus();
            }
        }

    }
}
