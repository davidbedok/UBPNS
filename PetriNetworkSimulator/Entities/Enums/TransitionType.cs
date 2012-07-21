using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Enums
{
    public class TransitionType : CustomEnum
    {
        
        public static readonly TransitionType NORMAL = new TransitionType("Normal", "NORMAL");
        public static readonly TransitionType SOURCE = new TransitionType("Source", "SOURCE");
        public static readonly TransitionType SINK = new TransitionType("Sink", "SINK");
        
        private static List<TransitionType> values;

        public static TransitionType[] Values
        {
            get {
                return TransitionType.values.ToArray();
            }
        }

        private TransitionType(string name, string value)
            : base(name, value)
        {
            TransitionType.addNewItem(this);
        }

        public static TransitionType getDefault()
        {
            return TransitionType.NORMAL;
        }

        public static TransitionType getEnumByValue(string value)
        {
            TransitionType ret = TransitionType.getDefault();
            TransitionType[] items = TransitionType.Values;
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

        private static void addNewItem(TransitionType item)
        {
            if (TransitionType.values == null)
            {
                TransitionType.values = new List<TransitionType>();
            }
            TransitionType.values.Add(item);
        }

    }
}
