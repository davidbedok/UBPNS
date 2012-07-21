using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Common.Base
{
    public interface IPetriItem
    {

        string Name
        {
            get;
            set;
        }

        string TypeStr
        {
            get;
        }

    }
}
