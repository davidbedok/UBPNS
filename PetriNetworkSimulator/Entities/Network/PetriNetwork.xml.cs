using System;
using System.Collections.Generic;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Exceptions;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.State.Hierarchy;
using PetriNetworkSimulator.Entities.Event;
using System.Xml.Schema;
using PetriNetworkSimulator.Utils;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace PetriNetworkSimulator.Entities.Network
{
    partial class PetriNetwork
    {

        public string TypeStr
        {
            get { return "PetriNet"; }
        }

        private XmlElement saveSettingsToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "NetworkSettings", PetriXmlHelper.XML_NAMESPACE);
            root.AppendChild(PetriXmlHelper.saveData(doc, this.name, "Name"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.description, "Description"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.certificateSubject, "CertificateSubject"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.lastModificationDate.ToString(PetriXmlHelper.DATE_FORMAT), "LastModificationDate"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.width, "Width"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.height, "Height"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.stateHierarchy.Width, "StateHierarchyWidth"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.stateHierarchy.Height, "StateHierarchyHeight"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.TokenGenNumber, "TokenGenNumber"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.PositionGenNumber, "PositionGenNumber"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.TransitionGenNumber, "TransitionGenNumber"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.NoteGenNumber, "NoteGenNumber"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.stateGenNumber, "StateGenNumber"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.TokenPrefix, "TokenPrefix"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.PositionPrefix, "PositionPrefix"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.TransitionPrefix, "TransitionPrefix"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.identityProvider.NotePrefix, "NotePrefix"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.statePrefix, "StatePrefix"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.defaultEdgeWeight, "DefaultEdgeWeight"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.fireRule.Value, "FireRule"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.simulationTimeout, "SimulationTimeout"));
            return root;
        }

        private XmlElement saveEventsToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "Events", PetriXmlHelper.XML_NAMESPACE);
            foreach (PetriEvent pe in this.PetriEvents.Events){
                if (pe.Adjusted)
                {
                    root.AppendChild(pe.saveEvent(doc, "Event"));
                }
            }
            return root;
        }

        public void saveToXml()
        {
            this.saveToXml(this.fileName);
        }

        public XmlDocument getXmlDocument()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                // doc.PreserveWhitespace = true;

                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                
                /*
                XmlSchemaInclude si = new XmlSchemaInclude();
                si.SchemaLocation = @"e:\workspacecsharp\PetriNetwork\xsd\output\test.pn.xsd";

                XmlSchemaSet xmlss = new XmlSchemaSet();
                xmlss.Add(PetriXmlHelper.XML_NAMESPACE, @"e:\workspacecsharp\PetriNetwork\xsd\output\test.pn.xsd");
                doc.Schemas.Add(xmlss);
                */

                XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "PetriNetwork", PetriXmlHelper.XML_NAMESPACE);
                doc.AppendChild(root);
                root.AppendChild(this.saveSettingsToFile(doc));
                root.AppendChild(this.saveEventsToFile(doc));
                root.AppendChild(this.visualSettings.saveToFile(doc));
                root.AppendChild(this.visibleSettings.saveToFile(doc));
                root.AppendChild(this.saveToFile(doc));
                root.AppendChild(this.stateHierarchy.saveToFile(doc));
                return doc;
            }
            catch (Exception e)
            {
                throw new PetriNetworkException("Cannor save project xml.", e);
            }
        }

        public void saveToXml(string xmlFile, bool setFileName = true)
        {
            try
            {
                this.certificateSubject = CryptoHelper.getInstance().Subject;
                this.lastModificationDate = DateTime.Now;
                XmlDocument doc = this.getXmlDocument();
                CryptoHelper.getInstance().createSignature(doc);
                doc.Save(xmlFile);
                if (setFileName)
                {
                    this.fileName = xmlFile;
                }
            }
            catch (Exception e)
            {
                throw new PetriNetworkException("Cannor save project xml.", e);
            }
        }

        public static int openIntData(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        public static string openStringData(XmlNode node)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        public static DateTime openDateTimeData(XmlNode node)
        {
            return PetriXmlHelper.openDateTimeAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        public static bool openBoolData(XmlNode node)
        {
            return PetriXmlHelper.openBoolAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        public static PetriNetwork openSettingsFromXml(Random rand, XmlNodeList root, string fileName)
        {
            int tokenGenNumber = 0;
            int positionGenNumber = 0;
            int transitionGenNumber = 0;
            int noteGenNumber = 0;
            int stateGenNumber = 0;
            string tokenPrefix = "";
            string positionPrefix = "P";
            string transitionPrefix = "T";
            string notePrefix = "N";
            string statePrefix = "S";
            int defaultEdgeWeight = 1;
            string name = "new";
            string certificateSubject = "";
            DateTime lastModificationDate = DateTime.Now;
            string description = "";
            int width = 640;
            int height = 480;
            int sh_width = 320;
            int sh_height = 240;
            int simulationTimeout = PetriNetwork.DEF_TIMEOUT;
            FireRule fireRule = FireRule.getDefault();
            EventTrunk events = new EventTrunk();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string localName = node.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_SETTINGS_NAMESPACE:
                        switch (localName)
                        {
                            case "Name":
                                name = PetriNetwork.openStringData(node);
                                break;
                            case "CertificateSubject":
                                certificateSubject = PetriNetwork.openStringData(node);
                                break;
                            case "LastModificationDate":
                                lastModificationDate = PetriNetwork.openDateTimeData(node);
                                break;
                            case "Description":
                                description = PetriNetwork.openStringData(node);
                                break;
                            case "Width":
                                width = PetriNetwork.openIntData(node);
                                break;
                            case "Height":
                                height = PetriNetwork.openIntData(node);
                                break;
                            case "StateHierarchyWidth":
                                sh_width = PetriNetwork.openIntData(node);
                                break;
                            case "StateHierarchyHeight":
                                sh_height = PetriNetwork.openIntData(node);
                                break;
                            case "TokenGenNumber":
                                tokenGenNumber = PetriNetwork.openIntData(node);
                                break;
                            case "PositionGenNumber":
                                positionGenNumber = PetriNetwork.openIntData(node);
                                break;
                            case "TransitionGenNumber":
                                transitionGenNumber = PetriNetwork.openIntData(node);
                                break;
                            case "NoteGenNumber":
                                noteGenNumber = PetriNetwork.openIntData(node);
                                break;
                            case "StateGenNumber":
                                stateGenNumber = PetriNetwork.openIntData(node);
                                break;
                            case "TokenPrefix":
                                tokenPrefix = PetriNetwork.openStringData(node);
                                break;
                            case "PositionPrefix":
                                positionPrefix = PetriNetwork.openStringData(node);
                                break;
                            case "TransitionPrefix":
                                transitionPrefix = PetriNetwork.openStringData(node);
                                break;
                            case "NotePrefix":
                                notePrefix = PetriNetwork.openStringData(node);
                                break;
                            case "StatePrefix":
                                statePrefix = PetriNetwork.openStringData(node);
                                break;
                            case "DefaultEdgeWeight":
                                defaultEdgeWeight = PetriNetwork.openIntData(node);
                                break;
                            case "FireRule":
                                fireRule = FireRule.getEnumByValue(PetriNetwork.openStringData(node));
                                break;
                            case "SimulationTimeout":
                                simulationTimeout = PetriNetwork.openIntData(node);
                                break;
                        }
                        break;
                }
            }
            IdentityProvider identityProvider = new IdentityProvider(positionPrefix, transitionPrefix, tokenPrefix, notePrefix,tokenGenNumber, positionGenNumber, transitionGenNumber, noteGenNumber);
            return new PetriNetwork(rand, name, description, width, height, sh_width, sh_height, identityProvider, statePrefix, defaultEdgeWeight, stateGenNumber, fileName, fireRule, simulationTimeout, certificateSubject, lastModificationDate);
        }

        public static PetriNetwork openFromXml(Random rand, string xmlFile)
        {
            PetriNetwork network = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            XmlNodeList networkSettingsList = doc.GetElementsByTagName("NetworkSettings", PetriXmlHelper.XML_NAMESPACE);
            foreach (XmlNode node in networkSettingsList)
            {
                network = PetriNetwork.openSettingsFromXml(rand, node.ChildNodes, xmlFile);
            }
            if (network != null)
            {
                XmlNodeList eventsList = doc.GetElementsByTagName("Events", PetriXmlHelper.XML_NAMESPACE);
                foreach (XmlNode node in eventsList)
                {
                    network.PetriEvents.addEvent(PetriEvent.openEvents(node.ChildNodes, "Event"));
                }
                XmlNodeList visualSettingsList = doc.GetElementsByTagName("VisualSettings", PetriXmlHelper.XML_NAMESPACE);
                foreach (XmlNode node in visualSettingsList)
                {
                    network.visualSettings = NetworkVisualSettings.openFromXml(node.ChildNodes);
                }
                XmlNodeList visibleSettingsList = doc.GetElementsByTagName("VisibleSettings", PetriXmlHelper.XML_NAMESPACE);
                foreach (XmlNode node in visibleSettingsList)
                {
                    network.visibleSettings = NetworkVisibleSettings.openFromXml(node.ChildNodes);
                }
                XmlNodeList networkList = doc.GetElementsByTagName("Network", PetriXmlHelper.XML_NAMESPACE);
                foreach (XmlNode node in networkList)
                {
                    XmlNodeList networkChildren = node.ChildNodes;
                    foreach (XmlNode nodeChild in networkChildren)
                    {
                        string namespaceUri = nodeChild.NamespaceURI;
                        string name = nodeChild.LocalName;
                        switch (namespaceUri)
                        {
                            case PetriXmlHelper.XML_NETWORKITEM_NAMESPACE:
                                switch (name)
                                {
                                    case "NetworkItems":
                                        network.items = AbstractNetwork.openItemsFromXml(nodeChild.ChildNodes);
                                        break;
                                    case "Edges":
                                        network.edges = AbstractNetwork.openEdgesFromXml(nodeChild.ChildNodes, network.items);
                                        break;
                                    case "Notes":
                                        List<AbstractItem> itemsAndEdges = new List<AbstractItem>();
                                        itemsAndEdges.AddRange(network.items);
                                        itemsAndEdges.AddRange(network.edges);
                                        network.items.AddRange(AbstractNetwork.openNotesFromXml(nodeChild.ChildNodes, itemsAndEdges));
                                        break;
                                }
                                break;
                        }
                    }
                }
                XmlNodeList stateHierarchyList = doc.GetElementsByTagName("StateHierarchy", PetriXmlHelper.XML_NAMESPACE);
                foreach (XmlNode node in stateHierarchyList)
                {
                    XmlNodeList networkChildren = node.ChildNodes;
                    StateHierarchy stateHierarchy = new StateHierarchy(rand, network.StateHierarchy.Width, network.StateHierarchy.Height);
                    foreach (XmlNode nodeChild in networkChildren)
                    {
                        string namespaceUri = nodeChild.NamespaceURI;
                        string name = nodeChild.LocalName;
                        switch (namespaceUri)
                        {
                            case PetriXmlHelper.XML_STATEHIERARCHY_NAMESPACE:
                                switch (name)
                                {
                                    case "States":
                                        stateHierarchy.addStates(StateHierarchy.openStatesFromXml(nodeChild.ChildNodes, network.Tokens));
                                        break;
                                    case "Edges":
                                        stateHierarchy.addEdges(StateHierarchy.openEdgesFromXml(nodeChild.ChildNodes, stateHierarchy.States));
                                        break;
                                }
                                break;
                        }
                    }
                    network.StateHierarchy = stateHierarchy;
                }
                network.setNextUnid();
            }

            if (network == null)
            {
                throw new PetriNetworkException("Network is null.");
            }
            if ( ( network.certificateSubject == null ) || ( "".Equals(network.certificateSubject) ) ) {
                throw new PetriNetworkException("Network hasn't got certificate subject information.");
            }
            RSACryptoServiceProvider publicKey = CryptoHelper.getInstance().getCertificateFromTrustStore(network.certificateSubject);
            if (publicKey == null)
            {
                throw new CryptoException("The certificate of the network (" + network.certificateSubject + ") is not trusted certificate.");
            }
            bool validSignature = CryptoHelper.checkSignature(doc, publicKey);
            if (!validSignature)
            {
                throw new CryptoException("The signature of the network is not valid. The file was modified.");
            }
            return network;
        }

    }
}
