using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using System.Xml;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Model.NoteItem;
using PetriNetworkLibrary.Model.State;
using PetriNetworkLibrary.Event;
using System.IO;

namespace PetriNetworkLibrary.Model.Network
{
    public partial class PetriNetwork
    {

        public static PetriNetwork openFromXml(Random rand, string xmlFile)
        {
            PetriNetwork network = null;
            if (File.Exists(xmlFile))
            {
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
                        network.EventTrunk.addEvents(PetriEvent.openEvents(node.ChildNodes, "Event"));
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
                                            network.items.AddRange(PetriNetwork.openItemsFromXml(nodeChild.ChildNodes));
                                            break;
                                        case "Edges":
                                            network.items.AddRange(PetriNetwork.openEdgesFromXml(nodeChild.ChildNodes, network.EventDrivenItems));
                                            break;
                                        case "Notes":
                                            network.items.AddRange(PetriNetwork.openNotesFromXml(nodeChild.ChildNodes, network.items));
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
                        StateHierarchy stateHierarchy = new StateHierarchy();
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
                        network.stateHierarchy = stateHierarchy;
                    }
                    network.setNextUnid();
                }
            }
            return network;
        }

        private static int openIntData(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        private static string openStringData(XmlNode node)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        private static DateTime openDateTimeData(XmlNode node)
        {
            return PetriXmlHelper.openDateTimeAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        private static bool openBoolData(XmlNode node)
        {
            return PetriXmlHelper.openBoolAttributeFromNode(node, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
        }

        private static PetriNetwork openSettingsFromXml(Random rand, XmlNodeList root, string fileName)
        {
            string name = "new";
            string certificateSubject = "";
            DateTime lastModificationDate = DateTime.Now;
            string description = "";
            int simulationTimeout = PetriNetwork.DEF_TIMEOUT;
            FireRule fireRule = FireRule.RANDOM;
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
                            case "FireRule":
                                fireRule = (FireRule)Enum.Parse(typeof(FireRule), PetriNetwork.openStringData(node));
                                break;
                            case "SimulationTimeout":
                                simulationTimeout = PetriNetwork.openIntData(node);
                                break;
                        }
                        break;
                }
            }
            return new PetriNetwork(rand, fileName, name, certificateSubject, lastModificationDate, description, fireRule);
        }

        private static List<AbstractEventDrivenItem> openItemsFromXml(XmlNodeList root)
        {
            List<AbstractEventDrivenItem> ret = new List<AbstractEventDrivenItem>();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string localname = node.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_ITEM_NAMESPACE:
                        switch (localname)
                        {
                            case "Position":
                                ret.Add(Position.openFromXml(node));
                                break;
                            case "Transition":
                                ret.Add(Transition.openFromXml(node));
                                break;
                        }
                        break;
                }
            }
            return ret;
        }

        private static List<AbstractEdge> openEdgesFromXml(XmlNodeList root, List<AbstractEventDrivenItem> items)
        {
            List<AbstractEdge> ret = new List<AbstractEdge>();
            if (items != null)
            {
                foreach (XmlNode node in root)
                {
                    string namespaceUri = node.NamespaceURI;
                    switch (namespaceUri)
                    {
                        case PetriXmlHelper.XML_EDGE_NAMESPACE:
                            ret.Add(AbstractEdge.openFromXml(node, items));
                            break;
                    }
                }
            }
            return ret;
        }

        private static List<AbstractItem> openNotesFromXml(XmlNodeList root, List<AbstractItem> itemsAndEdges)
        {
            List<AbstractItem> ret = new List<AbstractItem>();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_ITEM_NAMESPACE:
                        ret.Add(Note.openFromXml(node, itemsAndEdges));
                        break;
                }
            }
            return ret;
        }

    }
}
