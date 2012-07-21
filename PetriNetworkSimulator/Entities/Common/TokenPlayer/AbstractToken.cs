using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.Common.TokenPlayer
{
    public abstract partial class AbstractToken : AbstractItem
    {
        protected const float TOKEN_RADIUS = 5;

        protected Color tokenColor;
        private Brush tokenBrush;

        public Color TokenColor
        {
            get { return this.tokenColor; }
            set { 
                this.tokenColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.tokenBrush = new SolidBrush(this.tokenColor);
            }
        }

        public Brush TokenBrush
        {
            get { return this.tokenBrush; }
        }

        public AbstractToken(string name, long unid, bool showAnnotation)
            : base(name, unid, showAnnotation)
        {
            this.TokenColor = Color.FromArgb(0, 0, 0);
        }

        public abstract void draw(Graphics g, PointF point, NetworkVisualSettings visualSettings);

        public override string ToString()
        {
            return "TOKEN - " + base.ToString();
        }

    }
}
