using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Entities.Common.Item.Base
{
    public abstract partial class AbstractNetworkItem : AbstractItem, IPetriEvent
    {
        private const float HELP_ELLIPSE_RADIUS = 2;
        private const float HELP_ELLIPSE_RADIUS_DRAW = 3;
        public const float MINIMUM_WIDTH = 5;
        public const float MINIMUM_HEIGHT = 10;
        public const float MINIMUM_RADIUS = 10;
        public const float MAXIMUM_RADIUS = 100;
        private const float SHADOW_X = 3;
        private const float SHADOW_Y = 3;
        public const float MAX_LABEL_OFFSET = 100;
        private const float LABEL_HELP_ELLIPSE_OFFSET = 10;
        
        private PointF point; // top-left
        private SizeF size; // width-height
        private PointF origo;
        private float radius;
        protected PointF labelOffset;

        private EventTrunk events;

        protected GeneralStatistics statistics;

        public EventTrunk PetriEvents
        {
            get { return this.events; }
        }

        public GeneralStatistics Statistics
        {
            get { return this.statistics; }
        }

        protected PointF NearByDistance
        {
            get
            {
                return new PointF(this.Radius - 2 * AbstractNetworkItem.HELP_ELLIPSE_RADIUS, this.Radius - 2 * AbstractNetworkItem.HELP_ELLIPSE_RADIUS);
            }
        }

        protected PointF NearByDistanceForMoveEdge
        {
            get
            {
                return new PointF(2 * AbstractNetworkItem.HELP_ELLIPSE_RADIUS, 2 * AbstractNetworkItem.HELP_ELLIPSE_RADIUS);
            }
        }

        protected PointF NearByDistanceForAddNewItem
        {
            get
            {
                return new PointF(this.Radius * 2, this.Radius * 2);
            }
        }

        public PointF Point
        {
            get { return this.point; }
            set {
                float changeX = value.X - this.point.X;
                float changeY = value.Y - this.point.Y;
                this.origo = new PointF(this.origo.X + changeX, this.origo.Y + changeY);
                this.point = value; 
            }
        }

        public SizeF Size
        {
            get { return this.size; }
            set {
                if ((value.Width >= AbstractNetworkItem.MINIMUM_WIDTH) && (value.Height >= AbstractNetworkItem.MINIMUM_HEIGHT))
                {
                    this.size = value;
                    this.point = AbstractNetworkItem.getPointFromOrigoAndSize(this.origo, this.size);
                    this.radius = AbstractNetworkItem.getRadiusFromSize(this.size);
                }
            }
        }

        public PointF LabelOffset
        {
            get { return this.labelOffset; }
            set { this.labelOffset = value; }
        }

        public RectangleF Rectangle
        {
            get { return new RectangleF(this.point, this.size); }
        }

        public RectangleF ShadowRectangle
        {
            get { return new RectangleF(new PointF(this.point.X + AbstractNetworkItem.SHADOW_X, this.point.Y + AbstractNetworkItem.SHADOW_Y), this.size); }
        }

        public PointF Origo
        {
            get { return this.origo; }
            set {
                float changeX = value.X - this.origo.X;
                float changeY = value.Y - this.origo.Y;
                this.point = new PointF(this.point.X + changeX,this.point.Y + changeY);
                this.origo = value; 
            }
        }

        public float Radius
        {
            get { return this.radius; }
            set {
                if (value >= AbstractNetworkItem.MINIMUM_RADIUS && value <= AbstractNetworkItem.MAXIMUM_RADIUS )
                {
                    this.radius = value;
                    this.point = AbstractNetworkItem.getPointFromOrigoAndRadius(this.origo, this.radius);
                    this.size = AbstractNetworkItem.getSizeFromRadius(this.radius);
                }
            }
        }

        protected static PointF getPointFromOrigoAndRadius(PointF origo, float radius)
        {
            return new PointF(origo.X - radius, origo.Y - radius);
        }

        protected static PointF getPointFromOrigoAndSize(PointF origo, SizeF size)
        {
            return new PointF(origo.X - size.Width / 2, origo.Y - size.Height / 2);
        }

        protected static SizeF getSizeFromRadius(float radius)
        {
            return new SizeF(2 * radius, 2 * radius);
        }

        protected static float getRadiusFromSize(SizeF size)
        {
            float x = size.Width / 2;
            float y = size.Height / 2;
            return (float)Math.Sqrt(x*x + y*y);
        }

        // Position
        public AbstractNetworkItem(string name, long unid, bool showAnnotation, PointF origo, float radius) : base (name, unid, showAnnotation)
        {
            this.init(origo);
            this.Radius = radius;
            // this.point = AbstractNetworkItem.getPointFromOrigoAndRadius(origo, radius);
            // this.size = AbstractNetworkItem.getSizeFromRadius(radius);
        }

        // Transition and Note
        public AbstractNetworkItem(string name, long unid, bool showAnnotation, PointF origo, SizeF size) : base (name, unid, showAnnotation)
        {
            this.init(origo);
            this.Size = size;
            // this.point = AbstractNetworkItem.getPointFromOrigoAndSize(origo, size);
            // this.radius = AbstractNetworkItem.getRadiusFromSize(size);
        }

        private void init(PointF origo)
        {
            this.labelOffset = new PointF(0, 0);
            this.statistics = new GeneralStatistics();
            this.events = new EventTrunk();
            this.origo = origo;
        }

        protected bool isNearby(PointF point, PointF distance)
        {
            bool ret = false;
            if ((Math.Abs(this.origo.X - point.X) < distance.X) &&
            (Math.Abs(this.origo.Y - point.Y) < distance.Y))
            {
                ret = true;
            }
            return ret;
        }

        private bool isNearby(PointF custom, PointF point, PointF distance)
        {
            bool ret = false;
            if ((Math.Abs(custom.X - point.X) < distance.X) &&
            (Math.Abs(custom.Y - point.Y) < distance.Y))
            {
                ret = true;
            }
            return ret;
        }

        public virtual bool isNearby(PointF point)
        {
            return this.isNearby(point, this.NearByDistance);
        }

        public bool isNearby(MoveCorner moveEdge, PointF point)
        {
            PointF custom = this.point;
            switch (moveEdge.Value)
            {
                case "TOPLEFT":
                    custom = this.point;
                    break;
                case "TOPRIGHT":
                    custom = new PointF(this.point.X + this.size.Width, this.point.Y);
                    break;
                case "BOTTOMLEFT":
                    custom = new PointF(this.point.X, this.point.Y + this.size.Height);
                    break;
                case "BOTTOMRIGHT":
                    custom = new PointF(this.point.X + this.size.Width, this.point.Y + this.size.Height);
                    break;
                case "LABEL":
                    custom = new PointF(this.point.X + (this.size.Width / 2) + this.labelOffset.X, this.point.Y - AbstractNetworkItem.LABEL_HELP_ELLIPSE_OFFSET + this.labelOffset.Y);
                    break;
            }
            return this.isNearby(custom, point, this.NearByDistanceForMoveEdge);
        }

        public bool isInsideby(RectangleF rect)
        {
            bool ret = false;
            float coordX1 = rect.Left;
            float coordX2 = rect.Left + rect.Width;
            if (rect.Width < 0)
            {
                coordX1 = rect.Right;
                coordX2 = rect.Right - rect.Width;
            }
            if ((this.origo.X >= coordX1) && (this.origo.X <= coordX2))
            {
                float coordY1 = rect.Top;
                float coordY2 = rect.Top + rect.Height;
                if (rect.Height < 0)
                {
                    coordY1 = rect.Bottom;
                    coordY2 = rect.Bottom - rect.Height;
                }
                if ((this.origo.Y >= coordY1) && (this.origo.Y <= coordY2))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public bool isNearbyForAddNewItem(PointF point)
        {
            return this.isNearby(point, this.NearByDistanceForAddNewItem);
        }

        public override string ToString()
        {
            return this.name + " (" + this.point + ":" + this.size + ")";
        }

        public virtual void draw(Graphics g, bool selected, bool mark, NetworkVisualSettings visualSettings, NetworkVisibleSettings visibleSettings, bool markAsReadyToFire, bool showHelpEllipse)
        {
            if (visibleSettings.VisibleEdgeHelpLine && showHelpEllipse)
            {
                float r = AbstractNetworkItem.HELP_ELLIPSE_RADIUS_DRAW;
                g.DrawEllipse(visualSettings.HelpPen, new RectangleF(this.point.X - r, this.point.Y - r, 2 * r, 2 * r));
                g.DrawEllipse(visualSettings.HelpPen, new RectangleF(this.point.X + this.size.Width - r, this.point.Y - r, 2 * r, 2 * r));
                g.DrawEllipse(visualSettings.HelpPen, new RectangleF(this.point.X - r, this.point.Y + this.size.Height - r, 2 * r, 2 * r));
                g.DrawEllipse(visualSettings.HelpPen, new RectangleF(this.point.X + this.size.Width - r, this.point.Y + this.size.Height - r, 2 * r, 2 * r));

                if (this.showAnnotation && this.name != null && this.name != "")
                {
                    g.DrawEllipse(visualSettings.HelpPen, new RectangleF(this.point.X + (this.size.Width / 2) + this.labelOffset.X - r, this.point.Y - AbstractNetworkItem.LABEL_HELP_ELLIPSE_OFFSET + this.labelOffset.Y - r, 2 * r, 2 * r));
                }
            }
        }

        public static AbstractNetworkItem findItemByName(List<AbstractNetworkItem> items, string name)
        {
            AbstractNetworkItem ret = null;
            int i = 0;
            bool find = false;
            while ((i < items.Count) && (!find))
            {
                if (name.Equals(items[i].Name))
                {
                    ret = items[i];
                    find = true;
                }
                i++;
            }
            return ret;
        }

        public static AbstractNetworkItem findItemByUnid(List<AbstractNetworkItem> items, long unid)
        {
            AbstractNetworkItem ret = null;
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
