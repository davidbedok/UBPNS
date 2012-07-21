using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkSimulator.Entities.State.Edge;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.State.Vector;

namespace PetriNetworkSimulator.Entities.State.Hierarchy
{
    public partial class StateHierarchy
    {
        public const int MARGIN = 15;

        public const int DEF_SHT_WIDTH = 320;
        public const int DEF_SHT_HEIGHT = 240;

        private Random rand;
        public int width = 320;
        public int height = 240;

        private List<StateVector> states;
        private List<EdgeStateState> edges;

        public List<StateVector> States
        {
            get { return this.states; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public StateHierarchy(Random rand, int width, int height)
        {
            this.rand = rand;
            this.states = new List<StateVector>();
            this.edges = new List<EdgeStateState>();
            this.width = width;
            this.height = height;
        }

        public void addState(StateVector state)
        {
            if (!(this.states.Contains(state)))
            {
                this.states.Add(state);
            }
        }

        public void addStates(List<StateVector> states)
        {
            foreach (StateVector state in states)
            {
                this.addState(state);
            }
        }

        private void addEdge(EdgeStateState edge)
        {
            if (!(this.edges.Contains(edge)))
            {
                this.edges.Add(edge);
            }
        }

        public void removeState(StateVector state)
        {
            if ( (state != null) && ( this.states.Contains(state) ) )
            {
                List<EdgeStateState> removeableEdges = new List<EdgeStateState>();
                foreach (EdgeStateState edge in this.edges)
                {
                    if (edge.Start.Equals(state) || edge.End.Equals(state))
                    {
                        removeableEdges.Add(edge);
                    }
                }
                foreach (EdgeStateState edge in removeableEdges)
                {
                    this.edges.Remove(edge);
                }
                this.states.Remove(state);
            }
        }

        public void addEdges(List<EdgeStateState> edges)
        {
            foreach (EdgeStateState edge in edges)
            {
                this.addEdge(edge);
            }
        }

        public void addEdge(StateVector start, StateVector end)
        {
            this.addEdge(new EdgeStateState(start, end));
        }

        public StateVector find(StateVector stateVector)
        {
            StateVector ret = null;
            foreach (StateVector sv in this.states)
            {
                if (sv.Equals(stateVector))
                {
                    ret = sv;
                    break;
                }
            }
            return ret;
        }

        public StateVector getVisualItemByCoordinates(PointF point)
        {
            StateVector item = null;
            IEnumerator<StateVector> iterItem = this.states.GetEnumerator();
            bool find = false;
            while (iterItem.MoveNext() && !find)
            {
                StateVector actItem = iterItem.Current;
                if (actItem.isNearby(point))
                {
                    item = actItem;
                    find = true;
                }
            }
            return item;
        }

        public void draw(Graphics g, NetworkVisualSettings visualSettings, StateVector actualState)
        {
            foreach (StateVector state in this.states)
            {
                state.draw(g, visualSettings, actualState);
            }
            foreach (EdgeStateState edge in this.edges)
            {
                edge.draw(g, visualSettings);
            }
        }

        public PointF getAvailableOrigo()
        {
            PointF ret = new PointF(0, 0);
            int i = 0;
            bool find = false;
            do
            {
                ret = new PointF(StateHierarchy.MARGIN + this.rand.Next(this.width - StateHierarchy.MARGIN * 2), StateHierarchy.MARGIN + this.rand.Next(this.height - StateHierarchy.MARGIN * 2));
                find = true;
                foreach (StateVector state in this.states)
                {
                    if (state.isNearbyForNew(ret))
                    {
                        find = false;
                        break;
                    }
                }
                i++;
            } while ( (!find) && ( i < 10 ) );
            return ret;
        }

        public void clear()
        {
            if ((edges != null) && (edges.Count > 0))
            {
                this.edges.Clear();
            }
            if ((states != null) && (states.Count > 0))
            {
                this.states.Clear();
            }
        }

        public long calculateMaxStateUnid()
        {
            long maxUnid = -1;
            foreach (StateVector item in this.states)
            {
                if (maxUnid < item.Unid)
                {
                    maxUnid = item.Unid;
                }
            }
            return maxUnid;
        }

    }
}
