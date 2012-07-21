using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Exceptions;
using System.Collections;

namespace PetriNetworkSimulator.Entities.Enums
{
    public class MoveCorner : CustomEnum
    {
        public static readonly MoveCorner NONE = new MoveCorner("None", "NONE");
        public static readonly MoveCorner TOPLEFT = new MoveCorner("Top-left corner", "TOPLEFT");
        public static readonly MoveCorner TOPRIGHT = new MoveCorner("Top-right corner", "TOPRIGHT");
        public static readonly MoveCorner BOTTOMLEFT = new MoveCorner("Bottom-left corner", "BOTTOMLEFT");
        public static readonly MoveCorner BOTTOMRIGHT = new MoveCorner("Bottom-right corner", "BOTTOMRIGHT");
        public static readonly MoveCorner LABEL = new MoveCorner("Label", "LABEL");

        private static List<MoveCorner> values;

        public static MoveCorner[] Values
        {
            get {
                return MoveCorner.values.ToArray();
            }
        }

        private MoveCorner(string name, string value)
            : base(name, value)
        {
            MoveCorner.addNewItem(this);
        }

        public static MoveCorner getDefault()
        {
            return MoveCorner.NONE;
        }

        public static MoveCorner getEnumByValue(string value)
        {
            MoveCorner ret = MoveCorner.getDefault();
            MoveCorner[] items = MoveCorner.Values;
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

        private static void addNewItem(MoveCorner item)
        {
            if (MoveCorner.values == null)
            {
                MoveCorner.values = new List<MoveCorner>();
            }
            MoveCorner.values.Add(item);
        }

    }
}
