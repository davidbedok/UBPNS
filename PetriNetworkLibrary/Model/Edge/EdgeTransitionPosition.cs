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
    public class EdgeTransitionPosition : AbstractEdge
    {

        public override AbstractEventDrivenItem Start
        {
            get { return this.transition; }
        }

        public override AbstractEventDrivenItem End
        {
            get { return this.position; }
        }

        public Transition StartTransition
        {
            get { return this.transition; }
        }

        public Position EndPosition
        {
            get { return this.position; }
        }

        public EdgeTransitionPosition(AbstractItemData itemData, int weight, Transition transition, Position position, EdgeType edgeType)
            : this(itemData.name, itemData.unid, itemData.showAnnotation, weight, transition, position, edgeType)
        {
        }

        public EdgeTransitionPosition(string name, long unid, bool showAnnotation, int weight, Transition transition, Position position, EdgeType edgeType)
            : base(name, unid, showAnnotation, weight, position, transition, edgeType)
        {
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendLine("  --== EDGE TRANSITION-POSITION ==--");
            sb.Append(base.ToString());
            sb.AppendLine("  startTransition: " + StartTransition.Unid);
            sb.AppendLine("  endPosition: " + EndPosition.Unid);
            return sb.ToString();
        }


    }
}
