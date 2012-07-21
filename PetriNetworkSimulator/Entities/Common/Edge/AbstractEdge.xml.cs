using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Edge;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Edge
{
    partial class AbstractEdge
    {

        public XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "Edge", PetriXmlHelper.XML_EDGE_NAMESPACE);

            base.saveToFile(doc, root);

            XmlAttribute weight = doc.CreateAttribute(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "weight", PetriXmlHelper.XML_EDGE_NAMESPACE);
            weight.Value = this.weight.ToString();
            root.SetAttributeNode(weight);

            XmlAttribute edgeType = doc.CreateAttribute(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "type", PetriXmlHelper.XML_EDGE_NAMESPACE);
            edgeType.Value = this.edgeType.Value;
            root.SetAttributeNode(edgeType);

            XmlElement start = doc.CreateElement(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "Start", PetriXmlHelper.XML_EDGE_NAMESPACE);
            start.InnerText = this.Start.Unid.ToString();
            XmlAttribute startType = doc.CreateAttribute(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "reftype", PetriXmlHelper.XML_EDGE_NAMESPACE);
            startType.Value = (this.Start is Position ? "POSITION" : "TRANSITION");
            start.SetAttributeNode(startType);
            root.AppendChild(start);

            XmlElement end = doc.CreateElement(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "End", PetriXmlHelper.XML_EDGE_NAMESPACE);
            end.InnerText = this.End.Unid.ToString();
            XmlAttribute endType = doc.CreateAttribute(PetriXmlHelper.XML_EDGE_NAMESPACE_PREFIX, "reftype", PetriXmlHelper.XML_EDGE_NAMESPACE);
            endType.Value = (this.End is Position ? "POSITION" : "TRANSITION");
            end.SetAttributeNode(endType);
            root.AppendChild(end);

            root.AppendChild(PetriXmlHelper.savePointF(doc, this.curveMiddlePointOffset, "CurveMiddlePointOffset"));
            return root;
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
            return EdgeType.getEnumByValue(attr.Value);
        }

        public static AbstractEdge openFromXml(XmlNode node, List<AbstractNetworkItem> items)
        {
            long positionUnid = 0;
            long transitionUnid = 0;
            bool isStartPosition = false;
            PointF curveMiddlePoint = new PointF(0, 0);
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
                    case PetriXmlHelper.XML_POINTF_NAMESPACE:
                        if ("CurveMiddlePointOffset".Equals(localName))
                        {
                            curveMiddlePoint = PetriXmlHelper.openPointF(childNode);
                        }
                        break;
                }
            }
            Position position = (Position)AbstractNetworkItem.findItemByUnid(items, positionUnid);
            Transition transition = (Transition)AbstractNetworkItem.findItemByUnid(items, transitionUnid); ;
            int weight = AbstractEdge.openWeightAttrFromNode(node);
            EdgeType edgeType = AbstractEdge.openEdgeTypeFromXml(node);
            string name = AbstractItem.openNameAttrFromNode(node);
            long unid = AbstractItem.openUnidAttrFromNode(node);
            bool showAnnotation = AbstractItem.openShowAnnotationAttrFromNode(node);
            AbstractEdge ret = null;
            if (isStartPosition)
            {
                ret = new EdgePositionTransition(name, unid, showAnnotation, weight, position, transition, curveMiddlePoint, edgeType);
            }
            else
            {
                ret = new EdgeTransitionPosition(name, unid, showAnnotation, weight, transition, position, curveMiddlePoint, edgeType);
            }
            return ret;
        }

    }
}
