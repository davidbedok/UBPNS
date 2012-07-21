using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.NetworkItem;
using System.Xml;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Model.TokenPlayer;

namespace PetriNetworkLibrary.Model.State
{
    public class StateHierarchy
    {
        private List<StateVector> states;
        private List<EdgeStateState> edges;

        public List<StateVector> States
        {
            get { return this.states; }
        }

        public List<String> StatesName
        {
            get
            {
                List<String> ret = new List<String>();
                foreach (StateVector item in this.states)
                {
                    ret.Add(item.Name);
                }
                return ret;
            }
        }

        public StateHierarchy()
        {
            this.states = new List<StateVector>();
            this.edges = new List<EdgeStateState>();
        }

        internal void addState(StateVector state)
        {
            if (!(this.states.Contains(state)))
            {
                this.states.Add(state);
            }
        }

        internal void addStates(List<StateVector> states)
        {
            foreach (StateVector state in states)
            {
                this.addState(state);
            }
        }

        private void addEdge(EdgeStateState edge)
        {
            if (!(this.edges.Contains(edge)))
            {
                this.edges.Add(edge);
            }
        }

        internal void addEdges(List<EdgeStateState> edges)
        {
            foreach (EdgeStateState edge in edges)
            {
                this.addEdge(edge);
            }
        }

        internal void addEdge(StateVector start, StateVector end)
        {
            this.addEdge(new EdgeStateState(start, end));
        }

        public StateVector find(StateVector stateVector)
        {
            StateVector ret = null;
            foreach (StateVector sv in this.states)
            {
                if (sv.Equals(stateVector))
                {
                    ret = sv;
                    break;
                }
            }
            return ret;
        }

        internal void clear()
        {
            if ((edges != null) && (edges.Count > 0))
            {
                this.edges.Clear();
            }
            if ((states != null) && (states.Count > 0))
            {
                this.states.Clear();
            }
        }

        internal long calculateMaxStateUnid()
        {
            long maxUnid = -1;
            foreach (StateVector item in this.states)
            {
                if (maxUnid < item.Unid)
                {
                    maxUnid = item.Unid;
                }
            }
            return maxUnid;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            foreach (StateVector item in this.states)
            {
                sb.AppendLine(item.ToString());
            }
            foreach (EdgeStateState item in this.edges)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        internal static List<StateVector> openStatesFromXml(XmlNodeList root, List<Token> alltokens)
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

        internal static List<EdgeStateState> openEdgesFromXml(XmlNodeList root, List<StateVector> states)
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
