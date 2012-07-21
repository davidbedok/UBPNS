using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Item.Note
{
    public partial class AbstractNote
    {
        
        protected override XmlElement saveToFile(XmlDocument doc, XmlElement root)
        {
            base.saveToFile(doc, root);
            XmlAttribute attachedItem = doc.CreateAttribute(PetriXmlHelper.XML_NOTE_NAMESPACE_PREFIX, "attachedItem", PetriXmlHelper.XML_NOTE_NAMESPACE);
            attachedItem.Value = this.attachedItem.Unid.ToString();
            root.SetAttributeNode(attachedItem);
            XmlElement text = doc.CreateElement(PetriXmlHelper.XML_NOTE_NAMESPACE_PREFIX, "Text", PetriXmlHelper.XML_NOTE_NAMESPACE);
            text.InnerText = this.text;
            root.AppendChild(text);
            return root;
        }

        protected static long openAttachedItemAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openLongAttributeFromNode(node, "attachedItem", PetriXmlHelper.XML_NOTE_NAMESPACE);
        }

    }
}
