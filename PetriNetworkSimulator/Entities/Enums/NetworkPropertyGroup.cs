using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Enums
{
    public enum NetworkPropertyGroup
    {
        NONE,
        POSITION,
        TRANSITION,
        TOKEN,
        EDGE,
        ABSTRACTNETWORKITEM // Position + Transition
    }
}
