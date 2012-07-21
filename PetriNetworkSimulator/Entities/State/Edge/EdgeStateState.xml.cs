using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.State.Edge
{
    partial class EdgeStateState
    {

        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_STATEEDGE_NAMESPACE_PREFIX, "StateEdge", PetriXmlHelper.XML_STATEEDGE_NAMESPACE);

            XmlElement start = doc.CreateElement(PetriXmlHelper.XML_STATEEDGE_NAMESPACE_PREFIX, "StartState", PetriXmlHelper.XML_STATEEDGE_NAMESPACE);
            start.InnerText = this.Start.Unid.ToString();
            root.AppendChild(start);
            XmlElement end = doc.CreateElement(PetriXmlHelper.XML_STATEEDGE_NAMESPACE_PREFIX, "EndState", PetriXmlHelper.XML_STATEEDGE_NAMESPACE);
            end.InnerText = this.End.Unid.ToString();
            root.AppendChild(end);

            return root;
        }

        public static EdgeStateState openFromXml(XmlNode node, List<StateVector> states)
        {
            XmlNodeList list = node.ChildNodes;
            long startStateUnid = 0;
            long endStateUnid = 0;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_STATEEDGE_NAMESPACE:
                        if ("StartState".Equals(localName))
                        {
                            startStateUnid = Convert.ToInt64(childNode.InnerText);
                        }
                        else if ("EndState".Equals(localName))
                        {
                            endStateUnid = Convert.ToInt64(childNode.InnerText);
                        }
                        break;
                }
            }

            StateVector startState = StateVector.findItemByUnid(states, startStateUnid);
            StateVector endState = StateVector.findItemByUnid(states, endStateUnid); ;
            return new EdgeStateState(startState, endState);
        }

    }
}
