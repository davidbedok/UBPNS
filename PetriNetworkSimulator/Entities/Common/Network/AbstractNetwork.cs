using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Item.Position;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Entities.Common.Item.Note;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Edge;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.State.Hierarchy;
using PetriNetworkSimulator.Entities.State.Vector;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.History;

namespace PetriNetworkSimulator.Entities.Common.Network
{

    public delegate void PetriNetworkEventHandler(string message);
    public delegate void PetriNetworkNotifier(NetworkNotifierAction action);

    public abstract partial class AbstractNetwork : IPetriEvent
    {

        protected Random rand;

        protected long unidGenNumber;

        protected int stateGenNumber;
        protected string statePrefix;

        protected NetworkVisualSettings visualSettings;
        protected NetworkVisibleSettings visibleSettings;

        protected List<AbstractNetworkItem> items;
        protected List<AbstractEdge> edges;
   
        private List<AbstractItem> selectedItems;     
        protected AbstractNetworkItem startNetworkItemForEdgeSelection;
        protected List<AbstractNetworkItem> connectedItems;

        protected StateVector actualStateVector;
        protected List<StateVector> stateMatrix;
        protected StateHierarchy stateHierarchy;

        private EventTrunk events;
        private List<TransitionHistoryItem> transitionHistory;

        public event PetriNetworkEventHandler eventHandler;
        public event PetriNetworkNotifier eventNotifier;

        #region Readonly properties 

        public long UnidGenNumber
        {
            get { return this.unidGenNumber; }
        }

        public int StateGenNumber
        {
            get { return this.stateGenNumber; }
        }

        public NetworkVisualSettings VisualSettings
        {
            get { return this.visualSettings; }
        }

        public NetworkVisibleSettings VisibleSettings
        {
            get { return this.visibleSettings; }
        }

        public List<AbstractNetworkItem> Items
        {
            get { return this.items; }
        }

        public List<AbstractItem> SelectedItems
        {
            get { return this.selectedItems; }
        }

        public List<StateVector> StateMatrix
        {
            get { return this.stateMatrix; }
        }

        public StateVector ActualStateVector
        {
            get { return this.actualStateVector; }
        }

        public EventTrunk PetriEvents
        {
            get { return this.events; }
        }

        public List<TransitionHistoryItem> TransitionHistory
        {
            get { return this.transitionHistory; }
        }

        #endregion

        #region Properties

        public StateHierarchy StateHierarchy
        {
            get { return this.stateHierarchy; }
            set { this.stateHierarchy = value; }
        }

        public string StatePrefix
        {
            get { return this.statePrefix; }
            set { this.statePrefix = value; }
        }

        #endregion

        public AbstractNetwork(Random rand, int stateGenNumber, string statePrefix, int sh_width, int sh_height)
        {
            this.rand = rand;
            this.items = new List<AbstractNetworkItem>();
            this.selectedItems = new List<AbstractItem>();
            this.edges = new List<AbstractEdge>();
            this.visualSettings = new NetworkVisualSettings();
            this.visibleSettings = new NetworkVisibleSettings();
            this.stateMatrix = new List<StateVector>();
            this.stateHierarchy = new StateHierarchy(this.rand, sh_width, sh_height);

            this.unidGenNumber = 0;
            this.stateGenNumber = stateGenNumber;
            this.statePrefix = statePrefix;
            this.events = new EventTrunk();
            this.transitionHistory = new List<TransitionHistoryItem>();
        }

        /// <summary>
        /// To detect which is the largest group to groupedit.
        /// </summary>
        public NetworkPropertyGroup getSelectedPropertyGroup()
        {
            NetworkPropertyGroup ret = NetworkPropertyGroup.NONE;
            if ((this.selectedItems != null) && (this.selectedItems.Count > 0))
            {
                int countPosition = 0;
                int countTransition = 0;
                int countEdge = 0;
                int countToken = 0;
                foreach (AbstractItem item in this.selectedItems)
                {
                    if (item is Position) { countPosition++; }
                    if (item is Transition) { countTransition++; }
                    if (item is AbstractEdge) { countEdge++; }
                    if (item is AbstractToken) { countToken++; }
                }
                if (this.selectedItems.Count == countPosition)
                {
                    ret = NetworkPropertyGroup.POSITION;
                }
                else if (this.selectedItems.Count == countTransition)
                {
                    ret = NetworkPropertyGroup.TRANSITION;
                }
                else if (this.selectedItems.Count == (countPosition + countTransition))
                {
                    ret = NetworkPropertyGroup.ABSTRACTNETWORKITEM;
                }
                else if (this.selectedItems.Count == countEdge)
                {
                    ret = NetworkPropertyGroup.EDGE;
                }
                else if (this.selectedItems.Count == countToken)
                {
                    ret = NetworkPropertyGroup.TOKEN;
                }
                else
                {
                    ret = NetworkPropertyGroup.ABSTRACTNETWORKITEM; // only move
                }
            }
            return ret;
        }

        /// <summary>
        /// Delete all selected item (and attached things).
        /// </summary>
        public void deleteSelectedItems()
        {
            for (int i = this.selectedItems.Count - 1; i >= 0; i--)
            {
                AbstractItem item = this.selectedItems[i];
                if (item is AbstractNetworkItem)
                {
                    this.reverseSelection(item);
                    this.deleteItemAndConnectedEdges((AbstractNetworkItem)item);
                }
            }
        }

        /// <summary>
        /// Clear the entire Network (states, selection, edges and items (position, transition and notes)).
        /// </summary>
        public void clearPetriNetwork()
        {
            this.deleteAllStates();
            this.selectedItems.Clear();
            this.edges.Clear();
            this.items.Clear();
        }

        /// <summary>
        /// Change the Origo of the selected items. Use this method via move tool.
        /// </summary>
        public void modifySelectedItems(NetworkProperty property, PointF origoOffset)
        {
            foreach (AbstractItem item in this.selectedItems)
            {
                if (item is AbstractNetworkItem)
                {
                    AbstractNetworkItem networkItem = (AbstractNetworkItem)item;
                    switch (property)
                    {
                        case NetworkProperty.ORIGO:
                            networkItem.Origo = new PointF(networkItem.Origo.X + origoOffset.X, networkItem.Origo.Y + origoOffset.Y);
                            break;
                    }
                }
            }
        }

        private AbstractNetworkItem getFirstSelectedNetworkItem()
        {
            AbstractNetworkItem ret = null;
            foreach (AbstractItem item in this.selectedItems)
            {
                if (item is AbstractNetworkItem)
                {
                    ret = (AbstractNetworkItem)item;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Change any properties of the selected items. Use this method via Property panel.
        /// </summary>
        public void modifySelectedItems(NetworkProperty property, float offset)
        {
            List<AbstractItem> changedItems = new List<AbstractItem>();
            AbstractNetworkItem firstSelectedItem = getFirstSelectedNetworkItem();
            foreach (AbstractItem item in this.selectedItems)
            {
                if (item is AbstractNetworkItem)
                {
                    AbstractNetworkItem networkItem = (AbstractNetworkItem)item;
                    switch (property)
                    {
                        case NetworkProperty.ORIGO_X:
                            networkItem.Origo = new PointF(networkItem.Origo.X + offset, networkItem.Origo.Y);
                            break;
                        case NetworkProperty.ORIGO_Y:
                            networkItem.Origo = new PointF(networkItem.Origo.X, networkItem.Origo.Y + offset);
                            break;
                        case NetworkProperty.RADIUS:
                            networkItem.Radius = networkItem.Radius + offset;
                            break;
                        case NetworkProperty.SIZE_WIDTH:
                            networkItem.Size = new SizeF(networkItem.Size.Width + offset, networkItem.Size.Height);
                            break;
                        case NetworkProperty.SIZE_HEIGHT:
                            networkItem.Size = new SizeF(networkItem.Size.Width, networkItem.Size.Height + offset);
                            break;
                        case NetworkProperty.LABELOFFSET_X:
                            networkItem.LabelOffset = new PointF(networkItem.LabelOffset.X + offset, networkItem.LabelOffset.Y);
                            break;
                        case NetworkProperty.LABELOFFSET_Y:
                            networkItem.LabelOffset = new PointF(networkItem.LabelOffset.X, networkItem.LabelOffset.Y + offset);
                            break;
                        case NetworkProperty.SAMEORIGOX:
                            if (firstSelectedItem != null)
                            {
                                networkItem.Origo = new PointF(firstSelectedItem.Origo.X, networkItem.Origo.Y);
                            }
                            break;
                        case NetworkProperty.SAMEORIGOY:
                            if (firstSelectedItem != null)
                            {
                                networkItem.Origo = new PointF(networkItem.Origo.X, firstSelectedItem.Origo.Y);
                            }
                            break;
                    }
                    if (item is Transition)
                    {
                        Transition transitionItem = (Transition)networkItem;
                        switch (property)
                        {
                            case NetworkProperty.ANGLE:
                                transitionItem.Angle += offset;
                                break;
                            case NetworkProperty.PRIORITY:
                                transitionItem.Priority += (int)offset;
                                break;
                            case NetworkProperty.DELAY:
                                transitionItem.Delay += (int)offset;
                                break;
                            case NetworkProperty.CLOCKRADIUS:
                                transitionItem.ClockRadius = transitionItem.ClockRadius + offset;
                                break;
                            case NetworkProperty.CLOCKOFFSET_X:
                                transitionItem.ClockOffset = new PointF(transitionItem.ClockOffset.X + offset, transitionItem.ClockOffset.Y);
                                break;
                            case NetworkProperty.CLOCKOFFSET_Y:
                                transitionItem.ClockOffset = new PointF(transitionItem.ClockOffset.X, transitionItem.ClockOffset.Y + offset);
                                break;
                        }
                    }
                    if (item is Position)
                    {
                        Position positionItem = (Position)networkItem;
                        switch (property)
                        {
                            case NetworkProperty.CAPACITYLIMIT:
                                positionItem.CapacityLimit += (int)offset;
                                break;
                        }
                    }
                }
                else if (item is AbstractEdge)
                {
                    AbstractEdge edge = (AbstractEdge)item;
                    switch (property)
                    {
                        case NetworkProperty.WEIGHT:
                            edge.Weight += (int)offset;
                            break;
                        case NetworkProperty.CHANGEDIRECTION:
                            changedItems.Add(edge);
                            break;
                    }
                }
            }
            if ((changedItems != null) && (changedItems.Count > 0))
            {
                for (int i = 0; i < changedItems.Count; i++)
                {
                    if (changedItems[i] is AbstractEdge)
                    {
                        this.changeEdgeDirection((AbstractEdge)changedItems[i], true);
                    }
                }
            }
        }

        /// <summary>
        /// Get an item via mouse coordinates (transform to picture coordinate is necessary).
        /// </summary>
        public AbstractItem getVisualItemByCoordinates(PointF point)
        {
            AbstractItem item = null;
            IEnumerator<AbstractNetworkItem> iterItem = this.items.GetEnumerator();
            bool find = false;
            while (iterItem.MoveNext() && !find)
            {
                AbstractNetworkItem actItem = iterItem.Current;
                if (actItem.isNearby(point))
                {
                    item = actItem;
                    find = true;
                }   
            }
            if (item == null)
            {
                IEnumerator<AbstractEdge> iterEdge = this.edges.GetEnumerator();
                // 1st: find CurveMiddlePoint
                find = false;
                while (iterEdge.MoveNext() && !find)
                {
                    AbstractEdge actItem = iterEdge.Current;
                    if (!actItem.isZeroCurveMiddlePointOffset())
                    {
                        if (actItem.getRelativeDistanceFromMiddle(point) < AbstractEdge.DISTANCE_LIMIT_FROM_HELPCIRCLE)
                        {
                            item = actItem;
                            find = true;
                        }
                    }
                }
                if (!find)
                {
                    // 2nd: find straight line point
                    iterEdge.Reset();
                    find = false;
                    while (iterEdge.MoveNext() && !find)
                    {
                        AbstractEdge actItem = iterEdge.Current;
                        if (actItem.isZeroCurveMiddlePointOffset())
                        {
                            if (actItem.getRelativeDistance(point) < AbstractEdge.DISTANCE_LIMIT)
                            {
                                item = actItem;
                                find = true;
                            }
                        }
                    }
                    if (item != null)
                    {
                        // 3nd: find opposite edge
                        AbstractEdge oppositeEdge = this.isAlreadyOppositeEdge((AbstractEdge)item);
                        if (oppositeEdge != null)
                        {
                            if (oppositeEdge.getRelativeDistanceFromMiddle(point) < ((AbstractEdge)item).getRelativeDistanceFromMiddle(point))
                            {
                                item = oppositeEdge;
                            }
                        }
                    }
                }
            }
            return item;
        }

        /// <summary>
        /// Get an item via mouse coordinates (transform to picture coordinate is necessary).
        /// </summary>
        public SearchItemResultTransfer getVisualItemEdgesByCoordinates(PointF point)
        {
            SearchItemResultTransfer ret = null;
            AbstractItem item = null;
            MoveCorner itemMoveEdge = MoveCorner.TOPLEFT;
            IEnumerator<AbstractNetworkItem> iterItem = this.items.GetEnumerator();
            MoveCorner[] moveEdges = MoveCorner.Values;
            //MoveEdge[] moveEdges = (MoveEdge[])Enum.GetValues(typeof(MoveEdge));
            bool find = false;
            while (iterItem.MoveNext() && !find)
            {
                AbstractNetworkItem actItem = iterItem.Current;
                int i = 0;
                while (i < moveEdges.Length && !find) 
                {
                    if (!MoveCorner.NONE.Equals(moveEdges[i]))
                    {
                        if (actItem.isNearby(moveEdges[i], point))
                        {
                            item = actItem;
                            itemMoveEdge = moveEdges[i];
                            find = true;
                        }
                    }
                    i++;
                }   
            }
            if (find)
            {
                ret = new SearchItemResultTransfer(item, itemMoveEdge);
            }
            return ret;
        }

        public void changeEdgeDirection(AbstractEdge item, bool selected)
        {
            if (this.edges.Contains(item))
            {
                AbstractEdge edge = null;
                if (item is EdgePositionTransition)
                {
                    Transition endTransition = (Transition)item.End;
                    if (!TransitionType.SINK.Equals(endTransition.TransitionType))
                    {
                        edge = new EdgeTransitionPosition(item.Name, item.Unid, item.ShowAnnotation, item.Weight, endTransition, (Position)item.Start, item.CurveMiddlePointOffset, EdgeType.NORMAL);
                    }
                }
                else if (item is EdgeTransitionPosition)
                {
                    Transition startTransition = (Transition)item.Start;
                    if (!TransitionType.SOURCE.Equals(startTransition.TransitionType))
                    {
                        edge = new EdgePositionTransition(item.Name, item.Unid, item.ShowAnnotation, item.Weight, (Position)item.End, startTransition, item.CurveMiddlePointOffset, item.EdgeType);
                    }
                }
                if (edge != null)
                {
                    this.deleteEdge(item);
                    this.addEdge(edge);
                    if (selected)
                    {
                        this.selectItem(edge);
                    }
                }
            }
        }

        protected void sendEvent(bool isAutoFire, string message)
        {
            if ((!isAutoFire) && (this.eventHandler != null))
            {
                this.eventHandler(message);
            }
        }

        private List<AbstractNetworkItem> getVisualItemByCoordinates(RectangleF rect)
        {
            List<AbstractNetworkItem> selectItems = new List<AbstractNetworkItem>();
            foreach (AbstractNetworkItem item in this.items)
            {
                if (item.isInsideby(rect))
                {
                    selectItems.Add(item);
                }
            }
            return selectItems;
        }

        private AbstractEdge isAlreadyOppositeEdge(AbstractEdge item)
        {
            AbstractEdge ret = null;
            foreach (AbstractEdge edge in this.edges)
            {
                if (edge.Start.Equals(item.End))
                {
                    if (edge.End.Equals(item.Start))
                    {
                        ret = edge;
                        break;
                    }
                }
            }
            return ret;
        }

        private void removeNoteAttachmentBeforeDelete(AbstractItem item)
        {
            AbstractNote note = this.hasNotes(item);
            if (note != null)
            {
                note.AttachedItem = null;
            }
        }

        #region Manage items (positions, transitions and notes)

        private AbstractNote hasNotes(AbstractItem aitem)
        {
            AbstractNote ret = null;
            foreach (AbstractNetworkItem item in this.items)
            {
                if (item is AbstractNote)
                {
                    AbstractNote note = (AbstractNote)item;
                    if (aitem.Equals(note.AttachedItem))
                    {
                        ret = note;
                        break;
                    }
                }
            }
            return ret;
        }

        protected void addItem(AbstractNetworkItem item)
        {
            if (!this.items.Contains(item))
            {
                this.items.Add(item);
                if (!(item is AbstractNote))
                {
                    this.deleteAllStates();
                }
            }
        }

        /// <summary>
        /// Delete item (position, transition or note) and any connected notes.
        /// </summary>
        private void deleteItem(AbstractNetworkItem item)
        {
            if (this.items.Contains(item))
            {
                this.removeNoteAttachmentBeforeDelete(item);
                this.unselectItem(item);
                this.items.Remove(item);
                if (!(item is AbstractNote))
                {
                    this.deleteAllStates();
                }
            }
        }

        /// <summary>
        /// Delete item (position, transition or note) and any connected edges.
        /// </summary>
        public void deleteItemAndConnectedEdges(AbstractItem item)
        {
            if (item != null)
            {
                if (item is AbstractNetworkItem)
                {
                    AbstractNetworkItem networkItem = (AbstractNetworkItem)item;
                    List<AbstractEdge> markToDelete = new List<AbstractEdge>();
                    foreach (AbstractEdge edge in this.edges)
                    {
                        if (networkItem.Equals(edge.Start) || networkItem.Equals(edge.End))
                        {
                            markToDelete.Add(edge);
                        }
                    }
                    foreach (AbstractEdge edge in markToDelete)
                    {
                        this.deleteEdge(edge);
                    }
                    this.deleteItem(networkItem);
                }
                else if (item is AbstractEdge)
                {
                    AbstractEdge edge = (AbstractEdge)item;
                    this.deleteEdge(edge);
                }
            }
        }

        #endregion

        #region Manage edges 

        private AbstractEdge isAlreadyEdge(AbstractEdge item)
        {
            AbstractEdge ret = null;
            foreach (AbstractEdge edge in this.edges)
            {
                if (edge.Start.Equals(item.Start))
                {
                    if (edge.End.Equals(item.End))
                    {
                        ret = edge;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Add edge. If edge is already exists, only increment weight. If opposite edge is exists, curve middle point offset is setted.
        /// </summary>
        protected void addEdge(AbstractEdge item)
        {
            if (!this.edges.Contains(item))
            {
                AbstractEdge edge = this.isAlreadyEdge(item);
                if (edge == null)
                {
                    AbstractEdge oppositeEdge = this.isAlreadyOppositeEdge(item);
                    if (oppositeEdge == null)
                    {
                        this.edges.Add(item);
                    }
                    else
                    {
                        item.CurveMiddlePointOffset = new PointF(50, 50);
                        this.edges.Add(item);
                    }
                }
                else
                {
                    edge.incrementWeight();
                }
            }
        }

        protected void deleteEdge(AbstractEdge item)
        {
            if (this.edges.Contains(item))
            {
                this.removeNoteAttachmentBeforeDelete(item);
                this.unselectItem(item);
                this.edges.Remove(item);
                this.deleteAllStates();
            }
        }

        protected void deleteEdges(List<AbstractEdge> items)
        {
            if (items != null)
            {
                foreach (AbstractEdge item in items)
                {
                    this.deleteEdge(item);
                }
            }
        }

        #endregion

        #region Manage selection (positions, transitions, edges and notes)

        private List<AbstractNetworkItem> getConnectedItems(AbstractNetworkItem item)
        {
            List<AbstractNetworkItem> ret = new List<AbstractNetworkItem>();
            if (item != null)
            {
                foreach (AbstractEdge edge in this.edges)
                {
                    if (item.Equals(edge.Start))
                    {
                        ret.Add(edge.End);
                    }
                }
            }
            return ret;
        }

        private void selectItem(AbstractItem item)
        {
            if (!this.selectedItems.Contains(item))
            {
                this.selectedItems.Add(item);
            }
        }

        public bool selectItem(PointF point)
        {
            bool ret = false;
            AbstractItem item = this.getVisualItemByCoordinates(point);
            if (item != null)
            {
                this.reverseSelection(item);
                ret = true;
            }
            return ret;
        }

        public bool selectItem(RectangleF rect)
        {
            List<AbstractNetworkItem> selectItems = this.getVisualItemByCoordinates(rect);
            foreach (AbstractNetworkItem item in selectItems)
            {
                this.selectItem(item);
            }
            return ((selectItems != null) && (selectedItems.Count > 0));
        }

        private void unselectItem(AbstractItem item)
        {
            if (this.selectedItems.Contains(item))
            {
                this.selectedItems.Remove(item);
            }
        }

        /// <summary>
        /// To select an edge via special tool, you have to select the start point first.
        /// </summary>
        public void setStartNetworkItemForEdgeSelection(AbstractNetworkItem item)
        {
            this.startNetworkItemForEdgeSelection = item;
            this.connectedItems = this.getConnectedItems(item);
        }

        /// <summary>
        /// Return true, if network item is connected to the start point (for edge selection).
        /// </summary>
        public bool isConnectedItem(AbstractNetworkItem item)
        {
            bool ret = false;
            if ((this.connectedItems != null) && (this.connectedItems.Count > 0))
            {
                ret = this.connectedItems.Contains(item);
            }
            return ret;
        }

        /// <summary>
        /// Select an edge between two NetworkItems (position and transitions). You have to set startNetworkItemForEdgeSelection before use this method.
        /// </summary>
        public void selectEdge(AbstractNetworkItem item)
        {
            AbstractEdge ret = null;
            if ((item != null) && (this.startNetworkItemForEdgeSelection != null) && (!item.Equals(this.startNetworkItemForEdgeSelection)))
            {
                foreach (AbstractEdge edge in this.edges)
                {
                    if ((item.Equals(edge.End)) && (this.startNetworkItemForEdgeSelection.Equals(edge.Start)))
                    {
                        ret = edge;
                        break;
                    }
                }
            }
            this.reverseSelection(ret);
            this.startNetworkItemForEdgeSelection = null;
            this.connectedItems = null;
        }

        /// <summary>
        /// Return true, if item (edge, position, transition or note) is selected.
        /// </summary>
        public bool isSelected(AbstractItem item)
        {
            return this.selectedItems.Contains(item);
        }

        public void reverseSelection(AbstractItem item)
        {
            if (this.isSelected(item))
            {
                this.unselectItem(item);
            }
            else
            {
                this.selectItem(item);
            }
        }

        public void selectAllItem()
        {
            this.unselectAllItem();
            foreach (AbstractNetworkItem item in this.items)
            {
                this.reverseSelection(item);
            }
        }

        public void unselectAllItem()
        {
            this.selectedItems.Clear();
        }

        public void reverseSelection()
        {
            foreach (AbstractNetworkItem item in this.items)
            {
                this.reverseSelection(item);
            }
        }

        #endregion

        #region Manage states

        protected StateVector addOrFetchStateToOrFromHierarchy(bool isAutoFire, StateVector stateVector)
        {
            StateVector findStateVector = this.stateHierarchy.find(stateVector);
            if (findStateVector == null)
            {
                stateVector.Origo = this.stateHierarchy.getAvailableOrigo();
                this.stateHierarchy.addState(stateVector);
                this.sendEvent(isAutoFire, "Add state to StateHierarchy.");
                findStateVector = stateVector;
            }
            return findStateVector;
        }

        protected void saveState(bool isAutoFire, string name, StateVector stateVector)
        {
            StateVector findStateVector = null;
            foreach (StateVector sv in this.stateMatrix)
            {
                if (sv.Equals(stateVector))
                {
                    findStateVector = sv;
                    break;
                }
            }
            if (findStateVector == null)
            {
                findStateVector = this.addOrFetchStateToOrFromHierarchy(isAutoFire, stateVector);
                if (this.actualStateVector != null)
                {
                    this.stateHierarchy.addEdge(this.actualStateVector, findStateVector);
                }
                this.stateMatrix.Add(findStateVector);
                this.actualStateVector = findStateVector;
                findStateVector.Statistics.add(1);
                this.sendEvent(isAutoFire,"Create a new StateVector, and add to the end of the StateMatrix. (Actual: " + this.actualStateVector + ")");
            }
            else
            {
                if (this.actualStateVector != null)
                {
                    this.stateHierarchy.addEdge(this.actualStateVector, findStateVector);
                }
                this.actualStateVector = findStateVector;
                findStateVector.Statistics.add(1);
                this.sendEvent(isAutoFire,"State is already exists, so do not create a new StateVector. Actual StateVector changed. (Actual: " + this.actualStateVector + ")");
            }
        }

        /// <summary>
        /// Clear StateMatrix.
        /// </summary>
        public void clearStateMatrix(bool isAutoFire)
        {
            this.actualStateVector = null;
            this.stateMatrix.Clear();
            this.saveState(isAutoFire, null, this.getNewStateVector(null));
        }

        /// <summary>
        /// Remove all states from Hierarchy and StateMatrix.
        /// </summary>
        public void deleteAllStates()
        {
            if (this.eventNotifier != null)
            {
                this.eventNotifier(NetworkNotifierAction.SIMULATIONMUSTSTOPED);
            }
            this.stateHierarchy.clear();
            this.clearStateMatrix(false);
        }

        protected StateVector getNewStateVector(string name)
        {
            if ((name == null) || ("".Equals(name)))
            {
                name = this.statePrefix + (++this.stateGenNumber).ToString();
            }
            return new StateVector(name, this.unidGenNumber++, this, new PointF(this.rand.Next(this.stateHierarchy.Width), this.rand.Next(this.stateHierarchy.Height)), StateVector.DEFAULT_RADIUS);
        }

        #endregion

        public void clearTransitionHistory()
        {
            this.transitionHistory.Clear();
        }

        protected void addItemToTransitionHistory(TransitionHistoryItem item)
        {
            this.transitionHistory.Add(item);
        }

    }
}
