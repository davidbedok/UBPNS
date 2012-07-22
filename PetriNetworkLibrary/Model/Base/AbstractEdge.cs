using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Utility;
using System.Xml;
using PetriNetworkLibrary.Model.Edge;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkLibrary.Model.Base {


    public abstract class AbstractEdge : AbstractItem
    {

        protected readonly int weight;
        protected readonly Position position;
        protected readonly Transition transition;
        protected readonly EdgeType edgeType;

        public int Weight
        {
            get { return this.weight; }
        }

        public abstract AbstractEventDrivenItem Start
        {
            get;
        }

        public abstract AbstractEventDrivenItem End
        {
            get;
        }

        public EdgeType EdgeType
        {
            get { return this.edgeType; }
        }

        public AbstractEdge(AbstractItemData itemData, int weight, Position position, Transition transition, EdgeType edgeType)
            : this(itemData.name, itemData.unid, itemData.showAnnotation, weight, position, transition, edgeType)
        {
        }

        public AbstractEdge(string name, long unid, bool showAnnotation, int weight, Position position, Transition transition, EdgeType edgeType)
            : base(name, unid, showAnnotation)
        {
            this.weight = weight;
            this.position = position;
            this.transition = transition;
            this.edgeType = edgeType;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine(base.ToString());
            sb.AppendLine("  edgeType: " + edgeType.ToString());
            return sb.ToString();
        }

        protected static int openWeightAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "weight", PetriXmlHelper.XML_EDGE_NAMESPACE);
        }

        private static string openRefTypeAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, "reftype", PetriXmlHelper.XML_EDGE_NAMESPACE);
        }

        protected static EdgeType openEdgeTypeFromXml(XmlNode node)
        {
            XmlAttribute attr = node.Attributes["type", PetriXmlHelper.XML_EDGE_NAMESPACE];
            return (EdgeType)Enum.Parse(typeof(EdgeType), attr.Value);
        }

        internal static AbstractEdge openFromXml(XmlNode node, List<AbstractEventDrivenItem> items)
        {
            long positionUnid = 0;
            long transitionUnid = 0;
            bool isStartPosition = false;
            XmlNodeList list = node.ChildNodes;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_EDGE_NAMESPACE:
                        string reftype = AbstractEdge.openRefTypeAttrFromNode(childNode);
                        switch (localName)
                        {
                            case "Start":
                                if ("TRANSITION".Equals(reftype))
                                {
                                    transitionUnid = Convert.ToInt64(childNode.InnerText);
                                }
                                else if ("POSITION".Equals(reftype))
                                {
                                    positionUnid = Convert.ToInt64(childNode.InnerText);
                                    isStartPosition = true;
                                }
                                break;
                            case "End":
                                if ("TRANSITION".Equals(reftype))
                                {
                                    isStartPosition = true;
                                    transitionUnid = Convert.ToInt64(childNode.InnerText);
                                }
                                else if ("POSITION".Equals(reftype))
                                {
                                    positionUnid = Convert.ToInt64(childNode.InnerText);
                                }
                                break;
                        }
                        break;
                }
            }
            Position position = (Position)AbstractEventDrivenItem.findNetworkItemByUnid(items, positionUnid);
            Transition transition = (Transition)AbstractEventDrivenItem.findNetworkItemByUnid(items, transitionUnid); ;
            int weight = AbstractEdge.openWeightAttrFromNode(node);
            EdgeType edgeType = AbstractEdge.openEdgeTypeFromXml(node);
            AbstractItemData itemData = AbstractItem.readItem(node);
            AbstractEdge ret = null;
            if (isStartPosition)
            {
                ret = new EdgePositionTransition(itemData, weight, position, transition, edgeType);
            }
            else
            {
                ret = new EdgeTransitionPosition(itemData, weight, transition, position, edgeType);
            }
            return ret;
        }


    }
}
