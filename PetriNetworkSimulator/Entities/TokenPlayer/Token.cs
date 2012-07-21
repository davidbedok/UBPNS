using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Utils;

namespace PetriNetworkSimulator.Entities.TokenPlayer
{
    public class Token : AbstractToken
    {

        public Token(string name, long unid, bool showAnnotation)
            : base(name, unid, showAnnotation)
        {
            //
        }

        public override void draw(Graphics g, PointF point, NetworkVisualSettings visualSettings)
        {
            g.FillEllipse(this.TokenBrush, point.X - AbstractToken.TOKEN_RADIUS, point.Y - AbstractToken.TOKEN_RADIUS, 2 * AbstractToken.TOKEN_RADIUS, 2 * AbstractToken.TOKEN_RADIUS);
        }

    }
}
