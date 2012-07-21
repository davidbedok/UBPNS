using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Model.Edge
{
    public class EdgePositionTransition : AbstractEdge
    {

        public override AbstractEventDrivenItem Start
        {
            get { return this.position; }
        }

        public override AbstractEventDrivenItem End
        {
            get { return this.transition; }
        }

        public Position StartPosition
        {
            get { return this.position; }
        }

        public Transition EndTransition
        {
            get { return this.transition; }
        }

        public EdgePositionTransition(string name, long unid, int weight, Position position, Transition transition, EdgeType edgeType)
            : base(name, unid, weight, position, transition, edgeType)
        {
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("  --== EDGE POSITION-TRANSITION ==--");
            sb.Append(base.ToString());
            sb.AppendLine("  startPosition: " + StartPosition.Unid);
            sb.AppendLine("  endTransition: " + EndTransition.Unid);
            return sb.ToString();
        }


    }
}
