using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;

namespace PetriNetworkSimulator.Entities.Utils
{
    partial class NetworkVisualSettings
    {
        public const string XML_COLOR_NAMESPACE = "http://color.petrinetwork.hu";
        private const string XML_COLOR_NAMESPACE_PREFIX = "c";
        private const string XML_PEN_NAMESPACE = "http://pen.petrinetwork.hu";
        private const string XML_PEN_NAMESPACE_PREFIX = "p";
        private const string XML_FONT_NAMESPACE = "http://font.petrinetwork.hu";
        private const string XML_FONT_NAMESPACE_PREFIX = "f";
        private const string XML_FONTSTYLE_NAMESPACE = "http://fontstyle.petrinetwork.hu";
        private const string XML_FONTSTYLE_NAMESPACE_PREFIX = "fs";

        public static XmlElement saveColor(XmlDocument doc, Color color, string rootName)
        {
            XmlElement root = doc.CreateElement(NetworkVisualSettings.XML_COLOR_NAMESPACE_PREFIX, rootName, NetworkVisualSettings.XML_COLOR_NAMESPACE);
            XmlAttribute red = doc.CreateAttribute(NetworkVisualSettings.XML_COLOR_NAMESPACE_PREFIX, "red", NetworkVisualSettings.XML_COLOR_NAMESPACE);
            red.Value = color.R.ToString();
            XmlAttribute green = doc.CreateAttribute(NetworkVisualSettings.XML_COLOR_NAMESPACE_PREFIX, "green", NetworkVisualSettings.XML_COLOR_NAMESPACE);
            green.Value = color.G.ToString();
            XmlAttribute blue = doc.CreateAttribute(NetworkVisualSettings.XML_COLOR_NAMESPACE_PREFIX, "blue", NetworkVisualSettings.XML_COLOR_NAMESPACE);
            blue.Value = color.B.ToString();
            XmlAttribute alpha = doc.CreateAttribute(NetworkVisualSettings.XML_COLOR_NAMESPACE_PREFIX, "alpha", NetworkVisualSettings.XML_COLOR_NAMESPACE);
            alpha.Value = color.A.ToString();
            root.SetAttributeNode(red);
            root.SetAttributeNode(green);
            root.SetAttributeNode(blue);
            root.SetAttributeNode(alpha);
            return root;
        }

        private static XmlElement savePen(XmlDocument doc, Pen pen, string rootName)
        {
            XmlElement root = doc.CreateElement(NetworkVisualSettings.XML_PEN_NAMESPACE_PREFIX, rootName, NetworkVisualSettings.XML_PEN_NAMESPACE);
            // root.AppendChild(NetworkVisualSettings.saveColor(doc, pen.Color, "Color"));
            XmlAttribute width = doc.CreateAttribute(NetworkVisualSettings.XML_PEN_NAMESPACE_PREFIX, "width", NetworkVisualSettings.XML_PEN_NAMESPACE);
            width.Value = pen.Width.ToString();
            root.SetAttributeNode(width);
            return root;
        }

        private static XmlElement saveFontStyle(XmlDocument doc, FontStyle fontStyle, string rootName)
        {
            XmlElement root = doc.CreateElement(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, rootName, NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
            if (!((fontStyle & FontStyle.Bold) == 0))
            {
                XmlElement style = doc.CreateElement(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "Bold", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
                root.AppendChild(style);
            }
            if (!((fontStyle & FontStyle.Italic) == 0))
            {
                XmlElement style = doc.CreateElement(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "Italic", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
                root.AppendChild(style);
            }
            if (!((fontStyle & FontStyle.Underline) == 0))
            {
                XmlElement style = doc.CreateElement(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "Underline", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
                root.AppendChild(style);
            }
            if (!((fontStyle & FontStyle.Strikeout) == 0))
            {
                XmlElement style = doc.CreateElement(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "Strikeout", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
                root.AppendChild(style);
            }
            return root;
        }

        private static XmlElement saveFont(XmlDocument doc, Font font, string rootName)
        {
            XmlElement root = doc.CreateElement(NetworkVisualSettings.XML_FONT_NAMESPACE_PREFIX, rootName, NetworkVisualSettings.XML_FONT_NAMESPACE);
            // root.AppendChild(NetworkVisualSettings.saveFontStyle(doc,font.Style,"FontStyle"));
            XmlAttribute bold = doc.CreateAttribute(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "bold", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
            bold.Value = font.Bold.ToString();
            XmlAttribute italic = doc.CreateAttribute(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "italic", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
            italic.Value = font.Italic.ToString();
            XmlAttribute underline = doc.CreateAttribute(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "underline", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
            underline.Value = font.Underline.ToString();
            XmlAttribute strikeout = doc.CreateAttribute(NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE_PREFIX, "strikeout", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE);
            strikeout.Value = font.Strikeout.ToString();
            root.SetAttributeNode(bold);
            root.SetAttributeNode(italic);
            root.SetAttributeNode(underline);
            root.SetAttributeNode(strikeout);
            XmlElement fontName = doc.CreateElement(NetworkVisualSettings.XML_FONT_NAMESPACE_PREFIX, "FontName", NetworkVisualSettings.XML_FONT_NAMESPACE);
            fontName.InnerText = font.FontFamily.Name;
            root.AppendChild(fontName);
            XmlElement fontSize = doc.CreateElement(NetworkVisualSettings.XML_FONT_NAMESPACE_PREFIX, "FontSize", NetworkVisualSettings.XML_FONT_NAMESPACE);
            fontSize.InnerText = font.Size.ToString();
            root.AppendChild(fontSize);
            return root;
        }

        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "VisualSettings", PetriXmlHelper.XML_NAMESPACE);
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.defaultColor, "DefaultColor"));
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.markColor, "MarkColor"));
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.selectedColor, "SelectedColor"));
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.helpColor, "HelpColor"));
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.stateColor, "StateColor"));
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.markAsReadyToFireColor, "MarkAsReadyToFireColor"));
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.clockColor, "ClockColor"));
            root.AppendChild(NetworkVisualSettings.saveFont(doc, this.defaultFont, "DefaultFont"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.defaultEdgePen, "DefaultEdgePen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.defaultPen, "DefaultPen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.markPen, "MarkPen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.selectedEdgePen, "SelectedEdgePen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.selectedItemPen, "SelectedItemPen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.helpPen, "HelpPen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.statePen, "StatePen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.stateEdgePen, "StateEdgePen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.markAsReadyToFirePen, "MarkAsReadyToFirePen"));
            root.AppendChild(NetworkVisualSettings.savePen(doc, this.clockPen, "ClockPen"));
            return root;
        }

        private static void openPen(XmlNode node, Pen pen)
        {
            XmlAttribute attWidth = node.Attributes["width", NetworkVisualSettings.XML_PEN_NAMESPACE];
            float width = Convert.ToSingle(attWidth.Value);
            pen.Width = width;
        }

        public static Color openColor(XmlNode node)
        {
            XmlAttribute attRed = node.Attributes["red", NetworkVisualSettings.XML_COLOR_NAMESPACE];
            byte red = Convert.ToByte(attRed.Value);
            XmlAttribute attGreen = node.Attributes["green", NetworkVisualSettings.XML_COLOR_NAMESPACE];
            byte green = Convert.ToByte(attGreen.Value);
            XmlAttribute attBlue = node.Attributes["blue", NetworkVisualSettings.XML_COLOR_NAMESPACE];
            byte blue = Convert.ToByte(attBlue.Value);
            XmlAttribute attAlpha = node.Attributes["alpha", NetworkVisualSettings.XML_COLOR_NAMESPACE];
            byte alpha = Convert.ToByte(attAlpha.Value);
            return Color.FromArgb(alpha, red, green, blue);
        }

        private static Font openFont(XmlNode node)
        {
            XmlAttribute attBold = node.Attributes["bold", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE];
            bool bold = Convert.ToBoolean(attBold.Value);
            XmlAttribute attItalic = node.Attributes["italic", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE];
            bool italic = Convert.ToBoolean(attItalic.Value);
            XmlAttribute attUnderline = node.Attributes["underline", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE];
            bool underline = Convert.ToBoolean(attUnderline.Value);
            XmlAttribute attStrikeout = node.Attributes["strikeout", NetworkVisualSettings.XML_FONTSTYLE_NAMESPACE];
            bool strikeout = Convert.ToBoolean(attStrikeout.Value);
            FontStyle fontStlye = FontStyle.Regular;
            if (bold) { fontStlye |= FontStyle.Bold; }
            if (italic) { fontStlye |= FontStyle.Italic; }
            if (underline) { fontStlye |= FontStyle.Underline; }
            if (strikeout) { fontStlye |= FontStyle.Strikeout; }
            float emSize = 10;
            string familyName = "Arial";
            XmlNodeList fontChildren = node.ChildNodes;
            foreach (XmlNode nodeItem in fontChildren)
            {
                string namespaceUri = nodeItem.NamespaceURI;
                string name = nodeItem.LocalName;
                switch (namespaceUri)
                {
                    case NetworkVisualSettings.XML_FONT_NAMESPACE:
                        switch (name)
                        {
                            case "FontName":
                                familyName = nodeItem.InnerText;
                                break;
                            case "FontSize":
                                emSize = Convert.ToSingle(nodeItem.InnerText);
                                break;
                        }
                        break;
                }
            }
            return new Font(familyName, emSize, fontStlye);
        }

        public static NetworkVisualSettings openFromXml(XmlNodeList root)
        {
            NetworkVisualSettings ret = new NetworkVisualSettings();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string localName = node.LocalName;
                switch (namespaceUri)
                {
                    case NetworkVisualSettings.XML_COLOR_NAMESPACE:
                        Color color = NetworkVisualSettings.openColor(node);
                        switch (localName)
                        {
                            case "DefaultColor":
                                ret.DefaultColor = color;
                                break;
                            case "MarkColor":
                                ret.MarkColor = color;
                                break;
                            case "SelectedColor":
                                ret.SelectedColor = color;
                                break;
                            case "HelpColor":
                                ret.HelpColor = color;
                                break;
                            case "StateColor":
                                ret.StateColor = color;
                                break;
                            case "MarkAsReadyToFireColor":
                                ret.MarkAsReadyToFireColor = color;
                                break;
                            case "ClockColor":
                                ret.ClockColor = color;
                                break;
                        }
                        break;
                    case NetworkVisualSettings.XML_FONT_NAMESPACE:
                        Font font = NetworkVisualSettings.openFont(node);
                        switch (localName)
                        {
                            case "DefaultFont":
                                ret.defaultFont = font;
                                break;
                        }
                        break;
                    case NetworkVisualSettings.XML_PEN_NAMESPACE:
                        switch (localName)
                        {
                            case "DefaultEdgePen":
                                NetworkVisualSettings.openPen(node, ret.defaultEdgePen);
                                break;
                            case "DefaultPen":
                                NetworkVisualSettings.openPen(node, ret.defaultPen);
                                break;
                            case "MarkPen":
                                NetworkVisualSettings.openPen(node, ret.markPen);
                                break;
                            case "SelectedEdgePen":
                                NetworkVisualSettings.openPen(node, ret.selectedEdgePen);
                                break;
                            case "SelectedItemPen":
                                NetworkVisualSettings.openPen(node, ret.selectedItemPen);
                                break;
                            case "HelpPen":
                                NetworkVisualSettings.openPen(node, ret.helpPen);
                                break;
                            case "StatePen":
                                NetworkVisualSettings.openPen(node, ret.statePen);
                                break;
                            case "StateEdgePen":
                                NetworkVisualSettings.openPen(node, ret.stateEdgePen);
                                break;
                            case "MarkAsReadyToFirePen":
                                NetworkVisualSettings.openPen(node, ret.markAsReadyToFirePen);
                                break;
                            case "ClockPen":
                                NetworkVisualSettings.openPen(node, ret.clockPen);
                                break;
                        }
                        break;
                }
            }
            return ret;
        }

    }
}
