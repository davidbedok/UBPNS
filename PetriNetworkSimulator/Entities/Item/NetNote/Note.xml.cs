using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;
using PetriNetworkSimulator.Entities.Common.Item.Note;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Item.NetNote
{
    public partial class Note
    {

        public override XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_ITEM_NAMESPACE_PREFIX, "Note", PetriXmlHelper.XML_ITEM_NAMESPACE);
            this.saveToFile(doc, root);
            return root;
        }

        public static AbstractNetworkItem openFromXml(XmlNode node, List<AbstractItem> itemsAndEdges)
        {
            PointF origo = new PointF(0, 0);
            SizeF size = new SizeF(0, 0);
            string text = "";
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
                        break;
                    case PetriXmlHelper.XML_SIZEF_NAMESPACE:
                        if ("Size".Equals(localName))
                        {
                            size = PetriXmlHelper.openSizeF(childNode);
                        }
                        break;
                    case PetriXmlHelper.XML_NOTE_NAMESPACE:
                        if ("Text".Equals(localName))
                        {
                            text = childNode.InnerText;
                        }
                        break;
                }
            }
            string name = AbstractItem.openNameAttrFromNode(node);
            long unid = AbstractItem.openUnidAttrFromNode(node);
            long attachedItemUnid = AbstractNote.openAttachedItemAttrFromNode(node);
            AbstractItem attachedItem = AbstractItem.findItemByUnid(itemsAndEdges, attachedItemUnid);
            return new Note(name, unid, true, origo, size, attachedItem, text);
        }

    }
}
