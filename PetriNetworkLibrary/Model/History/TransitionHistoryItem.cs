using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Model.TokenPlayer;

namespace PetriNetworkLibrary.Model.History
{
    public class TransitionHistoryItem : System.Object
    {
        private Transition transition;
        private string transitionNameAtFire;
        private List<Token> tokens;
        private List<string> nameOfTokensAtFire;

        public string TransitionNameAtFire
        {
            get { return transitionNameAtFire; }
        }

        public Transition Transition
        {
            get { return transition; }
        }

        public TransitionHistoryItem(Transition transition)
        {
            this.transition = transition;
            this.transitionNameAtFire = this.transition.Name;
            this.tokens = new List<Token>();
            this.nameOfTokensAtFire = new List<string>();
        }

        internal void addToken(List<Token> tokens)
        {
            if (tokens != null)
            {
                foreach (Token token in tokens)
                {
                    this.addToken(token);
                }
            }
        }

        private void addToken(Token item)
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
