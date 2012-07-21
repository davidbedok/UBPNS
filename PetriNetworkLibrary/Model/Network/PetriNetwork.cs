using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Model.NoteItem;
using System.Xml;
using PetriNetworkLibrary.Model.History;
using PetriNetworkLibrary.Model.TokenPlayer;
using System.Drawing;
using PetriNetworkLibrary.Model.State;

namespace PetriNetworkLibrary.Model.Network
{
    public partial class PetriNetwork : System.Object
    {
        private const int DEF_TIMEOUT = 100;

        private readonly Random rand;

        private readonly EventTrunk eventTrunk;
        private readonly string fileName;
        private readonly string name;
        private readonly string certificateSubject;
        private readonly DateTime lastModificationDate;
        private readonly string description;
        private readonly FireRule fireRule;
        private readonly List<AbstractItem> items;

        protected StateHierarchy stateHierarchy;

        protected long unidGenNumber;
        protected int stateGenNumber;
        protected string statePrefix;
        private List<TransitionHistoryItem> transitionHistory;
        private List<StateVector> stateMatrix;
        private StateVector actualStateVector;

        private Dictionary<String, PetriHandler> handlers;
        
        public EventTrunk EventTrunk
        {
            get { return this.eventTrunk; }
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string CertificateSubject
        {
            get { return this.certificateSubject; }
        }

        public DateTime LastModificationDate
        {
            get { return this.lastModificationDate; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public FireRule FireRule
        {
            get { return this.fireRule; }
        }

        private List<AbstractItem> Items
        {
            get { return this.items; }
        }

        public List<Position> Positions
        {
            get
            {
                List<Position> ret = new List<Position>();
                foreach (AbstractItem item in this.items)
                {
                    if (item is Position)
                    {
                        ret.Add((Position)item);
                    }
                }
                return ret;
            }
        }

        public List<Transition> Transitions
        {
            get
            {
                List<Transition> ret = new List<Transition>();
                foreach (AbstractItem item in this.items)
                {
                    if (item is Transition)
                    {
                        ret.Add((Transition)item);
                    }
                }
                return ret;
            }
        }

        private List<AbstractEventDrivenItem> EventDrivenItems
        {
            get
            {
                List<AbstractEventDrivenItem> ret = new List<AbstractEventDrivenItem>();
                foreach (AbstractItem item in this.items)
                {
                    if (item is AbstractEventDrivenItem)
                    {
                        ret.Add((AbstractEventDrivenItem)item);
                    }
                }
                return ret;
            }
        }

        public List<AbstractEdge> Edges
        {
            get
            {
                List<AbstractEdge> ret = new List<AbstractEdge>();
                foreach (AbstractItem item in this.items)
                {
                    if (item is AbstractEdge)
                    {
                        ret.Add((AbstractEdge)item);
                    }
                }
                return ret;
            }
        }

        public List<Token> Tokens
        {
            get
            {
                List<Token> ret = new List<Token>();
                foreach (Position position in this.Positions)
                {
                    ret.AddRange(position.Tokens);
                }
                return ret;
            }
        }

        public PetriNetwork(Random rand, string fileName, string name, string certificateSubject, DateTime lastModificationDate, string description, FireRule fireRule)
        {
            this.rand = rand;
            this.fileName = fileName;
            this.name = name;
            this.certificateSubject = certificateSubject;
            this.lastModificationDate = lastModificationDate;
            this.description = description;
            this.fireRule = fireRule;
            this.eventTrunk = new EventTrunk();
            this.items = new List<AbstractItem>();
            this.stateHierarchy = new StateHierarchy();
            this.transitionHistory = new List<TransitionHistoryItem>();
            this.stateMatrix = new List<StateVector>();
            this.handlers = new Dictionary<String, PetriHandler>();

            this.unidGenNumber = 0;
            this.stateGenNumber = 0;
            this.statePrefix = "S";
        }

        public void initStatistics()
        {
            foreach (AbstractEventDrivenItem item in this.items)
            {
                item.initStatistics();
            }
        }

        public void clearTransitionHistory()
        {
            this.transitionHistory.Clear();
        }

        protected void addItemToTransitionHistory(TransitionHistoryItem item)
        {
            this.transitionHistory.Add(item);
        }

        private void setNextUnid()
        {
            this.unidGenNumber = this.calculateMaxUnid() + 1;
        }

        private long calculateMaxUnid()
        {
            long maxUnid = -1;
            foreach (AbstractItem item in this.items)
            {
                if (maxUnid < item.Unid)
                {
                    maxUnid = item.Unid;
                }
                if (item is Position)
                {
                    Position position = (Position)item;
                    long tokenMaxUnid = position.calculateMaxTokenUnid();
                    if (maxUnid < tokenMaxUnid)
                    {
                        maxUnid = tokenMaxUnid;
                    }
                }
            }
            long stateMaxUnid = this.stateHierarchy.calculateMaxStateUnid();
            if (maxUnid < stateMaxUnid)
            {
                maxUnid = stateMaxUnid;
            }
            return maxUnid;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("--== PETRI NETWORK ==--");
            sb.AppendLine("events: " + eventTrunk.ToString());
            sb.AppendLine("fileName: " + fileName);
            sb.AppendLine("name: " + name);
            sb.AppendLine("certificateSubject: " + certificateSubject);
            sb.AppendLine("lastModificationDate: " + lastModificationDate);
            sb.AppendLine("description: " + description);
            sb.AppendLine("fireRule: " + fireRule);
            foreach (AbstractItem item in this.items)
            {
                sb.Append(item.ToString());
            }
            sb.AppendLine(this.stateHierarchy.ToString());
            return sb.ToString();
        }

    }
}
