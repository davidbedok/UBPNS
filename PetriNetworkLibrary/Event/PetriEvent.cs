using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Utility;
using System.Xml;

namespace PetriNetworkLibrary.Event
{
    public class PetriEvent : System.Object
    {

        private readonly EventType type;
        private readonly string name;

        public EventType Type
        {
            get { return this.type; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public PetriEvent(EventType type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public override string ToString()
        {
            return this.name + " (" + this.type + ")";
        }

        internal static List<PetriEvent> openEvents(XmlNodeList root, string eventLocalName)
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

        private static PetriEvent openEventData(XmlNode node)
        {
            XmlAttribute attrName = node.Attributes["name", PetriXmlHelper.XML_EVENT_NAMESPACE];
            string name = attrName.Value;
            XmlAttribute attrType = node.Attributes["type", PetriXmlHelper.XML_EVENT_NAMESPACE];
            EventType type = (EventType)Enum.Parse(typeof(EventType), attrType.Value);
            return new PetriEvent(type, name);
        }

    }
}
