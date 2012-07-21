using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.Item.Position
{
    partial class AbstractPosition
    {
        
        protected static int openCapacityLimitAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "capacitylimit", PetriXmlHelper.XML_POSITION_NAMESPACE);
        }

    }
}
