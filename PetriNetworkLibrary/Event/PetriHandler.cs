using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;

namespace PetriNetworkLibrary.Event
{

    public delegate void PetriHandler(AbstractEventDrivenItem item, EventType eventType); 

}
