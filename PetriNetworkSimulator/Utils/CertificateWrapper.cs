using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace PetriNetworkSimulator.Utils
{
    public class CertificateWrapper
    {

        private readonly X509Certificate2 certificate;

        public X509Certificate2 Certificate
        {
            get { return this.certificate; }
        }

        public CertificateWrapper(X509Certificate2 certificate)
        {
            this.certificate = certificate;
        }

        public override string ToString()
        {
            return this.certificate.Subject;
        }

    }
}
