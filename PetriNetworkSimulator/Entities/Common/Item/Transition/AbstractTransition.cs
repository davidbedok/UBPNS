using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Item.Base;

namespace PetriNetworkSimulator.Entities.Common.Item.Transition
{
    public abstract partial class AbstractTransition : AbstractNetworkItem
    {
        public const float DEFAULT_CLOCK_RADIUS = 5F;

        private int priority;
        private double angleRad;
        private double angle;
        private TransitionType transitionType;
        private int delay;
        private float clockRadius;
        protected PointF clockOffset;

        public TransitionType TransitionType
        {
            get { return this.transitionType; }
            set { this.transitionType = value;}
        }

        public int Priority
        {
            get { return this.priority; }
            set {
                if (value >= 0)
                {
                    this.priority = value;
                }
            }
        }

        public double Angle
        {
            get { return this.angle; }
            set {
                this.angle = value;
                this.angleRad = this.angle / 180 * Math.PI;
            }
        }

        public double AngleRadian
        {
            get { return this.angleRad; }
        }

        public int Delay
        {
            get { return this.delay; }
            set {
                if (value >= 0)
                {
                    this.delay = value;
                }
            }
        }

        public float ClockRadius
        {
            get { return this.clockRadius; }
            set
            {
                if (value >= 5)
                {
                    this.clockRadius = value;
                }
            }
        }

        public PointF ClockOffset
        {
            get { return this.clockOffset; }
            set { this.clockOffset = value; }
        }

        public AbstractTransition(string name, long unid, bool showAnnotation, double angle, PointF origo, SizeF size, int priority, TransitionType transitionType, int delay, float clockRadius, PointF clockOffset) 
            : base(name, unid, showAnnotation, origo, size)
        {
            this.Angle = angle;
            this.priority = priority;
            this.transitionType = transitionType;
            this.delay = delay;
            this.clockRadius = clockRadius;
            this.clockOffset = clockOffset;
        }

        public void setTransitionParametersForResize(MoveCorner moveCorner, PointF offset)
        {
            PointF moveOffset = offset;
            switch (moveCorner.Value)
            {
                case "TOPLEFT":
                    moveOffset = new PointF((-1) * offset.X, (-1) * offset.Y);
                    break;
                case "TOPRIGHT":
                    moveOffset = new PointF(offset.X, (-1) * offset.Y);
                    break;
                case "BOTTOMLEFT":
                    moveOffset = new PointF((-1) * offset.X, offset.Y);
                    break;
                case "BOTTOMRIGHT":
                    moveOffset = new PointF(offset.X, offset.Y);
                    break;
            }
            this.Size = new SizeF(this.Size.Width + moveOffset.X, this.Size.Height + moveOffset.Y);
        }

    }
}
