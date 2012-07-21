using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Base
{
    partial class AbstractItem
    {

        protected virtual XmlElement saveToFile(XmlDocument doc, XmlElement root)
        {
            XmlAttribute name = doc.CreateAttribute(PetriXmlHelper.XML_BASEITEM_NAMESPACE_PREFIX, "name", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
            name.Value = this.name;
            root.SetAttributeNode(name);
            XmlAttribute unid = doc.CreateAttribute(PetriXmlHelper.XML_BASEITEM_NAMESPACE_PREFIX, "unid", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
            unid.Value = this.unid.ToString();
            root.SetAttributeNode(unid);
            XmlAttribute showAnnotation = doc.CreateAttribute(PetriXmlHelper.XML_BASEITEM_NAMESPACE_PREFIX, "showannotation", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
            showAnnotation.Value = this.showAnnotation.ToString();
            root.SetAttributeNode(showAnnotation);
            return root;
        }

        protected static string openNameAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, "name", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static long openUnidAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openLongAttributeFromNode(node, "unid", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static bool openShowAnnotationAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openBoolAttributeFromNode(node, "showannotation", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

    }
}
