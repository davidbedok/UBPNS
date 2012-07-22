using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Model.Base
{
    public partial class AbstractItem
    {

        protected XmlElement writeItemData(XmlDocument doc, XmlElement root)
        {
            XmlAttribute name = doc.CreateAttribute(PetriXmlHelper.XML_BASEITEM_NAMESPACE_PREFIX, AbstractItem.XML_NAME, PetriXmlHelper.XML_BASEITEM_NAMESPACE);
            name.Value = this.name;
            root.SetAttributeNode(name);
            XmlAttribute unid = doc.CreateAttribute(PetriXmlHelper.XML_BASEITEM_NAMESPACE_PREFIX, AbstractItem.XML_UNID, PetriXmlHelper.XML_BASEITEM_NAMESPACE);
            unid.Value = this.unid.ToString();
            root.SetAttributeNode(unid);
            XmlAttribute showAnnotation = doc.CreateAttribute(PetriXmlHelper.XML_BASEITEM_NAMESPACE_PREFIX, AbstractItem.XML_SHOWANNOTATION, PetriXmlHelper.XML_BASEITEM_NAMESPACE);
            showAnnotation.Value = this.showAnnotation.ToString();
            root.SetAttributeNode(showAnnotation);
            return root;
        }

    }
}
