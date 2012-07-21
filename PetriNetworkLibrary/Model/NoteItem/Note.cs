using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using System.Xml;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Model.NoteItem
{
    public class Note : AbstractItem
    {

        private readonly AbstractItem attachedItem;
        private readonly string text;

        public AbstractItem AttachedItem
        {
            get { return this.attachedItem; }
        }

        public string Text
        {
            get { return this.text; }
        }

        public Note(string name, long unid, AbstractItem attachedItem, string text)
            : base(name, unid)
        {
           this.attachedItem = attachedItem;
           this.text = text;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("abstractItem: " + base.ToString());
            sb.AppendLine("attachedItem: " + attachedItem.ToString());
            sb.AppendLine("text: " + text);
            return sb.ToString();
        }

        internal static AbstractItem openFromXml(XmlNode node, List<AbstractItem> itemsAndEdges)
        {
            string text = "";
            XmlNodeList list = node.ChildNodes;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
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
            long attachedItemUnid = Note.openAttachedItemAttrFromNode(node);
            AbstractItem attachedItem = AbstractItem.findItemByUnid(itemsAndEdges, attachedItemUnid);
            return new Note(name, unid, attachedItem, text);
        }

        private static long openAttachedItemAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openLongAttributeFromNode(node, "attachedItem", PetriXmlHelper.XML_NOTE_NAMESPACE);
        }

    }
}
