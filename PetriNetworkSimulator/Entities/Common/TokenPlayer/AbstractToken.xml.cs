using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.TokenPlayer;

namespace PetriNetworkSimulator.Entities.Common.TokenPlayer
{
    partial class AbstractToken
    {

        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_TOKEN_NAMESPACE_PREFIX, "Token", PetriXmlHelper.XML_TOKEN_NAMESPACE);
            base.saveToFile(doc, root);
            root.AppendChild(NetworkVisualSettings.saveColor(doc, this.tokenColor, "TokenColor"));
            return root;
        }

        public static AbstractToken openFromXml(XmlNode node)
        {
            Token ret = new Token(AbstractItem.openNameAttrFromNode(node), AbstractItem.openUnidAttrFromNode(node), true);
            XmlNodeList list = node.ChildNodes;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case NetworkVisualSettings.XML_COLOR_NAMESPACE:
                        if ("TokenColor".Equals(localName))
                        {
                            ret.TokenColor = NetworkVisualSettings.openColor(childNode);
                        }
                        break;
                }
            }
            return ret;
        }

    }
}
