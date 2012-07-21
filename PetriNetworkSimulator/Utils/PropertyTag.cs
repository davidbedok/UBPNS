using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Event;

namespace PetriNetworkSimulator.Utils
{
    public class PropertyTag
    {
        private readonly AbstractNetwork network;
        private readonly AbstractItem parent;
        private readonly AbstractItem item;
        private readonly NetworkProperty property;
        private readonly PetriEvent petriEvent;
        private readonly Object parentObject;

        public AbstractNetwork Network
        {
            get { return this.network; }
        }

        public AbstractItem Parent
        {
            get { return this.parent; }
        }

        public AbstractItem Item
        {
            get { return this.item; }
        }

        public NetworkProperty Property
        {
            get { return this.property; }
        }

        public Object ParentObject
        {
            get { return this.parentObject; }
        }

        public PetriEvent PetriEvent
        {
            get { return this.petriEvent; }
        }

        public PropertyTag(AbstractItem parent, AbstractItem item, NetworkProperty property)
        {
            this.parent = parent;
            this.item = item;
            this.property = property;
        }

        public PropertyTag(NetworkProperty property, AbstractNetwork network) 
            : this(null, null, property)
        {
            this.network = network;
        }

        public PropertyTag(Object parentObject, PetriEvent petriEvent)
        {
            this.parentObject = parentObject;
            this.petriEvent = petriEvent;
        }

    }
}
