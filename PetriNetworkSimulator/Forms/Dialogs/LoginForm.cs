using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Exceptions;
using PetriNetworkSimulator.Forms.Common;
using System.Reflection;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class LoginForm : Form, IUpdateCulture
    {
        public LoginForm()
        {
            InitializeComponent();
            this.openKeyStoreDialog.Filter = "Public-Key Cryptography Standards 12 (*.pfx)|*.pfx|Public-Key Cryptography Standards 12 (*.p12)|*.p12";
            this.updateCulture();
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ( CryptoHelper.isValidPrivateStore(tbStore.Text, tbStorePassword.Text) )
                {
                    CryptoHelper.getInstance().initPrivateStoreAndKey(tbStore.Text, tbStorePassword.Text); 
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (CryptoException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            if (this.openKeyStoreDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbStore.Text = this.openKeyStoreDialog.FileName;
                this.tbStorePassword.Focus();
            }
        }

        public void updateCulture()
        {
            this.Text = CultureHelper.getInstance().RM.GetString("applicationTitle") + " v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
}
