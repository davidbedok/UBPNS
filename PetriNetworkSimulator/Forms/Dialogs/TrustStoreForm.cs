using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Utils;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class TrustStoreForm : PetriNetworkSimulator.Forms.Common.GeneralDialogForm
    {
        public TrustStoreForm()
        {
            InitializeComponent();
            this.hideCancelButton();

            this.lbTrustStore.Items.AddRange(CryptoHelper.getInstance().TrustStore.ToArray());
        }

        protected override string information()
        {
            return "Here you can see all the valid certificates, which is located in your Trust store directory. You can open Petri nets only if you have the creators public certificate. Details: double click.";
        }

        private void lbTrustStore_DoubleClick(object sender, EventArgs e)
        {
            if (this.lbTrustStore.SelectedItem != null)
            {
                CertificateForm certForm = new CertificateForm();
                certForm.Certificate = (CertificateWrapper)this.lbTrustStore.SelectedItem;
                certForm.ShowDialog();
            }
        }

    }
}
