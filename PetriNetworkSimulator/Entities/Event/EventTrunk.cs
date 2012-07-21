using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Entities.Event
{
    public class EventTrunk
    {
        private List<PetriEvent> events;

        public List<PetriEvent> Events
        {
            get { return this.events; }
        }

        public EventTrunk()
        {
            this.events = new List<PetriEvent>();
        }

        private bool allowEventType()
        {
            return true;
        }

        private void addEvent(PetriEvent petriEvent)
        {
            if (!this.events.Contains(petriEvent))
            {
                this.events.Add(petriEvent);
            }
        }

        private void deleteEvent(PetriEvent petriEvent)
        {
            if (this.events.Contains(petriEvent))
            {
                this.events.Remove(petriEvent);
            }
        }

        public void addEvent(List<PetriEvent> petriEvents)
        {
            foreach (PetriEvent pe in petriEvents)
            {
                this.addEvent(pe);
            }
        }

        public void modifyEvent(EventType type, string name)
        {
            if (name != null)
            {
                // TODO - allow events handing (via delegate)
                if (this.allowEventType())
                {
                    PetriEvent pe = this.getEventFromList(type);
                    name = name.Trim();
                    if (!name.Equals(""))
                    {
                        if (pe != null)
                        {
                            pe.Name = name;
                        }
                        else
                        {
                            this.addEvent(new PetriEvent(type, name));
                        }
                    }
                    else
                    {
                        if (pe != null)
                        {
                            this.deleteEvent(pe);
                        }
                    }
                }
            }
        }

        private PetriEvent getEventFromList(EventType type)
        {
            PetriEvent ret = null;
            foreach (PetriEvent pe in this.events)
            {
                if (pe.Type.Equals(type))
                {
                    ret = pe;
                    break;
                }
            }
            return ret;
        }

        public PetriEvent getEvent(EventType type)
        {
            PetriEvent ret = this.getEventFromList(type);
            if (ret == null)
            {
                ret = new PetriEvent(type, "");
            }
            return ret;
        }
        
    }
}
