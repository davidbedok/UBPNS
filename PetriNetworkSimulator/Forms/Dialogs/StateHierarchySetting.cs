using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class StateHierarchySetting : GeneralDialogForm
    {

        public int StateHierarchyWidth
        {
            get { return Convert.ToInt32(this.nudStateHierarchyWidth.Value); }
            set
            {
                this.nudStateHierarchyWidth.Value = value; 
            }
        }

        public int StateHierarchyHeight
        {
            get { return Convert.ToInt32(this.nudStateHierarchyHeight.Value); }
            set
            {
                this.nudStateHierarchyHeight.Value = value;
            }
        }

        public StateHierarchySetting()
        {
            InitializeComponent();
        }

        protected override void dialogOK()
        {
            //
        }

        protected override string information()
        {
            return "Here you can change the settings of the State Hierarchy. These attributes belong directly to the network (and saved when the network saved).";
        }

    }
}
