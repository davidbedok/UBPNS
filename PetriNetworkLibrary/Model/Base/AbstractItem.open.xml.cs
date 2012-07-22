using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkLibrary.Utility;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkLibrary.Model.Base
{
    public partial class AbstractItem
    {

        protected static AbstractItemData readItem(XmlNode node)
        {
            string name = AbstractItem.readItemName(node);
            long unid = AbstractItem.readItemUnid(node);
            bool showAnnotation = AbstractItem.readShowAnnotation(node);
            return new AbstractItemData(name, unid, showAnnotation);
        }

        private static string readItemName(XmlNode node)
        {
            return readItemName(node, PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static string readItemName(XmlNode node, string nameSpace)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, AbstractItem.XML_NAME, nameSpace);
        }

        private static long readItemUnid(XmlNode node)
        {
            return readItemUnid(node, PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static long readItemUnid(XmlNode node, string nameSpace)
        {
            return PetriXmlHelper.openLongAttributeFromNode(node, AbstractItem.XML_UNID, nameSpace);
        }

        private static bool readShowAnnotation(XmlNode node)
        {
            return readShowAnnotation(node, PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static bool readShowAnnotation(XmlNode node, string nameSpace)
        {
            return PetriXmlHelper.openBoolAttributeFromNode(node, AbstractItem.XML_SHOWANNOTATION, nameSpace);
        }

    }
}
