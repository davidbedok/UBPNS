using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Model.TokenPlayer;
using System.Xml;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Utility;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkLibrary.Model.NetworkItem
{
    public class Position : AbstractEventDrivenItem
    {

        private readonly int capacityLimit;
        private List<Token> tokens;

        public int CapacityLimit
        {
            get { return this.capacityLimit; }
        }

        public List<Token> Tokens
        {
            get { return this.tokens; }
        }

        public int TokenCount
        {
            get { return this.Tokens.Count; }
        }

        public Position(AbstractItemData itemData, int capacityLimit)
            : this(itemData.name, itemData.unid, itemData.showAnnotation, capacityLimit)
        {
        }

        public Position(string name, long unid, bool showAnnotation, int capacityLimit)
            : base(name, unid, showAnnotation)
        {
            this.capacityLimit = capacityLimit;
            this.tokens = new List<Token>();
        }

        internal long calculateMaxTokenUnid()
        {
            long maxUnid = -1;
            foreach (Token item in this.tokens)
            {
                if (maxUnid < item.Unid)
                {
                    maxUnid = item.Unid;
                }
            }
            return maxUnid;
        }

        public override void initStatistics()
        {   
            if (this.tokens != null)
            {
                this.statistics.init(this.tokens.Count);
            }
        }

        public bool hasEnoughTokens(int weight)
        {
            return (this.tokens.Count >= weight);
        }

        public bool isCapcityLimitInjured(int weight)
        {
            bool ret = false;
            if (this.capacityLimit > 0)
            {
                ret = ((this.tokens.Count + weight) > this.capacityLimit);
            }
            return ret;
        }

        internal void addToken(Token item)
        {
            if (!this.tokens.Contains(item))
            {
                this.tokens.Add(item);
            }
        }

        private void deleteToken(Token token)
        {
            if (this.tokens.Contains(token))
            {
                this.tokens.Remove(token);
            }
        }

        private Token takeAwayToken()
        {
            Token token = null;
            int count = this.tokens.Count;
            if (count > 0)
            {
                token = this.tokens[0];
                this.deleteToken(token);
            }
            return token;
        }

        internal List<Token> takeAwayTokens(bool changeStatistics)
        {
            return this.takeAwayTokens(this.tokens.Count, changeStatistics);
        }

        internal List<Token> takeAwayTokens(int weight, bool changeStatistics)
        {
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < weight; i++)
            {
                tokens.Add(this.takeAwayToken());
            }
            if (changeStatistics)
            {
                this.addStatistics();
            }
            return tokens;
        }

        internal void addStatistics()
        {
            if (this.tokens != null)
            {
                this.statistics.add(this.tokens.Count);
            }
        }

        internal void changeTokens(List<Token> tokens)
        {
            this.tokens = new List<Token>();
            this.tokens.AddRange(tokens);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("  --== POSITION ==--");
            sb.Append(base.ToString());
            sb.AppendLine("  capacityLimit: " + capacityLimit);
            foreach (Token token in this.tokens){
                sb.AppendLine(token.ToString());
            }
            return sb.ToString();
        }

        internal static AbstractEventDrivenItem openFromXml(XmlNode node)
        {
            List<Token> tokens = null;
            List<PetriEvent> events = null;
            XmlNodeList list = node.ChildNodes;
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                switch (namespaceUri)
                {
                    case PetriXmlHelper.XML_POSITION_NAMESPACE:
                        switch (localName)
                        {
                            case "Tokens":
                                tokens = Position.openTokensFromXml(childNode.ChildNodes);
                                break;
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
            int capacityLimit = Position.openCapacityLimitAttrFromNode(node);
            Position ret = new Position(AbstractItem.readItem(node), capacityLimit);
            ret.EventTrunk.addEvents(events);
            ret.tokens.AddRange(tokens);
            return ret;
        }

        private static List<Token> openTokensFromXml(XmlNodeList list)
        {
            List<Token> ret = new List<Token>();
            foreach (XmlNode childNode in list)
            {
                string namespaceUri = childNode.NamespaceURI;
                string localName = childNode.LocalName;
                if ((PetriXmlHelper.XML_TOKEN_NAMESPACE.Equals(namespaceUri)) && ("Token".Equals(localName)))
                {
                    ret.Add(Token.openFromXml(childNode));
                }
            }
            return ret;
        }

        private static int openCapacityLimitAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openIntAttributeFromNode(node, "capacitylimit", PetriXmlHelper.XML_POSITION_NAMESPACE);
        }

    }
}
