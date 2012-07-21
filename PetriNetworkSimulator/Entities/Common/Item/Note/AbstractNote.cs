using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Entities.Common.Item.Note
{
    public abstract partial class AbstractNote : AbstractNetworkItem
    {
        protected const float NOTEEDGECUT = 10;

        protected AbstractItem attachedItem;
        protected string text;

        public AbstractItem AttachedItem
        {
            get { return this.attachedItem; }
            set { this.attachedItem = value; }
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public AbstractNote(string name, long unid, bool showAnnotation, PointF origo, SizeF size, AbstractItem attachedItem, string text)
            : base(name, unid, showAnnotation, origo, size)
        {
           this.attachedItem = attachedItem;
           this.text = text;
        }

        public override bool isNearby(PointF point)
        {
            bool ret = false;
            if ((point.X >= this.Point.X && point.X <= this.Point.X + this.Size.Width) &&
            (point.Y >= this.Point.Y && point.Y <= this.Point.Y + this.Size.Height)) {
                ret = true;
            }
            return ret;
        }

        public void setNoteParametersForResize(MoveCorner moveCorner, PointF offset)
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
