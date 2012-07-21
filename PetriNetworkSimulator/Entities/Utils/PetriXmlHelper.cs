using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Globalization;

namespace PetriNetworkSimulator.Entities.Utils
{
    public class PetriXmlHelper
    {
        public const string DATE_FORMAT = "yyyy.MM.dd HH:mm:ss";

        public const string XML_NAMESPACE = "http://petrinetwork.hu";
        public const string XML_NAMESPACE_PREFIX = "pn";

        public const string XML_POINTF_NAMESPACE = "http://pointf.petrinetwork.hu";
        private const string XML_POINTF_NAMESPACE_PREFIX = "pf";
        public const string XML_SIZEF_NAMESPACE = "http://sizef.petrinetwork.hu";
        private const string XML_SIZEF_NAMESPACE_PREFIX = "sf";

        public const string XML_NETWORKITEM_NAMESPACE = "http://networkitem.petrinetwork.hu";
        public const string XML_NETWORKITEM_NAMESPACE_PREFIX = "neit";
        public const string XML_BASEITEM_NAMESPACE = "http://baseitem.petrinetwork.hu";
        public const string XML_BASEITEM_NAMESPACE_PREFIX = "bi";
        public const string XML_EDGE_NAMESPACE = "http://edge.petrinetwork.hu";
        public const string XML_EDGE_NAMESPACE_PREFIX = "edg";
        public const string XML_ITEM_NAMESPACE = "http://item.petrinetwork.hu";
        public const string XML_ITEM_NAMESPACE_PREFIX = "i";
        public const string XML_NOTE_NAMESPACE = "http://note.petrinetwork.hu";
        public const string XML_NOTE_NAMESPACE_PREFIX = "not";
        public const string XML_POSITION_NAMESPACE = "http://position.petrinetwork.hu";
        public const string XML_POSITION_NAMESPACE_PREFIX = "pos";
        public const string XML_TRANSITION_NAMESPACE = "http://transition.petrinetwork.hu";
        public const string XML_TRANSITION_NAMESPACE_PREFIX = "tra";
        public const string XML_TOKEN_NAMESPACE = "http://token.petrinetwork.hu";
        public const string XML_TOKEN_NAMESPACE_PREFIX = "tok";
        public const string XML_EVENT_NAMESPACE = "http://event.petrinetwork.hu";
        public const string XML_EVENT_NAMESPACE_PREFIX = "pe";   
        public const string XML_SETTINGS_NAMESPACE = "http://settings.petrinetwork.hu";
        public const string XML_SETTIGNS_NAMESPACE_PREFIX = "ns";
        public const string XML_STATEEDGE_NAMESPACE = "http://stateedge.petrinetwork.hu";
        public const string XML_STATEEDGE_NAMESPACE_PREFIX = "se";
        public const string XML_STATEHIERARCHY_NAMESPACE = "http://statehierarchy.petrinetwork.hu";
        public const string XML_STATEHIERARCHY_NAMESPACE_PREFIX = "sh";
        public const string XML_STATE_NAMESPACE = "http://statevector.petrinetwork.hu";
        public const string XML_STATE_NAMESPACE_PREFIX = "sv";

        #region PointF methods

        public static XmlElement savePointF(XmlDocument doc, PointF point, string rootName)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_POINTF_NAMESPACE_PREFIX, rootName, PetriXmlHelper.XML_POINTF_NAMESPACE);
            XmlAttribute x = doc.CreateAttribute(PetriXmlHelper.XML_POINTF_NAMESPACE_PREFIX, "x", PetriXmlHelper.XML_POINTF_NAMESPACE);
            x.Value = point.X.ToString();
            root.SetAttributeNode(x);
            XmlAttribute y = doc.CreateAttribute(PetriXmlHelper.XML_POINTF_NAMESPACE_PREFIX, "y", PetriXmlHelper.XML_POINTF_NAMESPACE);
            y.Value = point.Y.ToString();
            root.SetAttributeNode(y);
            return root;
        }

        public static PointF openPointF(XmlNode node)
        {
            XmlAttribute attrX = node.Attributes["x", PetriXmlHelper.XML_POINTF_NAMESPACE];
            float x = Convert.ToSingle(attrX.Value);
            XmlAttribute attrY = node.Attributes["y", PetriXmlHelper.XML_POINTF_NAMESPACE];
            float y = Convert.ToSingle(attrY.Value);
            return new PointF(x, y);
        }

        #endregion

        #region SizeF methods

        public static XmlElement saveSizeF(XmlDocument doc, SizeF size, string rootName)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_SIZEF_NAMESPACE_PREFIX, rootName, PetriXmlHelper.XML_SIZEF_NAMESPACE);
            XmlAttribute width = doc.CreateAttribute(PetriXmlHelper.XML_SIZEF_NAMESPACE_PREFIX, "width", PetriXmlHelper.XML_SIZEF_NAMESPACE);
            width.Value = size.Width.ToString();
            root.SetAttributeNode(width);
            XmlAttribute height = doc.CreateAttribute(PetriXmlHelper.XML_SIZEF_NAMESPACE_PREFIX, "height", PetriXmlHelper.XML_SIZEF_NAMESPACE);
            height.Value = size.Height.ToString();
            root.SetAttributeNode(height);
            return root;
        }

        public static SizeF openSizeF(XmlNode node)
        {
            XmlAttribute attrWidth = node.Attributes["width", PetriXmlHelper.XML_SIZEF_NAMESPACE];
            float width = Convert.ToSingle(attrWidth.Value);
            XmlAttribute attrHeight = node.Attributes["height", PetriXmlHelper.XML_SIZEF_NAMESPACE];
            float height = Convert.ToSingle(attrHeight.Value);
            return new SizeF(width, height);
        }

        #endregion

        #region Open Attribute methods

        public static string openStringAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return attr.Value;
        }

        public static DateTime openDateTimeAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            DateTimeFormatInfo formatter = new DateTimeFormatInfo();
            formatter.FullDateTimePattern = PetriXmlHelper.DATE_FORMAT;
            return DateTime.ParseExact(attr.Value, PetriXmlHelper.DATE_FORMAT, formatter);
        }

        public static int openIntAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToInt32(attr.Value);
        }

        public static long openLongAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToInt64(attr.Value);
        }

        public static bool openBoolAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToBoolean(attr.Value);
        }

        public static float openFloatAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToSingle(attr.Value);
        }

        #endregion

        public static XmlElement saveData(XmlDocument doc, string value, string rootName)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_SETTIGNS_NAMESPACE_PREFIX, rootName, PetriXmlHelper.XML_SETTINGS_NAMESPACE);
            XmlAttribute attr = doc.CreateAttribute(PetriXmlHelper.XML_SETTIGNS_NAMESPACE_PREFIX, "value", PetriXmlHelper.XML_SETTINGS_NAMESPACE);
            attr.Value = value;
            root.SetAttributeNode(attr);
            return root;
        }

        public static XmlElement saveData(XmlDocument doc, int value, string rootName)
        {
            return PetriXmlHelper.saveData(doc, value.ToString(), rootName);
        }

    }
}
