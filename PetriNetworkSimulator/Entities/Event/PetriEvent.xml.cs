using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Event
{
    public partial class PetriEvent
    {

        public XmlElement saveEvent(XmlDocument doc, string rootname)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_EVENT_NAMESPACE_PREFIX, rootname, PetriXmlHelper.XML_EVENT_NAMESPACE);
            XmlAttribute name = doc.CreateAttribute(PetriXmlHelper.XML_EVENT_NAMESPACE_PREFIX, "name", PetriXmlHelper.XML_EVENT_NAMESPACE);
            name.Value = this.Name;
            root.SetAttributeNode(name);
            XmlAttribute type = doc.CreateAttribute(PetriXmlHelper.XML_EVENT_NAMESPACE_PREFIX, "type", PetriXmlHelper.XML_EVENT_NAMESPACE);
            type.Value = this.Type.Value;
            root.SetAttributeNode(type);
            return root;
        }

        public static PetriEvent openEventData(XmlNode node)
        {
            XmlAttribute attrName = node.Attributes["name", PetriXmlHelper.XML_EVENT_NAMESPACE];
            string name = attrName.Value;
            XmlAttribute attrType = node.Attributes["type", PetriXmlHelper.XML_EVENT_NAMESPACE];
            EventType type = EventType.getEnumByValue(attrType.Value);
            return new PetriEvent(type, name);
        }

        public static List<PetriEvent> openEvents(XmlNodeList root, string eventLocalName)
        {
            List<PetriEvent> ret = new List<PetriEvent>();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string localName = node.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_EVENT_NAMESPACE:
                        if (eventLocalName.Equals(localName))
                        {
                            ret.Add(PetriEvent.openEventData(node));
                        }
                        break;
                }
            }
            return ret;
        }


    }
}
