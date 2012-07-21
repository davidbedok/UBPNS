using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Exceptions;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class StateMatrixSetting : GeneralDialogForm
    {
        private FireRule fireRule;

        public FireRule SelectedFireRule
        {
            get { return this.fireRule; }
            set {
                this.cbFireRule.SelectedItem = value;
                this.fireRule = value; 
            }
        }

        public int SimulationTimeout
        {
            get { return (int)this.nudTimeout.Value; }
            set { this.nudTimeout.Value = value; }
        }

        public StateMatrixSetting()
        {
            InitializeComponent();

            this.cbFireRule.Items.Add(FireRule.RANDOM);
            this.cbFireRule.Items.Add(FireRule.ASC_UNID);
            this.cbFireRule.Items.Add(FireRule.DESC_UNID);
            this.cbFireRule.Items.Add(FireRule.PRIORITY);
            this.cbFireRule.SelectedIndex = 0;

            this.nudTimeout.Minimum = PetriNetwork.MIN_TIMEOUT;
        }

        protected override void dialogOK()
        {
            try
            {
                this.fireRule = (FireRule)this.cbFireRule.SelectedItem;
            }
            catch (OverflowException e)
            {
                throw new SimApplicationException("Cannot convert fireRule.", e);
            }
        }

        protected override string information()
        {
            return "Here you can change the settings of the tokengame. These attributes belong directly to the network (and saved when the network saved).";
        }

    }
}
