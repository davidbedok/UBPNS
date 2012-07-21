using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.TokenPlayer;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Event;

namespace PetriNetworkSimulator.Entities.State.Vector
{
    partial class StateVector
    {

        public string TypeStr
        {
            get { return "StateVector"; }
        }

        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "StateVector", PetriXmlHelper.XML_STATE_NAMESPACE);
            XmlAttribute name = doc.CreateAttribute(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "name", PetriXmlHelper.XML_STATE_NAMESPACE);
            name.Value = this.name;
            root.SetAttributeNode(name);
            XmlAttribute unid = doc.CreateAttribute(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
            unid.Value = this.unid.ToString();
            root.SetAttributeNode(unid);

            root.AppendChild(PetriXmlHelper.savePointF(doc, this.origo, "StateOrigo"));
            XmlAttribute radius = doc.CreateAttribute(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "radius", PetriXmlHelper.XML_STATE_NAMESPACE);
            radius.Value = this.radius.ToString();
            root.SetAttributeNode(radius);

            XmlElement tokenDistributions = doc.CreateElement(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "TokenDistributions", PetriXmlHelper.XML_STATE_NAMESPACE);
            foreach (KeyValuePair<Int64, List<AbstractToken>> entry in this.tokenDistribution)
            {
                Int64 positionUnid = entry.Key;
                List<AbstractToken> tokens = entry.Value;

                XmlElement position = doc.CreateElement(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "Position", PetriXmlHelper.XML_STATE_NAMESPACE);
                XmlAttribute posUnid = doc.CreateAttribute(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
                posUnid.Value = positionUnid.ToString();
                position.SetAttributeNode(posUnid);
                foreach (AbstractToken token in tokens)
                {
                    XmlElement tokenElement = doc.CreateElement(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "Token", PetriXmlHelper.XML_STATE_NAMESPACE);
                    XmlAttribute tokUnid = doc.CreateAttribute(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
                    tokUnid.Value = token.Unid.ToString();
                    tokenElement.SetAttributeNode(tokUnid);
                    position.AppendChild(tokenElement);
                }
                tokenDistributions.AppendChild(position);
            }
            root.AppendChild(tokenDistributions);
            XmlElement events = doc.CreateElement(PetriXmlHelper.XML_STATE_NAMESPACE_PREFIX, "Events", PetriXmlHelper.XML_STATE_NAMESPACE);
            foreach (PetriEvent pe in this.PetriEvents.Events)
            {
                if (pe.Adjusted)
                {
                    events.AppendChild(pe.saveEvent(doc, "StateEvent"));
                }
            }
            root.AppendChild(events);
            return root;
        }

        public static List<AbstractToken> openTokensFromXml(XmlNodeList list, List<AbstractItem> alltokens)
        {
            List<AbstractToken> ret = new List<AbstractToken>();
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                if ((PetriXmlHelper.XML_STATE_NAMESPACE.Equals(namespaceUri)) && ("Token".Equals(localName)))
                {
                    long tokUnid = StateVector.openUnidAttrFromNode(childNode);
                    AbstractToken token = (Token)AbstractItem.findItemByUnid(alltokens, tokUnid);
                    if (token == null)
                    {
                        token = new Token("", tokUnid, true);
                    }
                    ret.Add(token);
                }
            }
            return ret;
        }

        public static Dictionary<Int64, List<AbstractToken>> openTokenDistributionFromXml(XmlNodeList list, List<AbstractItem> alltokens)
        {
            Dictionary<Int64, List<AbstractToken>> ret = new Dictionary<Int64, List<AbstractToken>>();
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                if ((PetriXmlHelper.XML_STATE_NAMESPACE.Equals(namespaceUri)) && ("Position".Equals(localName)))
                {
                    long posUnid = StateVector.openUnidAttrFromNode(childNode);
                    ret.Add(posUnid, StateVector.openTokensFromXml(childNode.ChildNodes, alltokens));
                }
            }
            return ret;
        }

        public static StateVector openFromXml(XmlNode node, List<AbstractItem> alltokens)
        {
            PointF origo = new PointF(0, 0);
            XmlNodeList list = node.ChildNodes;
            Dictionary<Int64, List<AbstractToken>> tokenDistribution = null;
            List<PetriEvent> events = null;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_POINTF_NAMESPACE:
                        if ("StateOrigo".Equals(localName))
                        {
                            origo = PetriXmlHelper.openPointF(childNode);
                        }
                        break;
                    case PetriXmlHelper.XML_STATE_NAMESPACE:
                        switch (localName)
                        {
                            case "TokenDistributions":
                                tokenDistribution = StateVector.openTokenDistributionFromXml(childNode.ChildNodes, alltokens);
                                break;
                            case "Events":
                                events = PetriEvent.openEvents(childNode.ChildNodes, "StateEvent");
                                break;
                        }
                        break;
                }
            }
            string name = StateVector.openNameAttrFromNode(node);
            long unid = StateVector.openUnidAttrFromNode(node);
            float radius = StateVector.openRadiusAttrFromNode(node);
            StateVector ret = new StateVector(name, unid, tokenDistribution, origo, radius);
            ret.PetriEvents.addEvent(events);
            return ret;
        }

        protected static string openNameAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, "name", PetriXmlHelper.XML_STATE_NAMESPACE);
        }

        protected static long openUnidAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openLongAttributeFromNode(node, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
        }

        protected static float openRadiusAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openFloatAttributeFromNode(node, "radius", PetriXmlHelper.XML_STATE_NAMESPACE);
        }


    }
}
