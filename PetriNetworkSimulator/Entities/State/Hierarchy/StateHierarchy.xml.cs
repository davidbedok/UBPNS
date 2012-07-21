using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.State.Edge;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.State.Hierarchy
{
    partial class StateHierarchy
    {

        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_NAMESPACE_PREFIX, "StateHierarchy", PetriXmlHelper.XML_NAMESPACE);
            XmlElement items = doc.CreateElement(PetriXmlHelper.XML_STATEHIERARCHY_NAMESPACE_PREFIX, "States", PetriXmlHelper.XML_STATEHIERARCHY_NAMESPACE);
            foreach (StateVector item in this.states)
            {
                items.AppendChild(item.saveToFile(doc));   
            }
            root.AppendChild(items);
            XmlElement edges = doc.CreateElement(PetriXmlHelper.XML_STATEHIERARCHY_NAMESPACE_PREFIX, "Edges", PetriXmlHelper.XML_STATEHIERARCHY_NAMESPACE);
            foreach (EdgeStateState edge in this.edges)
            {
                edges.AppendChild(edge.saveToFile(doc));
            }
            root.AppendChild(edges);
            return root;
        }

        public static List<StateVector> openStatesFromXml(XmlNodeList root, List<AbstractItem> alltokens)
        {
            List<StateVector> ret = new List<StateVector>();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string name = node.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_STATE_NAMESPACE:
                        ret.Add(StateVector.openFromXml(node, alltokens));
                        break;
                }
            }
            return ret;
        }

        public static List<EdgeStateState> openEdgesFromXml(XmlNodeList root, List<StateVector> states)
        {
            List<EdgeStateState> ret = new List<EdgeStateState>();
            foreach (XmlNode node in root)
            {
                string namespaceUri = node.NamespaceURI;
                string name = node.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_STATEEDGE_NAMESPACE:
                        ret.Add(EdgeStateState.openFromXml(node, states));
                        break;
                }
            }
            return ret;
        }

    }
}
