using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.NetworkItem;

namespace PetriNetworkLibrary.Utility
{
    public class FireReturn : System.Object
    {

        private FireEvent fireEvent;
        private Transition fireTransition;

        public FireEvent FireEvent
        {
            get { return this.fireEvent; }
        }

        public Transition FireTransition
        {
            get { return this.fireTransition; }
        }

        public FireReturn(FireEvent fireEvent, Transition fireTransition)
        {
            this.fireEvent = fireEvent;
            this.fireTransition = fireTransition;
        }

        public override string ToString()
        {
            String ftStr = "";
            if (fireTransition != null)
            {
                ftStr = " fireTransition: " + fireTransition.Name;
            }
            return "[FIRERETURN] fireEvent: " + fireEvent + ftStr;
        }

    }
}
