using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Utils;
using System.Security.Cryptography.X509Certificates;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class CertificateForm : PetriNetworkSimulator.Forms.Common.GeneralDialogForm
    {

        private CertificateWrapper certificate;

        public CertificateWrapper Certificate
        {
            set { 
                if ( value != null ) {
                    this.certificate = value;
                    this.Text = this.certificate.Certificate.Subject;
                    this.tbAlias.Text = this.certificate.Certificate.FriendlyName;
                    this.tbFormat.Text = this.certificate.Certificate.GetFormat();
                    this.tbIssuer.Text = this.certificate.Certificate.Issuer;
                    this.tbNotAfter.Text = this.certificate.Certificate.NotAfter.ToLocalTime().ToString();
                    this.tbNotBefore.Text = this.certificate.Certificate.NotBefore.ToLocalTime().ToString();
                    this.tbSerialNumber.Text = this.certificate.Certificate.SerialNumber;
                    this.tbSignatureAlgorthm.Text = this.certificate.Certificate.SignatureAlgorithm.FriendlyName;
                    this.tbSubject.Text = this.certificate.Certificate.Subject;
                    this.tbThumbprint.Text = this.certificate.Certificate.Thumbprint;
                    this.tbVersion.Text = this.certificate.Certificate.Version.ToString();

                    X509ExtensionCollection extensions = this.certificate.Certificate.Extensions;
                    foreach (X509Extension extension in extensions)
                    {
                        this.lbExtensions.Items.Add(extension.Oid.FriendlyName + " Value - " + extension.Oid.Value + " IsCritical? " + extension.Critical);
                    }
                }
            }
        }

        public CertificateForm()
        {
            InitializeComponent();
            this.hideCancelButton();
        }

        protected override string information()
        {
            return "View details of the selected certificate.";
        }

    }
}
