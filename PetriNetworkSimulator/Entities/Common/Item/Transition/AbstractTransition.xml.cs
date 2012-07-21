using System.Xml;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Item.Transition
{
    partial class AbstractTransition
    {

        protected override XmlElement saveToFile(XmlDocument doc, XmlElement root)
        {
            base.saveToFile(doc, root);
            root.AppendChild(PetriXmlHelper.savePointF(doc, this.clockOffset, "ClockOffset"));
            XmlAttribute angle = doc.CreateAttribute(PetriXmlHelper.XML_TRANSITION_NAMESPACE_PREFIX, "angle", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
            angle.Value = this.angle.ToString();
            root.SetAttributeNode(angle);
            XmlAttribute priority = doc.CreateAttribute(PetriXmlHelper.XML_TRANSITION_NAMESPACE_PREFIX, "priority", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
            priority.Value = this.priority.ToString();
            root.SetAttributeNode(priority);
            XmlAttribute transitionType = doc.CreateAttribute(PetriXmlHelper.XML_TRANSITION_NAMESPACE_PREFIX, "type", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
            transitionType.Value = this.transitionType.Value;
            root.SetAttributeNode(transitionType);
            XmlAttribute delay = doc.CreateAttribute(PetriXmlHelper.XML_TRANSITION_NAMESPACE_PREFIX, "delay", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
            delay.Value = this.delay.ToString();
            root.SetAttributeNode(delay);
            XmlAttribute clockRadius = doc.CreateAttribute(PetriXmlHelper.XML_TRANSITION_NAMESPACE_PREFIX, "clockRadius", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
            clockRadius.Value = this.clockRadius.ToString();
            root.SetAttributeNode(clockRadius);
            return root;
        }

        protected static float openAngleAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openFloatAttributeFromNode(node, "angle", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
        }

        protected static int openPriorityAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "priority", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
        }

        protected static TransitionType openTransitionTypeAttrFromNode(XmlNode node)
        {
            XmlAttribute attr = node.Attributes["type", PetriXmlHelper.XML_TRANSITION_NAMESPACE];
            return TransitionType.getEnumByValue(attr.Value);
        }

        protected static int openDelayAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "delay", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
        }

        protected static float openClockRadiusAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openFloatAttributeFromNode(node, "clockRadius", PetriXmlHelper.XML_TRANSITION_NAMESPACE);
        }

    }
}
