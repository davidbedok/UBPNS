using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Model.Base
{
    public abstract class AbstractItem : System.Object
    {
        protected readonly string name;
        protected readonly long unid;

        public string Name
        {
            get { return this.name; }
        }

        public long Unid
        {
            get { return this.unid; }
        }

        public AbstractItem(string name, long unid)
        {
            this.name = name;
            this.unid = unid;
        }

        public override string ToString()
        {
            return "  " + this.name + " (unid: " + this.unid + ")";
        }

        protected static string openNameAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openStringAttributeFromNode(node, "name", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static long openUnidAttrFromNode(XmlNode node)
        {
            return PetriXmlHelper.openLongAttributeFromNode(node, "unid", PetriXmlHelper.XML_BASEITEM_NAMESPACE);
        }

        protected static AbstractItem findItemByUnid(List<AbstractItem> items, long unid)
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

    }
}
