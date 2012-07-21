using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.Item.Note;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Item.Base
{
    partial class AbstractNetworkItem
    {

        protected override XmlElement saveToFile(XmlDocument doc, XmlElement root)
        {
            base.saveToFile(doc, root);
            root.AppendChild(PetriXmlHelper.savePointF(doc, this.point, "TopLeftPoint"));
            root.AppendChild(PetriXmlHelper.savePointF(doc, this.origo, "Origo"));
            root.AppendChild(PetriXmlHelper.savePointF(doc, this.labelOffset, "LabelOffset"));
            root.AppendChild(PetriXmlHelper.saveSizeF(doc, this.size, "Size"));
            XmlAttribute radius = doc.CreateAttribute(PetriXmlHelper.XML_ITEM_NAMESPACE_PREFIX, "radius", PetriXmlHelper.XML_ITEM_NAMESPACE);
            radius.Value = this.radius.ToString();
            root.SetAttributeNode(radius);
            return root;
        }

        public abstract XmlElement saveToFile(XmlDocument doc);

        protected static float openRadiusAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openFloatAttributeFromNode(node, "radius", PetriXmlHelper.XML_ITEM_NAMESPACE);
        }

    }
}
