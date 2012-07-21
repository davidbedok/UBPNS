using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Item.NetTransition
{
    public partial class Transition : AbstractTransition, IPetriItem
    {

        public string TypeStr
        {
            get { return "Transition"; }
        }

        public Transition(string name, long unid, bool showAnnotation, double angle, PointF origo, SizeF size, int priority, TransitionType transitionType, int delay, float clockRadius, PointF clockOffset)
            : base(name, unid, showAnnotation, angle, origo, size, priority, transitionType, delay, clockRadius, clockOffset)
        {
            // 
        }

        public void drawClock(Graphics g, PointF clockOrigo, NetworkVisualSettings visualSettings)
        {
            float bigRadius = this.ClockRadius;
            float smallRadius = bigRadius * 2 / 3;
            g.DrawEllipse(visualSettings.ClockPen, clockOrigo.X - bigRadius, clockOrigo.Y - bigRadius, 2 * bigRadius, 2 * bigRadius);
            PointF p1 = new PointF((float)(clockOrigo.X - (bigRadius - 1) * Math.Cos(Math.PI / 3)), (float)(clockOrigo.Y - (bigRadius - 1) * Math.Sin(Math.PI / 3)));
            g.DrawLine(visualSettings.ClockPen, clockOrigo, p1);
            PointF p2 = new PointF((float)(clockOrigo.X + smallRadius * Math.Cos(Math.PI / 6)), (float)(clockOrigo.Y - smallRadius * Math.Sin(Math.PI / 6)));
            g.DrawLine(visualSettings.ClockPen, clockOrigo, p2);
            g.DrawString(this.Delay.ToString() + " ms", visualSettings.DefaultFont, visualSettings.ClockBrush, new PointF(clockOrigo.X + smallRadius, clockOrigo.Y + smallRadius));
        }

        public override void draw(Graphics g, bool selected, bool mark, NetworkVisualSettings visualSettings, NetworkVisibleSettings visibleSettings, bool markAsReadyToFire, bool showHelpEllipse)
        {
            base.draw(g, selected, mark, visualSettings, visibleSettings, markAsReadyToFire, showHelpEllipse);
            Brush brush = ((mark && !TransitionType.SOURCE.Equals(this.TransitionType)) ? visualSettings.MarkBrush : (selected ? visualSettings.SelectedItemBrush : (markAsReadyToFire ? visualSettings.MarkAsReadyToFireBrush : visualSettings.DefaultBrush)));

            AbstractItem.MATRIX.RotateAt((float)this.Angle, this.Origo, MatrixOrder.Append);
            g.Transform = AbstractItem.MATRIX;
            if ( TransitionType.NORMAL.Equals(this.TransitionType) ) {
                g.FillRectangle(brush, this.Rectangle);
            }
            else
            {
                Pen pen = ((mark && !TransitionType.SOURCE.Equals(this.TransitionType)) ? visualSettings.MarkPen : (selected ? visualSettings.SelectedItemPen : (markAsReadyToFire ? visualSettings.MarkAsReadyToFirePen : visualSettings.DefaultPen)));
                RectangleF rect = this.Rectangle;
                g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                if (TransitionType.SOURCE.Equals(this.TransitionType)) {
                    float radius = 3;
                    if (this.Size.Width > this.Size.Height)
                    {
                        radius = Math.Abs(this.Size.Height / 2 - 1);
                    }
                    else
                    {
                        radius = Math.Abs(this.Size.Width / 2 - 1);
                    }
                    g.FillEllipse(brush, this.Origo.X - radius, this.Origo.Y - radius, 2 * radius, 2 * radius);
                }
                else if (TransitionType.SINK.Equals(this.TransitionType))
                {
                    //
                } 
            }

            AbstractItem.MATRIX.Reset();
            g.Transform = AbstractItem.MATRIX;
            if (this.showAnnotation)
            {
                string labelName = (visibleSettings.VisibleTransitionLabel ? this.name : "");
                string label = labelName + (this.Priority > 0 && visibleSettings.VisiblePriority ? ("".Equals(labelName) ? "" : " (") + this.Priority.ToString() + ("".Equals(labelName) ? "" : ")") : "");
                if (!"".Equals(label))
                {
                    SizeF textSize = g.MeasureString(label, visualSettings.DefaultFont);
                    float offset = (this.Size.Width - textSize.Width) / 2;
                    g.DrawString(label, visualSettings.DefaultFont, visualSettings.DefaultBrush, this.Point.X + offset + this.LabelOffset.X, this.Point.Y - textSize.Height + this.LabelOffset.Y);
                }
            }
            if ((visibleSettings.VisibleClock) && ( this.Delay > 0 ))
            {
                this.drawClock(g, new PointF(this.Origo.X + this.clockOffset.X, this.Origo.Y + this.clockOffset.Y), visualSettings);
            }
        }

        public override string ToString()
        {
            return "Transition " + this.name + " (type: " + this.TransitionType + ")";
        }

    }
}
