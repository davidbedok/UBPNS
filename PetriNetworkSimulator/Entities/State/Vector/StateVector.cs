using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkSimulator.Entities.State.Vector
{
    public partial class StateVector : IPetriItem, IPetriEvent
    {

        public const float DEFAULT_RADIUS = 20;
        public const float NEAR_OFFSET = 10;
        private const float MAX_RADIUS = 50;
        public const float RADIUS_OFFSET = 2;

        private string name;
        private long unid;
        private PointF origo;
        private float radius;
        private RectangleF map;
        private GeneralStatistics statistics;

        private EventTrunk events;

        public EventTrunk PetriEvents
        {
            get { return this.events; }
        }

        public PointF Origo
        {
            get { return this.origo; }
            set { this.origo = value; }
        }

        public float Radius
        {
            get { return this.radius; }
            set {
                if ( ( value > 0 ) && ( value < StateVector.MAX_RADIUS )) {
                    this.radius = value;
                }
            }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public long Unid
        {
            get { return this.unid; }
        }

        public RectangleF Map
        {
            get { return this.map; }
            set { this.map = value; }
        }

        public RectangleF Rectangle
        {
            get {
                PointF point = new PointF(this.origo.X - this.radius, this.origo.Y - this.radius);
                return new RectangleF(point, new SizeF(this.radius * 2, this.radius * 2)); 
            }
        }

        protected PointF NearByDistance
        {
            get
            {
                return new PointF(this.radius, this.radius);
            }
        }

        protected PointF NearByDistanceForNew
        {
            get
            {
                return new PointF(this.radius + StateVector.NEAR_OFFSET, this.radius + StateVector.NEAR_OFFSET);
            }
        }

        public GeneralStatistics Statistics
        {
            get { return this.statistics; }
        }

        private Dictionary<Int64, List<AbstractToken>> tokenDistribution;
        
        public StateVector(string name, long unid, Dictionary<Int64, List<AbstractToken>> tokenDistribution, PointF origo, float radius)
        {
            this.tokenDistribution = tokenDistribution;
            this.init(name, unid, origo, radius);
        }

        public StateVector(string name, long unid, AbstractNetwork network, PointF origo, float radius)
        {
            this.tokenDistribution = new Dictionary<Int64, List<AbstractToken>>();
            if (network is PetriNetwork)
            {
                List<Position> positions = (network as PetriNetwork).Positions;
                foreach (Position position in positions)
                {
                    List<AbstractToken> copiedTokens = new List<AbstractToken>();
                    copiedTokens.AddRange(position.Tokens);
                    this.tokenDistribution.Add(position.Unid, copiedTokens);
                }
            }
            this.init(name, unid, origo, radius);
        }

        private void init(string name, long unid, PointF origo, float radius)
        {
            this.name = name;
            this.unid = unid;
            this.origo = origo;
            this.radius = radius;
            this.statistics = new GeneralStatistics();
            this.events = new EventTrunk();
        }

        public bool isInside(PointF point)
        {
            bool ret = false;
            if ((point.X > this.map.Left) && (point.X < this.map.Right))
            {
                if ((point.Y > this.map.Top) && (point.Y < this.map.Bottom))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public List<AbstractToken> getTokensByPositionUnid(long positionUnid)
        {
            return this.tokenDistribution[positionUnid];
        }

        public int getTokenCountByPositionUnid(long positionUnid)
        {
            return this.getTokensByPositionUnid(positionUnid).Count;
            // return this.tokenCount[positionUnid];
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is StateVector))
            {
                return false;
            }
            return this.Equals((StateVector)obj);
        }

        public bool Equals(StateVector sv)
        {
            bool ret = false;
            if (sv != null)
            {
                ret = true;
                foreach (KeyValuePair<Int64, List<AbstractToken>> entry in sv.tokenDistribution)
                {
                    Int64 positionUnid = entry.Key;
                    if (this.tokenDistribution[positionUnid] != null)
                    {
                        List<AbstractToken> thisTokens = this.tokenDistribution[positionUnid];
                        List<AbstractToken> tokens = entry.Value;
                        if ((tokens != null) && (thisTokens != null))
                        {
                            if (tokens.Count != thisTokens.Count)
                            {
                                ret = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "State " + this.name;
        }

        private bool isNearby(PointF point, PointF distance)
        {
            bool ret = false;
            if ((Math.Abs(this.origo.X - point.X) < distance.X) &&
            (Math.Abs(this.origo.Y - point.Y) < distance.Y))
            {
                ret = true;
            }
            return ret;
        }

        public bool isNearby(PointF point)
        {
            return this.isNearby(point, this.NearByDistance);
        }

        public bool isNearbyForNew(PointF point)
        {
            return this.isNearby(point, this.NearByDistanceForNew);
        }

        public void draw(Graphics g, NetworkVisualSettings visualSettings, StateVector actualState)
        {
            RectangleF rect = this.Rectangle;
            Pen pen = ( this.Equals(actualState) ? visualSettings.SelectedItemPen : visualSettings.StatePen);
            g.DrawEllipse(pen, rect);
            SizeF textSize = g.MeasureString(this.name, visualSettings.DefaultFont);
            g.DrawString(this.name, visualSettings.DefaultFont, visualSettings.StateBrush, this.origo.X - textSize.Width / 2, this.origo.Y - textSize.Height / 2);
        }

        public static StateVector findItemByUnid(List<StateVector> states, long unid)
        {
            StateVector ret = null;
            int i = 0;
            bool find = false;
            while ((i < states.Count) && (!find))
            {
                if (unid.Equals(states[i].Unid))
                {
                    ret = states[i];
                    find = true;
                }
                i++;
            }
            return ret;
        }

    }
}
