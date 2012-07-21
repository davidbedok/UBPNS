using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;

namespace PetriNetworkSimulator.Entities.Utils
{
    partial class NetworkVisibleSettings
    {
        
        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "VisibleSettings", PetriXmlHelper.XML_NAMESPACE);
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleEdgeLabel.ToString(), "EdgeLabel"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleEdgeWeight.ToString(), "EdgeWeight"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleNotes.ToString(), "Notes"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visiblePositionLabel.ToString(), "PositionLabel"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visiblePriority.ToString(), "Priority"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleTransitionLabel.ToString(), "TransitionLabel"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleEdgeHelpLine.ToString(), "EdgeHelpLine"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleReadyToFireTransitions.ToString(), "ReadyToFireTransitions"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleCapacity.ToString(), "Capacity"));
            root.AppendChild(PetriXmlHelper.saveData(doc, this.visibleClock.ToString(), "Clock"));
            return root;
        }

        public static NetworkVisibleSettings openFromXml(XmlNodeList root)
        {
            NetworkVisibleSettings ret = new NetworkVisibleSettings();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string localName = node.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_SETTINGS_NAMESPACE:
                        switch (localName)
                        {
                            case "EdgeLabel":
                                ret.visibleEdgeLabel = PetriNetwork.openBoolData(node);
                                break;
                            case "EdgeWeight":
                                ret.visibleEdgeWeight = PetriNetwork.openBoolData(node);
                                break;
                            case "Notes":
                                ret.visibleNotes = PetriNetwork.openBoolData(node);
                                break;
                            case "PositionLabel":
                                ret.visiblePositionLabel = PetriNetwork.openBoolData(node);
                                break;
                            case "Priority":
                                ret.visiblePriority = PetriNetwork.openBoolData(node);
                                break;
                            case "TransitionLabel":
                                ret.visibleTransitionLabel = PetriNetwork.openBoolData(node);
                                break;
                            case "EdgeHelpLine":
                                ret.visibleEdgeHelpLine = PetriNetwork.openBoolData(node);
                                break;
                            case "ReadyToFireTransitions":
                                ret.visibleReadyToFireTransitions = PetriNetwork.openBoolData(node);
                                break;
                            case "Capacity":
                                ret.visibleCapacity = PetriNetwork.openBoolData(node);
                                break;
                            case "Clock":
                                ret.visibleClock = PetriNetwork.openBoolData(node);
                                break;
                        }
                        break;
                }
            }
            return ret;
        }


    }
}
