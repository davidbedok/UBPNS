using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Item.Position;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Entities.Edge
{
    public class EdgePositionTransition : AbstractEdge
    {

        public override AbstractNetworkItem Start
        {
            get { return this.position; }
            set { this.position = (Position)value; }
        }

        public override AbstractNetworkItem End
        {
            get { return this.transition; }
            set { this.transition = (Transition)value; }
        }

        public Position StartPosition
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Transition EndTransition
        {
            get { return this.transition; }
            set { this.transition = value; }
        }

        public EdgePositionTransition(string name, long unid, bool showAnnotation, int weight, Position position, Transition transition, PointF curveMiddlePoint, EdgeType edgeType)
            : base(name, unid, showAnnotation, weight, position, transition, curveMiddlePoint, edgeType)
        {

        }

        protected override PointF getStartPoint()
        {
            float A = Math.Abs(this.End.Origo.X - this.Start.Origo.X);
            float B = Math.Abs(this.End.Origo.Y - this.Start.Origo.Y);
            double R = Math.Sqrt(A * A + B * B);
            float r = this.StartPosition.Radius;
            float a = (float)(r * A / R);
            float b = (float)(r * B / R);
            return new PointF(this.Start.Origo.X + (this.End.Origo.X <= this.Start.Origo.X ? -a : +a), this.Start.Origo.Y + (this.End.Origo.Y <= this.Start.Origo.Y ? -b : +b));
        }

        /*
        protected override PointF getEndPoint()
        {
            return this.End.Origo;
        }
         * */

        protected override PointF getEndPoint()
        {
            float A = Math.Abs(this.End.Origo.X - this.Start.Origo.X);
            float B = Math.Abs(this.End.Origo.Y - this.Start.Origo.Y);
            double R = Math.Sqrt(A * A + B * B);
            float r = this.EndTransition.Radius;
            float a = (float)(r * A / R);
            float b = (float)(r * B / R);
            return new PointF(this.End.Origo.X + (this.End.Origo.X <= this.Start.Origo.X ? +a : -a), this.End.Origo.Y + (this.End.Origo.Y <= this.Start.Origo.Y ? +b : -b));
        }

        public override string ToString()
        {
            return this.position + " --(" + this.Weight + ")--> " + this.transition;
        }

    }
}
