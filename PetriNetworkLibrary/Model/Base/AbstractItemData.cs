using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Common.Base
{
    public struct AbstractItemData
    {

        public string name;
        public long unid;
        public bool showAnnotation;

        public AbstractItemData(string name, long unid, bool showAnnotation)
        {
            this.name = name;
            this.unid = unid;
            this.showAnnotation = showAnnotation;
        }

    }
}
