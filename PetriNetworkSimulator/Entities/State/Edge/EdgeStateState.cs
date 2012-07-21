using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.State.Vector;

namespace PetriNetworkSimulator.Entities.State.Edge
{
    public partial class EdgeStateState
    {

        private StateVector startState;
        private StateVector endState;

        public StateVector Start
        {
            get { return this.startState; }
        }

        public StateVector End
        {
            get { return this.endState; }
        }

        public EdgeStateState(StateVector startState, StateVector endState)
        {
            this.startState = startState;
            this.endState = endState;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is EdgeStateState))
            {
                return false;
            }
            return this.Equals((EdgeStateState)obj);
        }

        public bool Equals(EdgeStateState sv)
        {
            bool ret = false;
            if (sv != null)
            {
                if ((sv.Start.Equals(this.startState)) && (sv.End.Equals(this.endState)))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected PointF getStartPoint()
        {
            float A = Math.Abs(this.End.Origo.X - this.Start.Origo.X);
            float B = Math.Abs(this.End.Origo.Y - this.Start.Origo.Y);
            double R = Math.Sqrt(A * A + B * B);
            float r = this.Start.Radius;
            float a = (float)(r * A / R);
            float b = (float)(r * B / R);
            return new PointF(this.Start.Origo.X + (this.End.Origo.X <= this.Start.Origo.X ? -a : +a), this.Start.Origo.Y + (this.End.Origo.Y <= this.Start.Origo.Y ? -b : +b));
        }

        protected PointF getEndPoint()
        {
            float A = Math.Abs(this.End.Origo.X - this.Start.Origo.X);
            float B = Math.Abs(this.End.Origo.Y - this.Start.Origo.Y);
            double R = Math.Sqrt(A * A + B * B);
            float r = this.End.Radius;
            float a = (float)(r * A / R);
            float b = (float)(r * B / R);
            return new PointF(this.End.Origo.X + (this.End.Origo.X <= this.Start.Origo.X ? +a : -a), this.End.Origo.Y + (this.End.Origo.Y <= this.Start.Origo.Y ? +b : -b));
        }

        public void draw(Graphics g, NetworkVisualSettings visualSettings)
        {
            g.DrawLine(visualSettings.StateEdgePen, this.getStartPoint(), this.getEndPoint());
        }

        public override string ToString()
        {
            return "[EdgeStateState] " + this.Start + " -- " + this.End;
        }

    }
}
