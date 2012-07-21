using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Item.NetTransition
{
    partial class Transition
    {

        public override XmlElement saveToFile(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement(PetriXmlHelper.XML_ITEM_NAMESPACE_PREFIX, "Transition", PetriXmlHelper.XML_ITEM_NAMESPACE);
            this.saveToFile(doc, root);
            XmlElement events = doc.CreateElement(PetriXmlHelper.XML_ITEM_NAMESPACE_PREFIX, "Events", PetriXmlHelper.XML_ITEM_NAMESPACE);
            foreach (PetriEvent pe in this.PetriEvents.Events)
            {
                if (pe.Adjusted)
                {
                    events.AppendChild(pe.saveEvent(doc, "ItemEvent"));
                }
            }
            root.AppendChild(events);
            return root;
        }

        public static AbstractNetworkItem openFromXml(XmlNode node)
        {
            PointF origo = new PointF(0, 0);
            PointF labelOffset = new PointF(0,0);
            PointF clockOffset = new PointF(0, 0);
            SizeF size = new SizeF(0, 0);
            XmlNodeList list = node.ChildNodes;
            List<PetriEvent> events = null;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_POINTF_NAMESPACE:
                        if ("Origo".Equals(localName))
                        {
                            origo = PetriXmlHelper.openPointF(childNode);
                        }
                        else if ("LabelOffset".Equals(localName))
                        {
                            labelOffset = PetriXmlHelper.openPointF(childNode);
                        }
                        else if ("ClockOffset".Equals(localName))
                        {
                            clockOffset = PetriXmlHelper.openPointF(childNode);
                        }
                        break;
                    case PetriXmlHelper.XML_SIZEF_NAMESPACE:
                        if ("Size".Equals(localName))
                        {
                            size = PetriXmlHelper.openSizeF(childNode);
                        }
                        break;
                    case PetriXmlHelper.XML_ITEM_NAMESPACE:
                        switch (localName)
                        {
                            case "Events":
                                events = PetriEvent.openEvents(childNode.ChildNodes, "ItemEvent");
                                break;
                        }
                        break;
                }
            }
            string name = AbstractItem.openNameAttrFromNode(node);
            long unid = AbstractItem.openUnidAttrFromNode(node);
            bool showAnnotation = AbstractItem.openShowAnnotationAttrFromNode(node);
            float angle = AbstractTransition.openAngleAttrFromNode(node);
            int priority = AbstractTransition.openPriorityAttrFromNode(node);
            TransitionType transitionType = AbstractTransition.openTransitionTypeAttrFromNode(node);
            int delay = AbstractTransition.openDelayAttrFromNode(node);
            float clockRadius = AbstractTransition.openClockRadiusAttrFromNode(node);
            Transition ret = new Transition(name, unid, showAnnotation, angle, origo, size, priority, transitionType, delay, clockRadius, clockOffset);
            ret.PetriEvents.addEvent(events);
            ret.LabelOffset = labelOffset;
            return ret;
        }

    }
}
