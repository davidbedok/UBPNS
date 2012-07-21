using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using PetriNetworkSimulator.Exceptions;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.IO;

namespace PetriNetworkSimulator.Utils
{

    public delegate void CryptoHandler(string message);

    public class CryptoHelper
    {
        private static readonly string PRIVATEKEY_ISSUER = "E=petrisubca@qwaevisz.hu, CN=Petri Universitas Budensis Sub CA, OU=NIK, O=UNI-OBUDA, S=Hungary, C=HU";
        private static readonly string FRIENDLYNAME = "petri";

        private static readonly string TRUST_STORE_DIR = "truststore";
        private static readonly string PUBLIC_STORE_PASSWORD = "";
                                                 
        private static CryptoHelper instance;

        private X509Certificate2 privateStore;
        private RSACryptoServiceProvider privateKey;
        private List<CertificateWrapper> trustStore;

        public event CryptoHandler cryptoEvent;

        public List<CertificateWrapper> TrustStore
        {
            get { return this.trustStore; }
        }

        public string Subject
        {
            get {
                string ret = "Private Key is not set (not possible to save)";
                if ((this.privateStore != null) && (this.privateKey != null))
                {
                    ret = this.privateStore.Subject;
                }
                return ret;
            }
        }

        public string SubjectCN
        {
            get
            {
                string ret = "anonymous";
                if ((this.privateStore != null) && (this.privateKey != null))
                {
                    string fullDN = this.privateStore.SubjectName.Name;
                    if (fullDN != null)
                    {
                        string[] partsOfDN = fullDN.Split(',');
                        for (int i = 0; i < partsOfDN.Length; i++)
                        {
                            partsOfDN[i] = partsOfDN[i].Trim();
                        }
                        for (int i = 0; i < partsOfDN.Length; i++)
                        {
                            int indexOfCN = partsOfDN[i].IndexOf("CN=");
                            if (indexOfCN >= 0)
                            {
                                ret = partsOfDN[i].Replace("CN=", "");
                                ret = ret.Replace(" ", "_");
                                ret = ret.ToLower();
                            }
                        }
                    }
                }
                return ret;
            }
        }

        public bool HasKey
        {
            get {
                bool ret = false;
                if ((this.privateStore != null) && (this.privateKey != null))
                {
                    ret = true;
                }
                return ret;
            }
        }

        private static bool isValidCertificate(X509Certificate2 certificate)
        {
            bool valid = false;
            if (certificate != null)
            {
                int notBefore = DateTime.Compare(DateTime.Now, certificate.NotBefore);
                int notAfter = DateTime.Compare(DateTime.Now, certificate.NotAfter);
                if ((notBefore > 0) && (notAfter < 0))
                {
                    valid = true;
                }
            }
            return valid;
        }

        public static bool isValidPrivateStore( string filename, string storepass )
        {
            if ( ( filename == null ) || ( "".Equals(filename) )) 
            {
                throw new CryptoException("Please browse a PKCS12 store or click the 'I haven't got key' button.");
            }
            X509Certificate2 privatestore = new X509Certificate2(filename, storepass, X509KeyStorageFlags.Exportable);
            if (privatestore == null)
            {
                throw new CryptoException("The store (" + filename + ") is not valid.");
            }
            if (!privatestore.HasPrivateKey)
            {
                throw new CryptoException("The store (" + filename + ") hasn't got private key.");
            }
            if (!CryptoHelper.isValidCertificate(privatestore))
            {
                throw new CryptoException("The store (" + filename + ") is expired.");
            }
            if ( (privatestore.Version != 3) || (!privatestore.GetFormat().Equals("X509") ) )
            {
                throw new CryptoException("The certificate (" + privatestore.Subject + ") must be X.509 v3.");
            }
            if (!(privatestore.Issuer.Equals(CryptoHelper.PRIVATEKEY_ISSUER)))
            {
                throw new CryptoException("The common name of the certificate (" + privatestore.Subject + ") must be \"Petri Universitas Budensis Sub CA\".");
            }
            if (!(privatestore.FriendlyName.Equals(CryptoHelper.FRIENDLYNAME)))
            {
                throw new CryptoException("The friendly name of the certificate (" + privatestore.Subject + ") must be \"" + CryptoHelper.FRIENDLYNAME + "\".");
            }
            return true;
        }

        public void initPrivateStoreAndKey(string filename, string storepass)
        {
            this.privateStore = new X509Certificate2(filename, storepass, X509KeyStorageFlags.Exportable);
            this.privateKey = (RSACryptoServiceProvider)this.privateStore.PrivateKey;
        }

        public void initTrustStore()
        {
            this.trustStore = new List<CertificateWrapper>();
            string[] iFiles = Directory.GetFiles(CryptoHelper.TRUST_STORE_DIR, "*.pfx");
            for (int i = 0; i < iFiles.Length; i++)
            {
                try
                {
                    X509Certificate2 cert = new X509Certificate2(iFiles[i], CryptoHelper.PUBLIC_STORE_PASSWORD, X509KeyStorageFlags.Exportable);
                    if (!CryptoHelper.isValidCertificate(cert))
                    {
                        throw new CryptoException("Certificate (" + cert.Subject + ") was expired.");
                    }
                    if ((cert.Version != 3) || (!cert.GetFormat().Equals("X509")))
                    {
                        throw new CryptoException("The certificate (" + cert.Subject + ") must be X.509 v3.");
                    }
                    if (!(cert.FriendlyName.Equals(CryptoHelper.FRIENDLYNAME)))
                    {
                        throw new CryptoException("The friendly name of the certificate (" + cert.Subject + ") must be \"" + CryptoHelper.FRIENDLYNAME + "\".");
                    }
                    this.trustStore.Add(new CertificateWrapper(cert));
                }
                catch (Exception e)
                {
                    if (this.cryptoEvent != null)
                    {
                        this.cryptoEvent("Error while loading certificate (" + Path.GetFileName(iFiles[i]) + "). Details: " + e.Message);
                    }
                }
            }
        }

        public RSACryptoServiceProvider getCertificateFromTrustStore(string subject)
        {
            RSACryptoServiceProvider ret = null;
            if ((subject != null) && (!"".Equals(subject)))
            {
                foreach (CertificateWrapper certificate in this.trustStore)
                {
                    if (certificate.Certificate.Subject.Equals(subject))
                    {
                        ret = (RSACryptoServiceProvider)certificate.Certificate.PublicKey.Key;
                        break;
                    }
                }
            }
            return ret;
        }

        public void createSignature(XmlDocument xmlDoc)
        {
            if ((this.HasKey) && (xmlDoc != null))
            {
                SignedXml signedXml = new SignedXml(xmlDoc);
                signedXml.SigningKey = this.privateKey;
                Reference reference = new Reference();
                reference.Uri = "";
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);
                signedXml.AddReference(reference);
                signedXml.ComputeSignature();
                XmlElement xmlDigitalSignature = signedXml.GetXml();
                xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            }
        }

        public static bool checkSignature(XmlDocument xmlDoc, RSA publicKey)
        {
            SignedXml signedXml = new SignedXml(xmlDoc);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }
            signedXml.LoadXml((XmlElement)nodeList[0]);
            return signedXml.CheckSignature(publicKey);
        }

        private CryptoHelper()
        {
            
        }

        public static CryptoHelper getInstance()
        {
            if (CryptoHelper.instance == null)
            {
                CryptoHelper.instance = new CryptoHelper();
            }
            return CryptoHelper.instance;
        }

    }
}
