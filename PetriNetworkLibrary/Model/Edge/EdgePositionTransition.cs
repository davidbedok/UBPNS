using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Utility;
using PetriNetworkSimulator.Entities.Common.Base;

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

        public EdgePositionTransition(AbstractItemData itemData, int weight, Position position, Transition transition, EdgeType edgeType)
            : this(itemData.name, itemData.unid, itemData.showAnnotation, weight, position, transition, edgeType)
        {

        }

        public EdgePositionTransition(string name, long unid, bool showAnnotation, int weight, Position position, Transition transition, EdgeType edgeType)
            : base(name, unid, showAnnotation, weight, position, transition, edgeType)
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
