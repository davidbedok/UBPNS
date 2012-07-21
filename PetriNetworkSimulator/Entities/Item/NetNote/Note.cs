using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Item.Note;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Entities.Item.NetNote
{
    public partial class Note : AbstractNote
    {

        public Note(string name, long unid, bool showAnnotation, PointF origo, SizeF size, AbstractItem attachedItem, string text)
            : base(name, unid, showAnnotation, origo, size, attachedItem, text)
        {

        }

        private void drawNote(Graphics g, Pen pen, Brush brush, RectangleF rect)
        {
            PointF[] points = new PointF[5];
            PointF point = rect.Location;
            SizeF size = rect.Size;
            points[0] = point;
            points[1] = new PointF(point.X, point.Y + size.Height);
            points[2] = new PointF(point.X + size.Width, point.Y + size.Height);
            points[3] = new PointF(point.X + size.Width, point.Y + AbstractNote.NOTEEDGECUT);
            points[4] = new PointF(point.X + size.Width - AbstractNote.NOTEEDGECUT, point.Y);
            g.FillPolygon(brush, points);
            if (pen != null)
            {
                g.DrawPolygon(pen, points);
            }
        }

        protected PointF getPositionPoint(PointF start, PointF end, float radius)
        {
            float A = Math.Abs(end.X - start.X);
            float B = Math.Abs(end.Y - start.Y);
            double R = Math.Sqrt(A * A + B * B);
            float a = (float)(radius * A / R);
            float b = (float)(radius * B / R);
            return new PointF(end.X + (end.X <= start.X ? +a : -a), end.Y + (end.Y <= start.Y ? +b : -b));
        }

        public override void draw(Graphics g, bool selected, bool mark, NetworkVisualSettings visualSettings, NetworkVisibleSettings visibleSettings, bool markAsReadyToFire, bool showHelpEllipse)
        {
            // base.draw(g, selected, mark, visualSettings, visibleSettings, markAsReadyToFire);
            if (visibleSettings.VisibleNotes)
            {
                if (this.attachedItem != null)
                {
                    if (this.attachedItem is AbstractNetworkItem)
                    {
                        AbstractNetworkItem networkItem = (AbstractNetworkItem)this.attachedItem;
                        if (this.attachedItem is Transition)
                        {
                            g.DrawLine(visualSettings.NoteLinePen, this.Origo, networkItem.Origo);
                        }
                        else if (this.attachedItem is Position)
                        {
                            Position position = (Position)networkItem;
                            g.DrawLine(visualSettings.NoteLinePen, this.Origo, this.getPositionPoint(this.Origo, position.Origo, position.Radius));
                        }
                    }
                    else if (this.attachedItem is AbstractEdge)
                    {
                        AbstractEdge edgeItem = (AbstractEdge)this.attachedItem;
                        g.DrawLine(visualSettings.NoteLinePen, this.Origo, edgeItem.getCurveMiddlePoint());
                    }
                }

                this.drawNote(g, null, visualSettings.ShadowBrush, this.ShadowRectangle);
                this.drawNote(g, (selected ? visualSettings.SelectedItemPen : visualSettings.NoteBorderPen), visualSettings.NoteBrush, this.Rectangle);
                g.DrawString(this.text, visualSettings.NoteFont, visualSettings.NoteTextBrush, this.Rectangle);
            }
        }

    }
}
