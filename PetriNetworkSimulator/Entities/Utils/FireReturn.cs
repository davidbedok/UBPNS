using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Entities.Utils
{
    public class FireReturn
    {
        private FireEvent fireEvent;
        private Transition fireTransition;

        public FireEvent FireEvent {
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

    }
}
