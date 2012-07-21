using System.Collections.Generic;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Item.Note;
using PetriNetworkSimulator.Entities.Item.NetNote;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Network
{
    partial class AbstractNetwork
    {

        protected XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "Network", PetriXmlHelper.XML_NAMESPACE);
            XmlElement items = doc.CreateElement(PetriXmlHelper.XML_NETWORKITEM_NAMESPACE_PREFIX, "NetworkItems", PetriXmlHelper.XML_NETWORKITEM_NAMESPACE);
            foreach (AbstractNetworkItem item in this.items)
            {
                if (!(item is AbstractNote))
                {
                    items.AppendChild(item.saveToFile(doc));
                }
            }
            root.AppendChild(items);
            XmlElement edges = doc.CreateElement(PetriXmlHelper.XML_NETWORKITEM_NAMESPACE_PREFIX, "Edges", PetriXmlHelper.XML_NETWORKITEM_NAMESPACE);
            foreach (AbstractEdge edge in this.edges)
            {
                edges.AppendChild(edge.saveToFile(doc));
            }
            root.AppendChild(edges);
            XmlElement notes = doc.CreateElement(PetriXmlHelper.XML_NETWORKITEM_NAMESPACE_PREFIX, "Notes", PetriXmlHelper.XML_NETWORKITEM_NAMESPACE);
            foreach (AbstractNetworkItem item in this.items)
            {
                if (item is AbstractNote)
                {
                    notes.AppendChild(item.saveToFile(doc));
                }
            }
            root.AppendChild(notes);
            return root;
        }

        public static List<AbstractNetworkItem> openItemsFromXml(XmlNodeList root)
        {
            List<AbstractNetworkItem> ret = new List<AbstractNetworkItem>();
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

        public static List<AbstractEdge> openEdgesFromXml(XmlNodeList root, List<AbstractNetworkItem> items)
        {
            List<AbstractEdge> ret = new List<AbstractEdge>();
            if (items != null)
            {
                foreach (XmlNode node in root)
                {
                    string namespaceUri = node.NamespaceURI;
                    // string localname = node.LocalName;
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

        public static List<AbstractNetworkItem> openNotesFromXml(XmlNodeList root, List<AbstractItem> itemsAndEdges)
        {
            List<AbstractNetworkItem> ret = new List<AbstractNetworkItem>();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                // string localname = node.LocalName;
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
