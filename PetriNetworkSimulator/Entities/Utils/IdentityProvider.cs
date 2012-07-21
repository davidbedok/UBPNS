using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Utils
{
    public class IdentityProvider
    {

        private int tokenGenNumber;
        private int positionGenNumber;
        private int transitionGenNumber;
        private int noteGenNumber;

        private string tokenPrefix;
        private string positionPrefix;
        private string transitionPrefix;
        private string notePrefix;

        #region Readonly properties

        public int TokenGenNumber
        {
            get { return this.tokenGenNumber; }
        }

        public int PositionGenNumber
        {
            get { return this.positionGenNumber; }
        }

        public int TransitionGenNumber
        {
            get { return this.transitionGenNumber; }
        }

        public int NoteGenNumber
        {
            get { return this.noteGenNumber; }
        }

        #endregion

        public string TokenPrefix
        {
            get { return this.tokenPrefix; }
            set { this.tokenPrefix = value; }
        }

        public string PositionPrefix
        {
            get { return this.positionPrefix; }
            set { this.positionPrefix = value; }
        }

        public string TransitionPrefix
        {
            get { return this.transitionPrefix; }
            set { this.transitionPrefix = value; }
        }

        public string NotePrefix
        {
            get { return this.notePrefix; }
            set { this.notePrefix = value; }
        }

        public IdentityProvider(string positionPrefix, string transitionPrefix, string tokenPrefix, string notePrefix, int tokenGenNumber, int positionGenNumber, int transitionGenNumber, int noteGenNumber)
        {
            this.tokenGenNumber = tokenGenNumber;
            this.positionGenNumber = positionGenNumber;
            this.transitionGenNumber = transitionGenNumber;
            this.noteGenNumber = noteGenNumber;

            this.positionPrefix = positionPrefix;
            this.transitionPrefix = transitionPrefix;
            this.tokenPrefix = tokenPrefix;
            this.notePrefix = notePrefix;
        }

        public string positionIdentity(string name)
        {
            if ((name == null) || ("".Equals(name)))
            {
                name = this.positionPrefix + (++this.positionGenNumber).ToString();
            }
            return name;
        }

        public string transitionIdentity(string name)
        {
            if ((name == null) || ("".Equals(name)))
            {
                name = this.transitionPrefix + (++this.transitionGenNumber).ToString();
            }
            return name;
        }

        public string noteIdentity(string name)
        {
            if ((name == null) || ("".Equals(name)))
            {
                name = this.notePrefix + (++this.noteGenNumber).ToString();
            }
            return name;
        }

        public string tokenIdentity(string name)
        {
            if ((name == null) || ("".Equals(name)))
            {
                name = this.tokenPrefix + (++this.tokenGenNumber).ToString();
            }
            return name;
        }

    }
}
