using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Event;

namespace PetriNetworkSimulator.Forms.Tools
{
    public partial class PetriEventList : PetriNetworkSimulator.Forms.Common.GeneralToolWindow
    {

        protected PetriNetwork network;

        public PetriEventList()
        {
            InitializeComponent();
        }

        public PetriEventList(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();
            this.clear();
        }

        public void clear()
        {
            this.lbEvents.Items.Clear();
            this.lbAttachedItems.Items.Clear();
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    this.lbEvents.Items.Clear();
                    this.network = network;
                    this.lbEvents.Items.AddRange(this.network.StringEvents.ToArray());
                }
                else
                {
                    this.clear();
                }
            }
        }

        private void lbEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( (this.lbEvents.SelectedItem != null) && ( this.network != null ) )
            {
                this.lbAttachedItems.Items.Clear();
                string eventName = (string)this.lbEvents.SelectedItem;
                if (eventName != null)
                {
                    List<PetriEventTransfer> transfer = this.network.getEventsByName(eventName);
                    this.lbAttachedItems.Items.AddRange(transfer.ToArray());
                }
            }
        }

    }
}
