using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace PetriNetworkLibrary.Utility
{
    public class PetriXmlHelper : System.Object
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

        public const string XML_COLOR_NAMESPACE = "http://color.petrinetwork.hu";
        private const string XML_COLOR_NAMESPACE_PREFIX = "c";

        #region Open Attribute methods

        internal static string openStringAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return attr.Value;
        }

        internal static DateTime openDateTimeAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            DateTimeFormatInfo formatter = new DateTimeFormatInfo();
            formatter.FullDateTimePattern = PetriXmlHelper.DATE_FORMAT;
            return DateTime.ParseExact(attr.Value, PetriXmlHelper.DATE_FORMAT, formatter);
        }

        internal static int openIntAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToInt32(attr.Value);
        }

        internal static long openLongAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToInt64(attr.Value);
        }

        internal static bool openBoolAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToBoolean(attr.Value);
        }

        internal static float openFloatAttributeFromNode(XmlNode node, string attributeName, string xmlNamespace)
        {
            XmlAttribute attr = node.Attributes[attributeName, xmlNamespace];
            return Convert.ToSingle(attr.Value);
        }

        #endregion

    }
}
