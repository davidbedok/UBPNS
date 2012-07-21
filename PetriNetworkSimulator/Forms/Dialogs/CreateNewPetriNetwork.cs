using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Exceptions;
using PetriNetworkSimulator.Utils;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class CreateNewPetriNetwork : GeneralDialogForm
    {

        private Random rand;
        private PetriNetwork petriNetwork;

        public PetriNetwork Network
        {
            get { return this.petriNetwork; }
        }

        public CreateNewPetriNetwork(Random rand, int numberOfMdiChildren)
        {
            InitializeComponent();
            this.rand = rand;
            this.nudTimeout.Minimum = PetriNetwork.MIN_TIMEOUT;
            this.tbName.Text = "pn_" + CryptoHelper.getInstance().SubjectCN + "_" + ++numberOfMdiChildren;
            this.nudWidth.Minimum = PetriNetwork.WIDTH_MIN;
            this.nudWidth.Maximum = PetriNetwork.WIDTH_MAX;
            this.nudHeight.Minimum = PetriNetwork.HEIGHT_MIN;
            this.nudHeight.Maximum = PetriNetwork.HEIGHT_MAX;
            this.tbWidth.Minimum = PetriNetwork.WIDTH_MIN;
            this.tbWidth.Maximum = PetriNetwork.WIDTH_MAX;
            this.tbHeight.Minimum = PetriNetwork.HEIGHT_MIN;
            this.tbHeight.Maximum = PetriNetwork.HEIGHT_MAX;
            this.nudDefaultEdgeWeight.Minimum = 1;
            this.nudDefaultEdgeWeight.Maximum = PetriNetwork.MAX_EDGE_WIDTH;
            this.tbWidth.Value = (int)this.nudWidth.Value;
            this.tbHeight.Value = (int)this.nudHeight.Value;
        }

        protected override void dialogOK()
        {
            try
            {
                int width = Convert.ToInt32(this.nudWidth.Value);
                int height = Convert.ToInt32(this.nudHeight.Value);
                int sh_width = Convert.ToInt32(this.nudStateHierarchyWidth.Value);
                int sh_height = Convert.ToInt32(this.nudStateHierarchyHeight.Value);
                int defaultEdgeWeight = Convert.ToInt32(this.nudDefaultEdgeWeight.Value);
                int simulationTimeout = Convert.ToInt32(this.nudTimeout.Value);
                IdentityProvider identityProvider = new IdentityProvider(tbPositionPrefix.Text, tbTransitionPrefix.Text, tbTokenPrefix.Text, tbNotePrefix.Text, 0, 0, 0, 0);
                string certificateSubject = CryptoHelper.getInstance().Subject;
                this.petriNetwork = new PetriNetwork(this.rand, tbName.Text, "", width, height, sh_width, sh_height, identityProvider, tbStatePrefix.Text, defaultEdgeWeight, 0, null, FireRule.getDefault(), simulationTimeout, certificateSubject, DateTime.Now);
            }
            catch (OverflowException e)
            {
                throw new SimApplicationException("Conversion error.",e);
            }
        }

        protected override string information()
        {
            return "Before the new Petri network created, please initialize it's main attributes. All properties can be changed later, but for instance if you know the approximate size of the network at the beginning of the process, these settings may accelerate your work.";
        }

        private void tbWidth_ValueChanged(object sender, EventArgs e)
        {
            this.nudWidth.Value = this.tbWidth.Value;
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            this.tbWidth.Value = (int)this.nudWidth.Value;
        }

        private void tbHeight_ValueChanged(object sender, EventArgs e)
        {
            this.nudHeight.Value = this.tbHeight.Value;
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            this.tbHeight.Value = (int)this.nudHeight.Value;
        }

    }
}
