using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Enums
{
    public class FireRule : CustomEnum
    {
        public static readonly FireRule RANDOM = new FireRule("Random", "RANDOM");
        public static readonly FireRule ASC_UNID = new FireRule("Ascendant Unid", "ASC_UNID");
        public static readonly FireRule DESC_UNID = new FireRule("Decrescent Unid", "DESC_UNID");
        public static readonly FireRule PRIORITY = new FireRule("Priority", "PRIORITY");

        private static List<FireRule> values;

        public static FireRule[] Values
        {
            get {
                return FireRule.values.ToArray();
            }
        }

        private FireRule(string name, string value)
            : base(name, value)
        {
            FireRule.addNewItem(this);
        }

        public static FireRule getDefault()
        {
            return FireRule.RANDOM;
        }

        public static FireRule getEnumByValue(string value)
        {
            FireRule ret = FireRule.getDefault();
            FireRule[] items = FireRule.Values;
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

        private static void addNewItem(FireRule item)
        {
            if (FireRule.values == null)
            {
                FireRule.values = new List<FireRule>();
            }
            FireRule.values.Add(item);
        }

    }
}
