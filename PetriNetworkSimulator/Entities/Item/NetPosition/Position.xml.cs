using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Item.Position;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Item.NetPosition
{
    partial class Position
    {

        public override XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_ITEM_NAMESPACE_PREFIX, "Position", PetriXmlHelper.XML_ITEM_NAMESPACE);
            this.saveToFile(doc, root);
            XmlAttribute capacityLimit = doc.CreateAttribute(PetriXmlHelper.XML_POSITION_NAMESPACE_PREFIX, "capacitylimit", PetriXmlHelper.XML_POSITION_NAMESPACE);
            capacityLimit.Value = this.capacityLimit.ToString();
            root.SetAttributeNode(capacityLimit);
            XmlElement tokens = doc.CreateElement(PetriXmlHelper.XML_POSITION_NAMESPACE_PREFIX, "Tokens", PetriXmlHelper.XML_POSITION_NAMESPACE);
            foreach (AbstractToken token in this.tokens)
            {
                tokens.AppendChild(token.saveToFile(doc));
            }
            root.AppendChild(tokens);
            XmlElement events = doc.CreateElement(PetriXmlHelper.XML_ITEM_NAMESPACE_PREFIX, "Events", PetriXmlHelper.XML_ITEM_NAMESPACE);
            foreach (PetriEvent pe in this.PetriEvents.Events)
            {
                if (pe.Adjusted)
                {
                    events.AppendChild(pe.saveEvent(doc, "ItemEvent"));
                }
            }
            root.AppendChild(events);
            return root;
        }

        public static List<AbstractToken> openTokensFromXml(XmlNodeList list)
        {
            List<AbstractToken> ret = new List<AbstractToken>();
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                if ((PetriXmlHelper.XML_TOKEN_NAMESPACE.Equals(namespaceUri)) && ("Token".Equals(localName)))
                {
                    ret.Add(AbstractToken.openFromXml(childNode));
                }
            }
            return ret;
        }

        public static AbstractNetworkItem openFromXml(XmlNode node)
        {
            PointF origo = new PointF(0, 0);
            PointF labelOffset = new PointF(0, 0);
            List<AbstractToken> tokens = null;
            List<PetriEvent> events = null;
            XmlNodeList list = node.ChildNodes;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_POINTF_NAMESPACE:
                        if ("Origo".Equals(localName))
                        {
                            origo = PetriXmlHelper.openPointF(childNode);
                        }
                        else if ("LabelOffset".Equals(localName))
                        {
                            labelOffset = PetriXmlHelper.openPointF(childNode);
                        }
                        break;
                    case PetriXmlHelper.XML_POSITION_NAMESPACE:
                        switch (localName)
                        {
                            case "Tokens":
                                tokens = Position.openTokensFromXml(childNode.ChildNodes);
                                break;
                        }
                        break;
                    case PetriXmlHelper.XML_ITEM_NAMESPACE:
                        switch (localName)
                        {
                            case "Events":
                                events = PetriEvent.openEvents(childNode.ChildNodes, "ItemEvent");
                                break;
                        }
                        break;
                }
            }
            string name = AbstractItem.openNameAttrFromNode(node);
            long unid = AbstractItem.openUnidAttrFromNode(node);
            bool showAnnotation = AbstractItem.openShowAnnotationAttrFromNode(node);
            float radius = AbstractNetworkItem.openRadiusAttrFromNode(node);
            int capacityLimit = AbstractPosition.openCapacityLimitAttrFromNode(node);
            Position ret = new Position(name, unid, showAnnotation, origo, radius, capacityLimit);
            ret.PetriEvents.addEvent(events);
            ret.LabelOffset = labelOffset;
            ret.tokens.AddRange(tokens);
            return ret;
        }

    }
}
