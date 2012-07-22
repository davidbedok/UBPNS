using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkLibrary.Model.Base;
using System.Xml;
using PetriNetworkLibrary.Utility;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkLibrary.Model.TokenPlayer
{
    public class Token : AbstractItem
    {

        private readonly Color tokenColor;

        public Color TokenColor
        {
            get { return this.tokenColor; }
        }

        public Token(AbstractItemData itemData, Color tokenColor)
            : this(itemData.name, itemData.unid, itemData.showAnnotation, tokenColor)
        {
        }

        public Token(string name, long unid, bool showAnnotation, Color tokenColor)
            : base(name, unid, showAnnotation)
        {
            this.tokenColor = tokenColor;
        }

        public override string ToString()
        {
            return "    [TOKEN] " + base.ToString() + " tokenColor:" + tokenColor.ToString();
        }

        public static Token findTokenByUnid(List<Token> items, long unid)
        {
            Token ret = null;
            int i = 0;
            bool find = false;
            while ((i < items.Count) && (!find))
            {
                if (unid.Equals(items[i].Unid))
                {
                    ret = items[i];
                    find = true;
                }
                i++;
            }
            return ret;
        }

        internal static Token openFromXml(XmlNode node)
        {
            Color color = Color.Black;
            XmlNodeList list = node.ChildNodes;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_COLOR_NAMESPACE:
                        if ("TokenColor".Equals(localName))
                        {
                            color = Token.openColor(childNode);
                        }
                        break;
                }
            }
            return new Token(AbstractItem.readItem(node), color);
        }

        private static Color openColor(XmlNode node)
        {
            XmlAttribute attRed = node.Attributes["red", PetriXmlHelper.XML_COLOR_NAMESPACE];
            byte red = Convert.ToByte(attRed.Value);
            XmlAttribute attGreen = node.Attributes["green", PetriXmlHelper.XML_COLOR_NAMESPACE];
            byte green = Convert.ToByte(attGreen.Value);
            XmlAttribute attBlue = node.Attributes["blue", PetriXmlHelper.XML_COLOR_NAMESPACE];
            byte blue = Convert.ToByte(attBlue.Value);
            XmlAttribute attAlpha = node.Attributes["alpha", PetriXmlHelper.XML_COLOR_NAMESPACE];
            byte alpha = Convert.ToByte(attAlpha.Value);
            return Color.FromArgb(alpha, red, green, blue);
        }

    }
}
