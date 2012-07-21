using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PetriNetworkSimulator.Forms.Common
{
    public partial class GeneralDialogForm : Form
    {
        public GeneralDialogForm()
        {
            InitializeComponent();
            this.rtbInformation.Text = this.information();
        }

        protected virtual string information()
        {
            return "";
        }

        protected virtual void dialogOK()
        {
            // TODO
        }

        protected void hideCancelButton()
        {
            this.bCancel.Visible = false;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.dialogOK();
        }

    }
}
