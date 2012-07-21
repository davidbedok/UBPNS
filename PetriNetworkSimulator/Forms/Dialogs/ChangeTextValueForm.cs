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
    public partial class ChangeTextValueForm : GeneralDialogForm
    {

        public string QuestionLabel
        {
            set { this.labelQuestion.Text = value; }
        }

        public string AnswerValue
        {
            get { return this.tbAnswer.Text; }
            set { 
                this.tbAnswer.Text = value;
            }
        }

        public ChangeTextValueForm()
        {
            InitializeComponent();
        }

        private void ChangeTextValueForm_Shown(object sender, EventArgs e)
        {
            this.tbAnswer.Focus();
        }

        protected override string information()
        {
            return "This form helps you to change the named attribute.";
        }

    }
}
