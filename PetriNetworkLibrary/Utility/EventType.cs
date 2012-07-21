using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkLibrary.Utility
{
    public enum EventType
    {
        PREACTIVATE,
        POSTACTIVATE,
        DEADLOCK,
        CYCLE,
        TICK
    }
}
