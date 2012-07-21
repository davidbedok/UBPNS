using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Enums
{
    public class EventType : CustomEnum
    {
        public static readonly EventType PREACTIVATE = new EventType("Pre activate", "PREACTIVATE");
        public static readonly EventType POSTACTIVATE = new EventType("Post activate", "POSTACTIVATE");
        public static readonly EventType DEADLOCK = new EventType("Deadlock", "DEADLOCK");
        public static readonly EventType CYCLE = new EventType("Cycle", "CYCLE");
        public static readonly EventType TICK = new EventType("Tick", "TICK");

        private static List<EventType> values;

        public static EventType[] Values
        {
            get {
                return EventType.values.ToArray();
            }
        }

        private EventType(string name, string value)
            : base(name, value)
        {
            EventType.addNewItem(this);
        }

        public static EventType getDefault()
        {
            return EventType.PREACTIVATE;
        }

        public static EventType getEnumByValue(string value)
        {
            EventType ret = EventType.getDefault();
            EventType[] items = EventType.Values;
            bool find = false;
            int i = 0;
            while ((i < items.Length) && (!find))
            {
                if (items[i].value.Equals(value.ToUpper()))
                {
                    ret = items[i];
                    find = true;
                }
                ++i;
            }
            return ret;
        }

        private static void addNewItem(EventType item)
        {
            if (EventType.values == null)
            {
                EventType.values = new List<EventType>();
            }
            EventType.values.Add(item);
        }

    }
}
