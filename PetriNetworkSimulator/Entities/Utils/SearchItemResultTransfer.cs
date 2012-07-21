using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Enums;

namespace PetriNetworkSimulator.Entities.Utils
{
    public class SearchItemResultTransfer
    {

        private AbstractItem item;
        private MoveCorner moveEdge;

        public AbstractItem Item
        {
            get { return item; }
        }

        public MoveCorner MoveEdge
        {
            get { return moveEdge; }
        }

        public SearchItemResultTransfer(AbstractItem item, MoveCorner moveEdge)
        {
            this.item = item;
            this.moveEdge = moveEdge;
        }

        public override string ToString()
        {
            return item.ToString() + " - " + moveEdge.ToString();
        }


    }
}
