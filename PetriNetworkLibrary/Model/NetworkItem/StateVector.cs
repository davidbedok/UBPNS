using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Model.TokenPlayer;
using PetriNetworkLibrary.Model.Network;
using System.Xml;
using System.Drawing;

namespace PetriNetworkLibrary.Model.NetworkItem
{
    public class StateVector : AbstractEventDrivenItem
    {
        private readonly Dictionary<Int64, List<Token>> tokenDistribution;

        public Dictionary<Int64, List<Token>> TokenDistribution
        {
            get { return this.tokenDistribution; }
        }

        public StateVector(string name, long unid, PetriNetwork network)
            : base(name, unid)
        {
            this.tokenDistribution = new Dictionary<Int64, List<Token>>();
            foreach (Position position in network.Positions)
            {
                List<Token> copiedTokens = new List<Token>();
                copiedTokens.AddRange(position.Tokens);
                this.tokenDistribution.Add(position.Unid, copiedTokens);
            }
        }

        private StateVector(string name, long unid, Dictionary<Int64, List<Token>> tokenDistribution)
            : base(name, unid)
        {
            this.tokenDistribution = tokenDistribution;
        }

        public List<Token> getTokensByPositionUnid(long positionUnid)
        {
            return this.tokenDistribution[positionUnid];
        }

        public int getTokenCountByPositionUnid(long positionUnid)
        {
            return this.getTokensByPositionUnid(positionUnid).Count;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is StateVector))
            {
                return false;
            }
            return this.Equals((StateVector)obj);
        }

        public bool Equals(StateVector sv)
        {
            bool ret = false;
            if (sv != null)
            {
                ret = true;
                foreach (KeyValuePair<Int64, List<Token>> entry in sv.tokenDistribution)
                {
                    Int64 positionUnid = entry.Key;
                    if (this.tokenDistribution[positionUnid] != null)
                    {
                        List<Token> thisTokens = this.tokenDistribution[positionUnid];
                        List<Token> tokens = entry.Value;
                        if ((tokens != null) && (thisTokens != null))
                        {
                            if (tokens.Count != thisTokens.Count)
                            {
                                ret = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        ret = false;
                        break;
                    }
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
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("  --== STATEVECTOR ==--");
            sb.Append(base.ToString());
            sb.AppendLine("  tokenDistribution:");
            foreach (KeyValuePair<Int64, List<Token>> entry in this.tokenDistribution)
            {
                sb.AppendLine("    positionUnid: " + entry.Key);
                foreach (Token token in entry.Value)
                {
                    sb.AppendLine(token.ToString());
                }
            }
            return sb.ToString();
        }

        public static StateVector findItemByUnid(List<StateVector> states, long unid)
        {
            StateVector ret = null;
            int i = 0;
            bool find = false;
            while ((i < states.Count) && (!find))
            {
                if (unid.Equals(states[i].Unid))
                {
                    ret = states[i];
                    find = true;
                }
                i++;
            }
            return ret;
        }

        public static StateVector findItemByName(List<StateVector> states, string name)
        {
            StateVector ret = null;
            int i = 0;
            bool find = false;
            while ((i < states.Count) && (!find))
            {
                if (name.Equals(states[i].Name))
                {
                    ret = states[i];
                    find = true;
                }
                i++;
            }
            return ret;
        }

        private static List<Token> openTokensFromXml(XmlNodeList list, List<Token> alltokens)
        {
            List<Token> ret = new List<Token>();
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                if ((PetriXmlHelper.XML_STATE_NAMESPACE.Equals(namespaceUri)) && ("Token".Equals(localName)))
                {
                    long tokUnid = PetriXmlHelper.openLongAttributeFromNode(childNode, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
                    Token token = Token.findTokenByUnid(alltokens, tokUnid);
                    if (token == null)
                    {
                        token = new Token("", tokUnid, Color.Black);
                    }
                    ret.Add(token);
                }
            }
            return ret;
        }

        private static Dictionary<Int64, List<Token>> openTokenDistributionFromXml(XmlNodeList list, List<Token> alltokens)
        {
            Dictionary<Int64, List<Token>> ret = new Dictionary<Int64, List<Token>>();
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                if ((PetriXmlHelper.XML_STATE_NAMESPACE.Equals(namespaceUri)) && ("Position".Equals(localName)))
                {
                    long posUnid = PetriXmlHelper.openLongAttributeFromNode(childNode, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
                    ret.Add(posUnid, StateVector.openTokensFromXml(childNode.ChildNodes, alltokens));
                }
            }
            return ret;
        }

        internal static StateVector openFromXml(XmlNode node, List<Token> alltokens)
        {
            XmlNodeList list = node.ChildNodes;
            Dictionary<Int64, List<Token>> tokenDistribution = null;
            List<PetriEvent> events = null;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_STATE_NAMESPACE:
                        switch (localName)
                        {
                            case "TokenDistributions":
                                tokenDistribution = StateVector.openTokenDistributionFromXml(childNode.ChildNodes, alltokens);
                                break;
                            case "Events":
                                events = PetriEvent.openEvents(childNode.ChildNodes, "StateEvent");
                                break;
                        }
                        break;
                }
            }
            string name = PetriXmlHelper.openStringAttributeFromNode(node, "name", PetriXmlHelper.XML_STATE_NAMESPACE);
            long unid = PetriXmlHelper.openLongAttributeFromNode(node, "unid", PetriXmlHelper.XML_STATE_NAMESPACE);
            StateVector ret = new StateVector(name, unid, tokenDistribution);
            ret.EventTrunk.addEvents(events);
            return ret;
        }

    }
}
