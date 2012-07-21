using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Event
{
    public class EventTrunk : System.Object
    {

        private readonly List<PetriEvent> events;

        public List<PetriEvent> Events
        {
            get { return this.events; }
        }

        public EventTrunk()
        {
            this.events = new List<PetriEvent>();
        }

        public void addEvent(PetriEvent petriEvent)
        {
            if (!this.events.Contains(petriEvent))
            {
                this.events.Add(petriEvent);
            }
        }

        internal void addEvents(List<PetriEvent> petriEvents)
        {
            foreach (PetriEvent pe in petriEvents)
            {
                this.addEvent(pe);
            }
        }

        public List<PetriEvent> getEventsByType(EventType eventType)
        {
            List<PetriEvent> ret = new List<PetriEvent>();
            foreach (PetriEvent petriEvent in this.events)
            {
                if (eventType.Equals(petriEvent.Type))
                {
                    ret.Add(petriEvent);
                }
            }
            return ret;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            foreach (PetriEvent petriEvent in this.events)
            {
                sb.Append(petriEvent.ToString()+" ");
            }
            return sb.ToString();
        }

    }
}
