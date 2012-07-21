using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Entities.Common.Item.Position
{
    public abstract partial class AbstractPosition : AbstractNetworkItem
    {

        protected const float TOKEN_MOVE_MINI = 3;
        protected const float TOKEN_MOVE_MINI2 = 5;
        protected const float TOKEN_MOVE_SMALL = 6;
        protected const float TOKEN_MOVE = 8;

        public AbstractPosition(string name, long unid, bool showAnnotation, PointF origo, float radius)
            : base(name, unid, showAnnotation, origo, radius)
        {
            
        }

        public void setPositionParametersForResize(MoveCorner moveCorner, PointF offset)
        {
            float radiusOffset = Math.Min(offset.X, offset.Y);
            switch (moveCorner.Value)
            {
                case "TOPLEFT":
                    radiusOffset = Math.Min((-1) * offset.X, (-1) * offset.Y);
                    break;
                case "TOPRIGHT":
                    radiusOffset = Math.Min(offset.X, (-1) * offset.Y);
                    break;
                case "BOTTOMLEFT":
                    radiusOffset = Math.Min((-1) * offset.X, offset.Y);
                    break;
                case "BOTTOMRIGHT":
                    radiusOffset = Math.Min(offset.X, offset.Y);
                    break;
            }
            this.Radius = this.Radius + radiusOffset;
        }

    }
}
