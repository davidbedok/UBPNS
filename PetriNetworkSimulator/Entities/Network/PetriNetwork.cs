using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Edge;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Item.NetNote;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.TokenPlayer;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.RecentFiles;
using PetriNetworkSimulator.Entities.History;

namespace PetriNetworkSimulator.Entities.Network
{
    public delegate void DimensionHandler(int width, int height);

    public partial class PetriNetwork : AbstractNetwork, IPetriItem
    {
        public const int WIDTH_MIN = 200;
        public const int WIDTH_MAX = 2048;
        public const int HEIGHT_MIN = 150;
        public const int HEIGHT_MAX = 1536;
        public const int MAX_EDGE_WIDTH = 50;

        private const int DEF_TIMEOUT = 100;
        public const int MIN_TIMEOUT = 50;

        private const int SWAP_TOKEN_NUMBER = 10;

        private string fileName;
        private string name;
        private string certificateSubject;
        private DateTime lastModificationDate;
        private int width;
        private int height;
        private string description;
        private IdentityProvider identityProvider;
        private int defaultEdgeWeight;
        private FireRule fireRule;
        private int simulationTimeout;
        private bool activeSimulation;

        public event DimensionHandler dimensionChanged;

        #region Readonly properties

        public string CertificateSubject
        {
            get { return this.certificateSubject; }
        }

        public string TmpFileName
        {
            get { return ((this.name != null && !"".Equals(this.name)) ? this.name : "tmp") + ".pn.xml"; }
        }

        public IdentityProvider IdentityProvider
        {
            get { return this.identityProvider; }
        }

        public RecentFile PetriRecentFile {
            get {
                RecentFile ret = null;
                if ( this.isSaved() ) {
                    ret = new RecentFile();
                    ret.NetworkName = this.name;
                    ret.FileName = this.fileName;
                    ret.Description = this.certificateSubject + " (" + this.lastModificationDate + ") " + this.description;
                }
                return ret;
            }
        }

        public DateTime LastModificationDate
        {
            get { return this.lastModificationDate; }
        }

        #endregion

        #region Properties

        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public FireRule ActualFireRule
        {
            get { return this.fireRule; }
            set { this.fireRule = value; }
        }

        public int DefaultEdgeWeight
        {
            get { return this.defaultEdgeWeight; }
            set { this.defaultEdgeWeight = value; }
        }

        public int Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
                if (this.dimensionChanged != null)
                {
                    this.dimensionChanged(this.width, this.height);
                }
            }
        }

        public int Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
                if (this.dimensionChanged != null)
                {
                    this.dimensionChanged(this.width, this.height);
                }
            }
        }

        public int SimulationTimeout
        {
            get { return this.simulationTimeout; }
            set {
                if (value >= PetriNetwork.MIN_TIMEOUT)
                {
                    this.simulationTimeout = value;
                }
            }
        }

        public bool ActiveSimulation
        {
            get { return this.activeSimulation; }
            set { this.activeSimulation = value; }
        }

        #endregion

        #region Additional properties (getters)

        public List<Position> Positions
        {
            get {
                List<Position> ret = new List<Position>();
                foreach (AbstractNetworkItem item in this.items)
                {
                    if (item is Position)
                    {
                        ret.Add((Position)item);
                    }
                }
                return ret;
            }
        }

        public List<AbstractItem> Tokens
        {
            get
            {
                List<AbstractItem> ret = new List<AbstractItem>();
                foreach (Position position in this.Positions)
                {
                    ret.AddRange(position.Tokens);   
                }
                return ret;
            }
        }

        public List<Transition> Transitions
        {
            get
            {
                List<Transition> ret = new List<Transition>();
                foreach (AbstractNetworkItem item in this.items)
                {
                    if (item is Transition)
                    {
                        ret.Add((Transition)item);
                    }
                }
                return ret;
            }
        }

        public string Title
        {
            get { return this.name + (this.fileName != null ? " (" + this.fileName + ")" : ""); }
        }

        private List<PetriEvent> Events
        {
            get
            {
                List<PetriEvent> ret = new List<PetriEvent>();
                ret.AddRange(this.PetriEvents.Events);
                foreach (AbstractNetworkItem item in this.items)
                {
                    ret.AddRange(item.PetriEvents.Events);
                }
                foreach (StateVector item in this.stateHierarchy.States)
                {
                    ret.AddRange(item.PetriEvents.Events);
                }
                return ret;
            }
        }

        /*
        private Dictionary<Object, List<PetriEvent>> EventsWithParent
        {
            get
            {
                List<Dictionary<Object, PetriEvent>> ret = new Dictionary<Object, List<PetriEvent>>();
                ret.Add(this, this.PetriEvents.Events);
                
                foreach (PetriEvent pe in this.PetriEvents.Events){
                    ret.Add(this, this.PetriEvents.Events);
                }
                
                foreach (AbstractNetworkItem item in this.items)
                {
                    ret.AddRange(item.PetriEvents.Events);
                }
                foreach (StateVector item in this.stateHierarchy.States)
                {
                    ret.AddRange(item.PetriEvents.Events);
                }
                return ret;
            }
        }
        */

        public List<String> StringEvents
        {
            get
            {
                List<String> ret = new List<String>();
                List<PetriEvent> events = this.Events;
                foreach (PetriEvent item in events)
                {
                    if (!ret.Contains(item.Name))
                    {
                        ret.Add(item.Name);
                    }
                }
                return ret;
            }
        }

        #endregion

        public PetriNetwork(Random rand, string name, string description, int width, int height, int sh_width, int sh_height, IdentityProvider identityProvider, string statePrefix, int defaultEdgeWeight, int stateGenNumber, string fileName, FireRule fireRule, int simulationTimeout, string certificateSubject, DateTime lastModificationDate)
            : base(rand, stateGenNumber, statePrefix, sh_width, sh_height)
        {
            this.name = name;
            this.description = description;
            this.fileName = fileName;
            this.width = width;
            this.height = height;
            this.identityProvider = identityProvider;  
            this.defaultEdgeWeight = defaultEdgeWeight;
            this.fireRule = fireRule;
            this.simulationTimeout = simulationTimeout;
            this.activeSimulation = false;
            this.certificateSubject = certificateSubject;
            this.lastModificationDate = lastModificationDate;
        }

        public bool isSaved()
        {
            return (this.fileName != null) && (!"".Equals(this.fileName));
        }

        public void draw(Graphics g, AbstractNetworkItem edgeStartItem, bool showHelpEllipse)
        {
            bool markPosition = false;
            bool markTransition = false;
            bool mark = false;
            bool markAsReadyToFire = false;
            if (edgeStartItem != null)
            {
                if (edgeStartItem is Position)
                {
                    markTransition = true;
                }
                else
                {
                    markPosition = true;
                }
            }
            List<Transition> readyToFireTransitions = null;
            if (this.visibleSettings.VisibleReadyToFireTransitions)
            {
                readyToFireTransitions = this.getReadyToFireTransition();
            }
            foreach (AbstractNetworkItem item in this.items)
            {
                mark = false;
                markAsReadyToFire = false;
                if ((item is Position) && (markPosition))
                {
                    mark = true;
                }
                else if ((item is Transition) && (markTransition))
                {
                    mark = true;
                }
                else if ((this.connectedItems != null) && (this.connectedItems.Contains(item)))
                {
                    mark = true;
                }
                if ((item is Transition) && (readyToFireTransitions != null) && (readyToFireTransitions.Contains(item)))
                {
                    markAsReadyToFire = true;
                }
                item.draw(g, this.isSelected(item), mark, this.visualSettings, this.visibleSettings, markAsReadyToFire, showHelpEllipse);
            }
            foreach (AbstractEdge item in this.edges)
            {
                item.draw(g, this.isSelected(item), this.VisualSettings, this.visibleSettings);
            }
        }

        /// <summary>
        /// When open a Network xml, max unid have to be calculated.
        /// </summary>
        private long calculateMaxUnid()
        {
            long maxUnid = -1;
            foreach (AbstractNetworkItem item in this.items)
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
            foreach (AbstractEdge item in this.edges)
            {
                if (maxUnid < item.Unid)
                {
                    maxUnid = item.Unid;
                }
            }
            long stateMaxUnid = this.stateHierarchy.calculateMaxStateUnid();
            if (maxUnid < stateMaxUnid)
            {
                maxUnid = stateMaxUnid;
            }
            return maxUnid;
        }

        /// <summary>
        /// When open a Network xml, next unid have to be calculated.
        /// </summary>
        private void setNextUnid()
        {
            this.unidGenNumber = this.calculateMaxUnid() + 1;
        }

        public void changeTransitionType(Transition transition, TransitionType transitionType)
        {
            if ((transition != null) && (transitionType != null))
            {
                List<AbstractEdge> edgesToDelete = new List<AbstractEdge>();
                if ( TransitionType.SINK.Equals(transitionType))
                {
                    edgesToDelete = this.getAllOutputEdge(transition);
                } else if ( TransitionType.SOURCE.Equals(transitionType)) {
                    edgesToDelete = this.getAllInputEdge(transition);
                }
                if (edgesToDelete.Count > 0)
                {
                    this.deleteEdges(edgesToDelete);
                }
            }
        }

        public int getMinusWeight(Position position, Transition transition)
        {
            int ret = 0;
            foreach (AbstractEdge edge in this.edges)
            {
                if (edge is EdgePositionTransition)
                {
                    if (EdgeType.NORMAL.Equals(edge.EdgeType))
                    {
                        EdgePositionTransition ept = (EdgePositionTransition)edge;
                        if ( ept.StartPosition.Equals(position) && ept.EndTransition.Equals(transition) )
                        {
                            ret = ept.Weight;
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        public int getPlusWeight(Position position, Transition transition)
        {
            int ret = 0;
            foreach (AbstractEdge edge in this.edges)
            {
                if (edge is EdgeTransitionPosition)
                {
                    if (EdgeType.NORMAL.Equals(edge.EdgeType))
                    {
                        EdgeTransitionPosition etp = (EdgeTransitionPosition)edge;
                        if (etp.StartTransition.Equals(transition) && etp.EndPosition.Equals(position))
                        {
                            ret = etp.Weight;
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        public void initStatistics()
        {
            List<Position> positions = this.Positions;
            List<Transition> transitions = this.Transitions;
            List<StateVector> states = this.stateHierarchy.States;

            foreach (Position item in positions)
            {
                item.initStatistics();
            }
            foreach (Transition item in transitions)
            {
                item.Statistics.init();
            }
            foreach (StateVector item in states)
            {
                item.Statistics.init();
            }
        }

        private void addPetriEventTransfer(IPetriEvent ipe, string name, List<PetriEventTransfer> transfer)
        {
            foreach (PetriEvent pe in ipe.PetriEvents.Events)
            {
                if (pe.Name.Equals(name))
                {
                    transfer.Add(new PetriEventTransfer(pe, ipe));
                }
            }
        }

        public List<PetriEventTransfer> getEventsByName(string name)
        {
            List<PetriEventTransfer> transfer = new List<PetriEventTransfer>();
            IPetriEvent ipe = (IPetriEvent)this;
            this.addPetriEventTransfer((IPetriEvent)this, name, transfer);
            
            /*
            foreach (PetriEvent pe in this.PetriEvents.Events)
            {
                if (pe.Name.Equals(name))
                {
                    transfer.Add(new PetriEventTransfer(pe, this));
                }
            }*/
            foreach (AbstractNetworkItem item in this.items)
            {
                this.addPetriEventTransfer((IPetriEvent)item, name, transfer);
            /*
                foreach (PetriEvent pe in item.PetriEvents.Events)
                {
                    if (pe.Name.Equals(name))
                    {
                        transfer.Add(new PetriEventTransfer(pe, item));
                    }
                }*/
            }
            foreach (StateVector item in this.stateHierarchy.States)
            {
                this.addPetriEventTransfer((IPetriEvent)item, name, transfer);
                /*
                foreach (PetriEvent pe in item.PetriEvents.Events)
                {
                    if (pe.Name.Equals(name))
                    {
                        transfer.Add(new PetriEventTransfer(pe, item));
                    }
                }*/
            }
            return transfer;
        }

        #region Manage items

        public AbstractNetworkItem addPosition(string name, PointF origo, float radius)
        {
            Position position = new Position(this.identityProvider.positionIdentity(name), this.unidGenNumber++, true, origo, radius, 0);
            this.addItem(position);
            return position;
        }

        public AbstractNetworkItem addTransition(string name, double angle, PointF origo, SizeF size)
        {
            Transition transition = new Transition(this.identityProvider.transitionIdentity(name), this.unidGenNumber++, true, angle, origo, size, 0, TransitionType.NORMAL, 0, AbstractTransition.DEFAULT_CLOCK_RADIUS, new PointF(10, 10));
            this.addItem(transition);
            return transition;
        }

        public void addNote(string name, PointF origo, SizeF size, AbstractItem attachedItem)
        {
            this.addItem(new Note(this.identityProvider.noteIdentity(name), this.unidGenNumber++, true, origo, size, attachedItem, "new note"));
        }

        protected void addEdgePositionTransition(int weight, Position position, Transition transition, PointF curveMiddlePoint)
        {
            this.addEdge(new EdgePositionTransition("", this.unidGenNumber++, true, weight, position, transition, curveMiddlePoint, EdgeType.NORMAL));
        }

        protected void addEdgeTransitionPosition(int weight, Transition transition, Position position, PointF curveMiddlePoint)
        {
            this.addEdge(new EdgeTransitionPosition("", this.unidGenNumber++, true, weight, transition, position, curveMiddlePoint, EdgeType.NORMAL));
        }

        public void addEdge(AbstractNetworkItem start, AbstractNetworkItem end, PointF curveMiddlePoint)
        {
            this.addEdge(this.defaultEdgeWeight, start, end, curveMiddlePoint);
        }

        public void addEdge(int weight, AbstractNetworkItem start, AbstractNetworkItem end, PointF curveMiddlePoint)
        {
            if ((start != null) && (end != null))
            {
                if ((start is Position) && (end is Transition))
                {
                    Transition endTransition = (Transition)end;
                    if (!TransitionType.SOURCE.Equals(endTransition.TransitionType))
                    {
                        this.addEdgePositionTransition(weight, (Position)start, endTransition, curveMiddlePoint);
                    }
                }
                else if ((start is Transition) && (end is Position))
                {
                    Transition startTransition = (Transition)start;
                    if (!TransitionType.SINK.Equals(startTransition.TransitionType))
                    {
                        this.addEdgeTransitionPosition(weight, startTransition, (Position)end, curveMiddlePoint);
                    }
                }
            }
        }

        #endregion

        #region Manage tokens

        public void addToken(Position position, string name)
        {
            if (position != null)
            {
                position.addToken(this.identityProvider.tokenIdentity(name), this.unidGenNumber++);
                this.deleteAllStates();
            }
        }

        public void deleteToken(Position position)
        {
            if (position != null)
            {
                position.deleteToken();
                this.deleteAllStates();
            }
        }

        #endregion

        #region Fire transitions

        private List<AbstractEdge> getAllInputEdge(Transition transition)
        {
            List<AbstractEdge> inputs = new List<AbstractEdge>();
            foreach (AbstractEdge edge in this.edges)
            {
                if (transition.Equals(edge.End))
                {
                    inputs.Add(edge);
                }
            }
            return inputs;
        }

        private List<AbstractEdge> getAllOutputEdge(Transition transition)
        {
            List<AbstractEdge> outputs = new List<AbstractEdge>();
            foreach (AbstractEdge edge in this.edges)
            {
                if (transition.Equals(edge.Start))
                {
                    outputs.Add(edge);
                }
            }
            return outputs;
        }

        private void cutStateMatrix(StateVector last)
        {
            List<StateVector> newStateMatrix = new List<StateVector>();
            foreach (StateVector stateVector in this.stateMatrix){
                if (stateVector.Equals(last))
                {
                    break;
                }
                newStateMatrix.Add(stateVector);
            }
            newStateMatrix.Add(last);
            this.stateMatrix = newStateMatrix;
        }

        private void swapTokens(List<AbstractToken> tokens, int indexA, int indexB)
        {
            AbstractToken tmpToken = tokens[indexA];
            tokens[indexA] = tokens[indexB];
            tokens[indexB] = tmpToken;
        }

        private void mixTokens(List<AbstractToken> tokens)
        {
            if (tokens.Count > 1)
            {
                for (int i = 0; i < PetriNetwork.SWAP_TOKEN_NUMBER; i++)
                {
                    this.swapTokens(tokens, this.rand.Next(tokens.Count), this.rand.Next(tokens.Count));
                }
            }
        }

        public StateVector simulateTransitionFire(Transition fireTransition, bool changeStatistics)
        {
            TransitionHistoryItem historyItem = null;
            if (changeStatistics)
            {
                fireTransition.Statistics.add(1);
                historyItem = new TransitionHistoryItem(fireTransition);
            }
            List<AbstractToken> tokens = new List<AbstractToken>();
            List<AbstractEdge> inputs = this.getAllInputEdge(fireTransition);
            List<AbstractEdge> outputs = this.getAllOutputEdge(fireTransition);
            foreach (AbstractEdge edge in inputs)
            {
                if (edge.Start is Position)
                {
                    Position position = (Position)edge.Start;
                    if (EdgeType.NORMAL.Equals(edge.EdgeType))
                    {
                        tokens.AddRange(position.takeAwayTokens(edge.Weight, changeStatistics));
                    }
                    else if (EdgeType.RESET.Equals(edge.EdgeType))
                    {
                        tokens.AddRange(position.takeAwayTokens(changeStatistics));
                    }
                    else if (EdgeType.INHIBITOR.Equals(edge.EdgeType))
                    {
                        // nothing to do
                    }
                }
            }
            this.mixTokens(tokens);
            if (changeStatistics && historyItem != null)
            {
                historyItem.addToken(tokens);
            }
            int ti = 0;
            foreach (AbstractEdge edge in outputs)
            {
                if (edge.End is Position)
                {
                    Position position = (Position)edge.End;
                    for (int ei = 0; ei < edge.Weight; ei++)
                    {
                        AbstractToken token = null;
                        if (ti < tokens.Count)
                        {
                            token = tokens[ti++];
                        }
                        else
                        {
                            token = new Token(this.identityProvider.tokenIdentity(null), this.unidGenNumber++, true);
                        }
                        position.addToken(token);
                    }
                    if (changeStatistics)
                    {
                        position.addStatistics();
                    }
                }
            }
            if (changeStatistics && historyItem != null)
            {
                this.addItemToTransitionHistory(historyItem);
            }
            return this.getNewStateVector(null);
        }

        private Transition chooseFireTransition(List<Transition> canFire)
        {
            Transition fireTransition = canFire[0];
            if (FireRule.RANDOM.Equals(this.fireRule))
            {
                fireTransition = canFire[this.rand.Next(canFire.Count)];
            }
            else if (FireRule.ASC_UNID.Equals(this.fireRule))
            {
                long minUnid = canFire[0].Unid;
                foreach (Transition tran in canFire)
                {
                    if (minUnid > tran.Unid)
                    {
                        minUnid = tran.Unid;
                        fireTransition = tran;
                    }
                }
            }
            else if (FireRule.DESC_UNID.Equals(this.fireRule))
            {
                long maxUnid = canFire[0].Unid;
                foreach (Transition tran in canFire)
                {
                    if (maxUnid < tran.Unid)
                    {
                        maxUnid = tran.Unid;
                        fireTransition = tran;
                    }
                }
            }
            else if (FireRule.PRIORITY.Equals(this.fireRule))
            {
                long minPriority = canFire[0].Priority;
                foreach (Transition tran in canFire)
                {
                    if (minPriority > tran.Priority)
                    {
                        minPriority = tran.Priority;
                    }
                }
                List<Transition> canFiremin = new List<Transition>();
                foreach (Transition tran in canFire)
                {
                    if (tran.Priority == minPriority)
                    {
                        canFiremin.Add(tran);
                    }
                }
                fireTransition = canFiremin[this.rand.Next(canFiremin.Count)];
            }
            return fireTransition;
        }

        private bool checkCapacityLimit(Transition transition, Dictionary<Position, Int32> touchedPositions)
        {
            bool cFire = true;
            List<AbstractEdge> outputs = this.getAllOutputEdge(transition);
            if (outputs.Count > 0)
            {
                foreach (AbstractEdge edge in outputs)
                {
                    if (edge.End is Position)
                    {
                        int needCapacity = edge.Weight;
                        Position tmpPos = (Position)edge.End;
                        if (touchedPositions.ContainsKey(tmpPos))
                        {
                            needCapacity -= touchedPositions[tmpPos];
                        }
                        if (tmpPos.isCapcityLimitInjured(needCapacity))
                        {
                            cFire = false;
                        }
                    }
                }
            }
            return cFire;
        }

        private List<Transition> getReadyToFireTransition()
        {
            List<Transition> canFire = new List<Transition>();
            List<Transition> transitions = this.Transitions;
            Dictionary<Position, Int32> touchedPositions = new Dictionary<Position, Int32>();
            bool cFire = false;
            foreach (Transition transition in transitions)
            {
                touchedPositions.Clear();
                if (TransitionType.SOURCE.Equals(transition.TransitionType))
                {
                    // touchedPositions: input positions --> itt ilyen nem lehet!
                    cFire = checkCapacityLimit(transition, touchedPositions);
                    if (cFire)
                    {
                        canFire.Add(transition);
                    }
                }
                else
                {
                    List<AbstractEdge> inputs = this.getAllInputEdge(transition);
                    if (inputs.Count > 0)
                    { 
                        cFire = true;
                        foreach (AbstractEdge edge in inputs)
                        {
                            if (edge.Start is Position)
                            {
                                if (EdgeType.NORMAL.Equals(edge.EdgeType))
                                {
                                    if (!((Position)edge.Start).hasEnoughTokens(edge.Weight))
                                    {
                                        cFire = false;
                                        break;
                                    } else {
                                        touchedPositions.Add((Position)edge.Start, edge.Weight);
                                    }
                                }
                                else if (EdgeType.INHIBITOR.Equals(edge.EdgeType))
                                {
                                    if (((Position)edge.Start).hasEnoughTokens(edge.Weight))
                                    {
                                        cFire = false;
                                        break;
                                    } else {
                                        touchedPositions.Add((Position)edge.Start, edge.Weight);
                                    }
                                }
                                else if (EdgeType.RESET.Equals(edge.EdgeType))
                                {
                                    // always can fire
                                }
                            }
                        }
                        if (cFire)
                        {
                            cFire = checkCapacityLimit(transition, touchedPositions);
                            /*
                            // check CapacityLimit
                            List<AbstractEdge> outputs = this.getAllOutputEdge(transition);
                            if (outputs.Count > 0)
                            {
                                foreach (AbstractEdge edge in outputs)
                                {
                                    if (edge.End is Position)
                                    {
                                        int needCapacity = edge.Weight;
                                        Position tmpPos = (Position)edge.End;
                                        if (touchedPositions.ContainsKey(tmpPos))
                                        {
                                            needCapacity -= touchedPositions[tmpPos];
                                        }
                                        if (tmpPos.isCapcityLimitInjured(needCapacity))
                                        {
                                            cFire = false;
                                        }
                                    }
                                }
                            }
                            */
                            if (cFire)
                            {
                                if (TransitionType.SINK.Equals(transition.TransitionType))
                                {
                                    canFire.Add(transition);
                                }
                                else
                                {
                                    List<AbstractEdge> outputs2 = this.getAllOutputEdge(transition);
                                    if (outputs2.Count > 0)
                                    {
                                        canFire.Add(transition);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return canFire;
        }

        public FireReturn fire(bool isAutoFire)
        {
            FireEvent fireEvent = FireEvent.NORMALFIRE;
            Transition fireTransition = null;
            if (this.stateMatrix.Count == 0)
            { 
                this.sendEvent(isAutoFire,"StateMatrix is null. Save the actual state..");
                this.clearStateMatrix(isAutoFire);
                fireEvent = FireEvent.INITFIRE;
            }
            else if (!(this.stateMatrix[this.stateMatrix.Count - 1].Equals(this.actualStateVector)))
            {
                this.cutStateMatrix(this.actualStateVector);
                this.sendEvent(isAutoFire,"Actual StateVector is not the last one in the StateMatrix. Cut the StateMatrix (now actual state vector is the last one). (Actual: " + this.actualStateVector + ")");
                fireEvent = FireEvent.RESETFIRE;
            }
            List<Transition> canFire = this.getReadyToFireTransition();
            if (canFire.Count > 0)
            {
                /*
                // add all available edges between states
                // bug: returnToState cannot recreate the previous network
                if (this.actualStateVector != null)
                {
                    foreach (Transition tran in canFire)
                    {
                        StateVector stateVector = this.simulateTransitionFire(tran, false);
                        StateVector findStateVector = this.addOrFetchStateToOrFromHierarchy(isAutoFire, stateVector);
                        this.stateHierarchy.addEdge(this.actualStateVector, findStateVector);
                        this.returnToState(this.actualStateVector);
                    }
                }
                */

                fireTransition = this.chooseFireTransition(canFire);
                if (fireTransition != null)
                {
                    this.sendEvent(isAutoFire,"Fire transition: " + fireTransition);
                    
                    StateVector stateVector = this.simulateTransitionFire(fireTransition, true);
                    this.saveState(isAutoFire, null, stateVector);
                }
            }
            else
            {
                this.sendEvent(isAutoFire,"Fireable transition does not exists.");
                fireEvent = FireEvent.DEADLOCK;
            }
            return new FireReturn(fireEvent, fireTransition);
        }

        public void returnToState(StateVector stateVector)
        {
            List<Position> positions = this.Positions;
            foreach ( Position position in positions ){
                List<AbstractToken> tokens = stateVector.getTokensByPositionUnid(position.Unid);
                position.changeTokens(tokens);
            }
            this.actualStateVector = stateVector;
        }

        #endregion

        public override string ToString()
        {
            return "Net " + this.name;
        }

    }
}
