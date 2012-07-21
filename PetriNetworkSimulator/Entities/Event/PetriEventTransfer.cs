using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkSimulator.Entities.Event
{
    public class PetriEventTransfer
    {

        private PetriEvent petriEvent;
        private IPetriEvent parent;

        public PetriEvent PetriEvent
        {
            get { return this.petriEvent; }
        }

        public IPetriEvent Parent
        {
            get { return this.parent; }
        }

        public PetriEventTransfer(PetriEvent petriEvent, IPetriEvent parent)
        {
            this.petriEvent = petriEvent;
            this.parent = parent;
        }

        public override string ToString()
        {
            string name = "";
            if (( this.parent != null ) && (this.parent is IPetriItem)) {
                IPetriItem ipi = ((IPetriItem)this.parent);
                name = ipi.Name + " [" + ipi.TypeStr + "]";
            }
            return name + " - " +this.petriEvent.ToString();
        }

    }
}
