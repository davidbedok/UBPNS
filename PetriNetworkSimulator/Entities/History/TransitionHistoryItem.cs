using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Entities.TokenPlayer;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;

namespace PetriNetworkSimulator.Entities.History
{
    public class TransitionHistoryItem
    {

        private Transition transition;
        private string transitionNameAtFire;
        private List<AbstractToken> tokens;
        private List<string> nameOfTokensAtFire;

        public string TransitionNameAtFire
        {
            get { return transitionNameAtFire; }
            set { transitionNameAtFire = value; }
        }

        public Transition Transition
        {
            get { return transition; }
            set { transition = value; }
        }

        public TransitionHistoryItem(Transition transition)
        {
            this.transition = transition;
            this.transitionNameAtFire = this.transition.Name;
            this.tokens = new List<AbstractToken>();
            this.nameOfTokensAtFire = new List<string>();
        }

        public void addToken(List<AbstractToken> tokens)
        {
            if (tokens != null)
            {
                foreach (AbstractToken token in tokens)
                {
                    this.addToken(token);
                }
            }
        }

        public void addToken(AbstractToken item)
        {
            if (item != null)
            {
                this.tokens.Add(item);
                this.nameOfTokensAtFire.Add(item.Name);
            }
        }

        public override string ToString()
        {
            string tokenNames = "";
            if (this.nameOfTokensAtFire.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" (");
                foreach (string name in this.nameOfTokensAtFire)
                {
                    sb.Append(name + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(")");
                tokenNames = sb.ToString();
            }
            return this.transitionNameAtFire + tokenNames;
        }

    }
}
