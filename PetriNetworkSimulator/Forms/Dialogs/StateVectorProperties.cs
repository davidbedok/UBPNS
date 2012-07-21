using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class StateVectorProperties : PetriNetworkSimulator.Forms.Common.GeneralDialogForm
    {

        private StateVector input;

        public StateVector Input
        {
            set
            {
                this.input = value;
                this.setValues();
            }
        }

        public int MaximumWidth
        {
            set { this.nudOrigoX.Maximum = value; }
        }

        public int MaximumHeight
        {
            set { this.nudOrigoY.Maximum = value; }
        }

        private void setValues()
        {
            this.tbName.Text = this.input.Name;
            this.tbUnid.Text = this.input.Unid.ToString();
            this.nudOrigoX.Value = (decimal)this.input.Origo.X;
            this.nudOrigoY.Value = (decimal)this.input.Origo.Y;
            this.nudRadius.Value = (decimal)this.input.Radius;
            this.tbPreActivate.Text = this.input.PetriEvents.getEvent(EventType.PREACTIVATE).Name;
            this.tbPostActivate.Text = this.input.PetriEvents.getEvent(EventType.POSTACTIVATE).Name;
        }

        public StateVectorProperties()
        {
            InitializeComponent();
        }

        protected override void dialogOK()
        {
            this.input.Name = this.tbName.Text;
            this.input.Origo = new PointF((float)this.nudOrigoX.Value, (float)this.nudOrigoY.Value);
            this.input.Radius = (float)this.nudRadius.Value;
            this.input.PetriEvents.modifyEvent(EventType.PREACTIVATE, this.tbPreActivate.Text);
            this.input.PetriEvents.modifyEvent(EventType.POSTACTIVATE, this.tbPostActivate.Text);
        }

        protected override string information()
        {
            return "This form helps you to change properties of the selected StateVector.";
        }
    }
}
