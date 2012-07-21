using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Model.Base
{
    public abstract class AbstractEventDrivenItem : AbstractItem
    {
        protected readonly EventTrunk eventTrunk;
        protected readonly GeneralStatistics statistics;

        public EventTrunk EventTrunk
        {
            get { return this.eventTrunk; }
        }

        public GeneralStatistics Statistics
        {
            get { return this.statistics; }
        }

        public AbstractEventDrivenItem(string name, long unid)
            : base(name, unid)
        {
            this.statistics = new GeneralStatistics();
            this.eventTrunk = new EventTrunk();
        }

        public virtual void initStatistics()
        {
            this.statistics.init();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine(base.ToString());
            sb.AppendLine("  events: " + eventTrunk.ToString());
            sb.AppendLine("  statistics: " + statistics.ToString());
            return sb.ToString();
        }

        public static AbstractEventDrivenItem findNetworkItemByUnid(List<AbstractEventDrivenItem> items, long unid)
        {
            AbstractEventDrivenItem ret = null;
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
