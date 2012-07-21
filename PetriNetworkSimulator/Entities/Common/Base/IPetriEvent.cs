using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkSimulator.Entities.Event;

namespace PetriNetworkSimulator.Entities.Common.Base
{
    public interface IPetriEvent
    {

        EventTrunk PetriEvents
        {
            get;
        }

    }
}
