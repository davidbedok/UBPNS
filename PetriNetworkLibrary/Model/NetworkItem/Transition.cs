using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using System.Xml;
using PetriNetworkLibrary.Event;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkLibrary.Model.NetworkItem
{
    public class Transition : AbstractEventDrivenItem
    {

        private readonly int priority;
        private readonly TransitionType transitionType;
        private readonly int delay;

        public int Priority
        {
            get { return this.priority; }
        }

        public TransitionType TransitionType
        {
            get { return this.transitionType; }
        }

        public int Delay
        {
            get { return this.delay; }
        }

        public Transition(AbstractItemData itemData, int priority, TransitionType transitionType, int delay)
            : this(itemData.name, itemData.unid, itemData.showAnnotation, priority, transitionType, delay)
        {
        }

        public Transition(string name, long unid, bool showAnnotation, int priority, TransitionType transitionType, int delay)
            : base(name, unid, showAnnotation)
        {
            this.priority = priority;
            this.transitionType = transitionType;
            this.delay = delay;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("  --== TRANSITION ==--");
            sb.Append(base.ToString());
            sb.AppendLine("  priority: " + priority);
            sb.AppendLine("  transitionType: " + transitionType);
            sb.AppendLine("  delay: " + delay);
            return sb.ToString();
        }

        internal static AbstractEventDrivenItem openFromXml(XmlNode node)
        {
            XmlNodeList list = node.ChildNodes;
            List<PetriEvent> events = null;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
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
            int priority = Transition.openPriorityAttrFromNode(node);
            TransitionType transitionType = Transition.openTransitionTypeAttrFromNode(node);
            int delay = Transition.openDelayAttrFromNode(node);
            Transition ret = new Transition(AbstractItem.readItem(node), priority, transitionType, delay);
            ret.EventTrunk.addEvents(events);
            return ret;
        }

        private static int openPriorityAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "priority", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
        }

        private static TransitionType openTransitionTypeAttrFromNode(XmlNode node)
        {
            XmlAttribute attr = node.Attributes["type", PetriXmlHelper.XML_TRANSITION_NAMESPACE];
            return (TransitionType)Enum.Parse(typeof(TransitionType), attr.Value); ;
        }

        private static int openDelayAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "delay", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
        }


    }
}
