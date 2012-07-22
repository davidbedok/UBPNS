using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkLibrary.Utility;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkLibrary.Model.Base
{
    public abstract partial class AbstractItem : System.Object
    {
        private const string XML_NAME = "name";
        private const string XML_UNID = "unid";
        private const string XML_SHOWANNOTATION = "showannotation";

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

        public AbstractItem(AbstractItemData itemData)
            : this(itemData.name, itemData.unid, itemData.showAnnotation)
        {
        }

        public AbstractItem(string name, long unid, bool showAnnotation)
        {
            this.name = name;
            this.unid = unid;
            this.showAnnotation = showAnnotation;
        }

        public override string ToString()
        {
            return "  " + this.name + " (unid: " + this.unid + ")";
        }

        protected static AbstractItem findItemByUnid(List<AbstractItem> items, long unid)
        {
            return items.Find(item => item.unid == unid);
            /*
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
            */
        }

    }
}
