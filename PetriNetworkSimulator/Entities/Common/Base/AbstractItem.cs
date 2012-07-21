using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing.Drawing2D;

namespace PetriNetworkSimulator.Entities.Common.Base
{
    public abstract partial class AbstractItem
    {

        public static readonly Matrix MATRIX = new Matrix();

        protected string name;
        protected long unid;
        protected bool showAnnotation;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public long Unid
        {
            get { return this.unid; }
        }

        public bool ShowAnnotation
        {
            get { return this.showAnnotation; }
            set { this.showAnnotation = value; }
        }

        public AbstractItem(string name, long unid, bool showAnnotation)
        {
            this.name = name;
            this.unid = unid;
            this.showAnnotation = showAnnotation;
        }

        public static AbstractItem findItemByUnid(List<AbstractItem> items, long unid)
        {
            AbstractItem ret = null;
            int i = 0;
            bool find = false;
            while ((i < items.Count) && (!find))
            {
                if (unid.Equals(items[i].Unid))
                {
                    ret = items[i];
                    find = true;
                }
                i++;
            }
            return ret;
        }

        public override string ToString()
        {
            return this.name + " (unid: "+this.unid+")";
        }

    }
}
