using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.NetworkItem;
using System.Xml;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Model.State
{
    public class EdgeStateState
    {

        private StateVector startState;
        private StateVector endState;

        public StateVector Start
        {
            get { return this.startState; }
        }

        public StateVector End
        {
            get { return this.endState; }
        }

        public EdgeStateState(StateVector startState, StateVector endState)
        {
            this.startState = startState;
            this.endState = endState;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is EdgeStateState))
            {
                return false;
            }
            return this.Equals((EdgeStateState)obj);
        }

        public bool Equals(EdgeStateState sv)
        {
            bool ret = false;
            if (sv != null)
            {
                if ((sv.Start.Equals(this.startState)) && (sv.End.Equals(this.endState)))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "  [EdgeStateState] " + this.Start.Name + " -- " + this.End.Name;
        }

        internal static EdgeStateState openFromXml(XmlNode node, List<StateVector> states)
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
