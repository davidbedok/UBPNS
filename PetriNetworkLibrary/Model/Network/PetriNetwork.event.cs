using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Model.NetworkItem;

namespace PetriNetworkLibrary.Model.Network
{
    public partial class PetriNetwork
    {

        private List<PetriEvent> Events
        {
            get
            {
                List<PetriEvent> ret = new List<PetriEvent>();
                ret.AddRange(this.EventTrunk.Events);
                foreach (AbstractEventDrivenItem item in this.EventDrivenItems)
                {
                    ret.AddRange(item.EventTrunk.Events);
                }
                foreach (StateVector item in this.stateHierarchy.States)
                {
                    ret.AddRange(item.EventTrunk.Events);
                }
                return ret;
            }
        }

        public List<String> EventsName
        {
            get
            {
                List<String> ret = new List<String>();
                List<PetriEvent> events = this.Events;
                foreach (PetriEvent item in events)
                {
                    if (!ret.Contains(item.Name))
                    {
                        ret.Add(item.Name);
                    }
                }
                return ret;
            }
        }

        public List<String> StatesName
        {
            get
            {
                return this.stateHierarchy.StatesName;
            }
        }

        public void bindPetriEvent(string eventName, PetriHandler handler)
        {
            if (this.EventsName.Contains(eventName))
            {
                if (this.handlers.ContainsKey(eventName))
                {
                    this.handlers[eventName] += handler;
                }
                else
                {
                    this.handlers.Add(eventName, handler);
                }
            }
            else
            {
                throw new ArgumentException("Cannot bind event, because '" + eventName + "' is not exists as a petri event in this network.");
            }
        }

        public void unbindPetriEvent(string eventName, PetriHandler handler)
        {
            if (this.EventsName.Contains(eventName))
            {
                if (this.handlers.ContainsKey(eventName))
                {
                    this.handlers[eventName] -= handler;
                }
                else
                {
                    throw new ArgumentException("Cannot unbind event, because '" + eventName + "' is not bind to this network previously.");
                }
            }
            else
            {
                throw new ArgumentException("Cannot unbind event, because '" + eventName + "' is not exists as a petri event in this network.");
            }
        }

        private PetriHandler getPetriEventByName(string eventName)
        {
            PetriHandler handler = null;
            if (this.handlers.ContainsKey(eventName))
            {
                handler = this.handlers[eventName];
            }
            return handler;
        }

        private PetriHandler getPetriEventByTrunk(List<PetriEvent> events)
        {
            PetriHandler handler = null;
            foreach (PetriEvent petriEvent in events)
            {
                handler += this.getPetriEventByName(petriEvent.Name);
            }
            return handler;
        }

        private void checkHandler(AbstractEventDrivenItem item, EventType eventType)
        {
            List<PetriEvent> events = item.EventTrunk.getEventsByType(eventType);
            PetriHandler handler = getPetriEventByTrunk(events);
            if (handler != null)
            {
                handler(item, eventType);
            }
        }

    }
}
