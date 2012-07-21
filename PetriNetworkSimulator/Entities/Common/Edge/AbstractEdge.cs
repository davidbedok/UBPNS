using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Entities.Common.Edge
{
    public abstract partial class AbstractEdge : AbstractItem
    {
        private const float MINIMUM_WEIGHT = 1;
        private const float MAXIMUM_WEIGHT = 10;

        public const double DISTANCE_LIMIT = 2;
        public const double DISTANCE_LIMIT_FROM_HELPCIRCLE = 5;
        private const float STRING_ROT_OFFSET_VALUE = 6;

        private int weight;
        protected Position position;
        protected Transition transition;
        protected PointF curveMiddlePointOffset;
        protected EdgeType edgeType;

        public EdgeType EdgeType
        {
            get { return this.edgeType; }
            set {
                if (EdgeType.RESET.Equals(value))
                {
                    this.weight = 1;
                }
                this.edgeType = value; 
            }
        }

        public abstract AbstractNetworkItem Start
        {
            get;
            set;
        }

        public abstract AbstractNetworkItem End
        {
            get;
            set;
        }

        public PointF CurveMiddlePointOffset
        {
            get { return this.curveMiddlePointOffset; }
            set { this.curveMiddlePointOffset = value; }
        }

        public int Weight
        {
            get { return this.weight; }
            set {
                if ( ( value >= AbstractEdge.MINIMUM_WEIGHT ) && ( value <= AbstractEdge.MAXIMUM_WEIGHT )) {
                    this.weight = value; 
                }
            }
        }

        public void incrementWeight()
        {
            this.Weight++;
        }

        public AbstractEdge(string name, long unid, bool showAnnotation, int weight, Position position, Transition transition, PointF curveMiddlePoint, EdgeType edgeType)
            : base(name, unid, showAnnotation)
        {
            this.weight = weight;
            this.position = position;
            this.transition = transition;
            this.curveMiddlePointOffset = curveMiddlePoint;
            this.edgeType = edgeType;
        }

        protected abstract PointF getStartPoint();
        protected abstract PointF getEndPoint();

        private static double getDistance( PointF start, PointF end)
        {
            float a = Math.Abs(end.X - start.X);
            float b = Math.Abs(end.Y - start.Y);
            return Math.Sqrt(a * a + b * b);
        }

        public double getRelativeDistance( PointF point )
        {   
            double length = AbstractEdge.getDistance(this.Start.Origo, this.End.Origo);
            double startDistance = AbstractEdge.getDistance(this.Start.Origo, point);
            double endDistance = AbstractEdge.getDistance(this.End.Origo, point);
            return Math.Abs(length - (startDistance + endDistance));
        }

        public double getRelativeDistanceFromMiddle(PointF point)
        {
            PointF middlePoint = this.getCurveMiddlePoint();
            double a = Math.Abs(middlePoint.X - point.X);
            double b = Math.Abs(middlePoint.Y - point.Y);
            return Math.Sqrt(a * a + b * b);
        }

        public PointF getMiddlePoint()
        {
            float la = Math.Abs(this.End.Origo.X - this.Start.Origo.X) / 2;
            float lb = Math.Abs(this.End.Origo.Y - this.Start.Origo.Y) / 2;
            float a = this.Start.Origo.X + la * (this.Start.Origo.X < this.End.Origo.X ? 1 : -1);
            float b = this.Start.Origo.Y + lb * (this.Start.Origo.Y < this.End.Origo.Y ? 1 : -1);
            return new PointF(a, b);
        }

        public PointF getCurveMiddlePoint()
        {
            PointF middlePoint = this.getMiddlePoint();
            return new PointF(middlePoint.X + this.curveMiddlePointOffset.X, middlePoint.Y + this.curveMiddlePointOffset.Y);
        }

        public void setCurveMiddlePointOffset(PointF newMiddlePoint)
        {
            PointF middlePoint = this.getMiddlePoint();
            this.curveMiddlePointOffset = new PointF(newMiddlePoint.X - middlePoint.X, newMiddlePoint.Y - middlePoint.Y);
        }

        public bool isZeroCurveMiddlePointOffset()
        {
            return ((this.curveMiddlePointOffset.X == 0) && (this.curveMiddlePointOffset.Y == 0));
        }

        public void draw(Graphics g, bool selected, NetworkVisualSettings visualSettings, NetworkVisibleSettings visibleSettings)
        {
            Pen pen = visualSettings.DefaultEdgePen;
            if (EdgeType.NORMAL.Equals(this.edgeType))
            {
                pen = (selected ? visualSettings.SelectedEdgePen : visualSettings.DefaultEdgePen);
                pen.Width = (this.Weight); // + 1
            }
            else if (EdgeType.INHIBITOR.Equals(this.edgeType))
            {
                pen = (selected ? visualSettings.SelectedInhibitorArcPen : visualSettings.InhibitorArcPen);
                pen.Width = (this.Weight); // + 1
            }
            else if (EdgeType.RESET.Equals(this.edgeType))
            {
                pen = (selected ? visualSettings.SelectedResetArcPen : visualSettings.ResetArcPen);
            }
            PointF startPoint = this.getStartPoint();
            PointF endPoint = this.getEndPoint();
            if (this.showAnnotation)
            {
                if ((this.Weight > 1) || (!"".Equals(this.name)))
                {
                    string label = "";
                    if ((this.Weight > 1) && ("".Equals(this.name)))
                    {
                        label = (visibleSettings.VisibleEdgeWeight ? this.Weight.ToString() : "");
                    }
                    else if ((this.Weight <= 1) && (!"".Equals(this.name)))
                    {
                        label = (visibleSettings.VisibleEdgeLabel ? this.name : "");
                    }
                    else
                    {
                        label = (visibleSettings.VisibleEdgeLabel ? this.name : "") + (visibleSettings.VisibleEdgeWeight ? " (" + this.Weight.ToString() + ")" : "");
                    }

                    float la = Math.Abs(endPoint.X - startPoint.X) / 2;
                    float lb = Math.Abs(endPoint.Y - startPoint.Y) / 2;
                    float a = startPoint.X + la * (startPoint.X < endPoint.X ? 1 : -1);
                    float b = startPoint.Y + lb * (startPoint.Y < endPoint.Y ? 1 : -1);

                    float offsetX = AbstractEdge.STRING_ROT_OFFSET_VALUE;
                    float offsetY = -AbstractEdge.STRING_ROT_OFFSET_VALUE;
                    SizeF textSize = g.MeasureString(label, visualSettings.DefaultFont);
                    double angle = Math.Atan(la / lb);
                    double angleDeg = angle * 180 / Math.PI;
                    if ((this.Start.Origo.X <= this.End.Origo.X) && (this.Start.Origo.Y <= this.End.Origo.Y))
                    {
                        angleDeg = 90 - angleDeg;
                    }
                    if ((this.Start.Origo.X < this.End.Origo.X) && (this.Start.Origo.Y > this.End.Origo.Y))
                    {
                        angleDeg = -90 + angleDeg;
                        offsetX *= -1;
                    }
                    if ((this.Start.Origo.X >= this.End.Origo.X) && (this.Start.Origo.Y >= this.End.Origo.Y))
                    {
                        angleDeg = 180 - 90 - angleDeg;
                    }
                    if ((this.Start.Origo.X > this.End.Origo.X) && (this.Start.Origo.Y < this.End.Origo.Y))
                    {
                        angleDeg = 180 + 90 + angleDeg;
                        offsetX *= -1;
                    }


                    AbstractItem.MATRIX.RotateAt((float)angleDeg, new PointF(a, b), MatrixOrder.Append);
                    g.Transform = AbstractItem.MATRIX;
                    /* if (!this.isZeroCurveMiddlePointOffset())
                    {
                        PointF middlePoint = this.getCurveMiddlePoint();
                        a = middlePoint.X;
                        b = middlePoint.Y;
                    }*/
                    PointF strPoint = new PointF(a - textSize.Width / 2 + offsetX, b - textSize.Height / 2 + offsetY);
                    if (!this.isZeroCurveMiddlePointOffset())
                    {
                        PointF middlePoint = this.getCurveMiddlePoint();
                        strPoint = new PointF(middlePoint.X - textSize.Width / 2, middlePoint.Y - textSize.Height / 2);
                    }
                    g.DrawString(label, visualSettings.DefaultFont, visualSettings.DefaultBrush, strPoint);
                    AbstractItem.MATRIX.Reset();
                    g.Transform = AbstractItem.MATRIX;
                }
            }
            if (this.isZeroCurveMiddlePointOffset())
            {
                g.DrawLine(pen, startPoint, endPoint);
            }
            else
            {
                PointF middlePoint = this.getMiddlePoint();
                PointF[] points = new PointF[3];
                points[0] = startPoint;
                points[1] = new PointF(middlePoint.X + this.curveMiddlePointOffset.X, middlePoint.Y + this.curveMiddlePointOffset.Y);
                points[2] = endPoint;
                g.DrawCurve(pen, points);
                if (visibleSettings.VisibleEdgeHelpLine)
                {
                    // g.DrawLine(visualSettings.HelpPen, this.getStartPoint(), this.getEndPoint());
                    // g.DrawEllipse(visualSettings.HelpPen, new RectangleF(middlePoint.X - 5, middlePoint.Y - 5, 10, 10));
                    g.DrawEllipse(visualSettings.HelpPen, new RectangleF(points[1].X - 5, points[1].Y - 5, 10, 10));
                }
            }
        }

    }
}
