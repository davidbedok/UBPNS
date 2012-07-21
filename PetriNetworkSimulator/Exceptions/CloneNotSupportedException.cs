using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Exceptions
{
    public class CloneNotSupportedException : Exception
    {

        public CloneNotSupportedException() : base()
        {
            //   
        }

        public CloneNotSupportedException( string message )
            : base(message)
        {
            //
        }

        public CloneNotSupportedException(string message, Exception innerException)
            : base(message,innerException)
        {
            //
        }
    }
}
