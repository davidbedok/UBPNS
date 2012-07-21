using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.State.Vector;

namespace PetriNetworkSimulator.Entities.Event
{
    public partial class PetriEvent
    {

        private EventType type;
        private string name;
        
        public EventType Type
        {
            get { return this.type; }
        }

        public string Name {
            get { return this.name; }
            set {
                string val = value;
                if (val != null)
                {
                    val = val.Trim();
                    val = val.Replace(" ", "_");
                    if (val.Equals(""))
                    {
                        val = null;
                    }
                }
                this.name = val;
            }
        }

        public bool Adjusted
        {
            get { return (this.name != null); }
        }

        public PetriEvent( EventType type, string name )
        {
            this.type = type;
            this.name = name;
        }

        public override string ToString()
        {
            return "Event " + this.name + " (" + this.type + ")";
        }

    }
}
