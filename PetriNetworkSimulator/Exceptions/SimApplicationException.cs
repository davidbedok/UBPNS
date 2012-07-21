using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Exceptions
{
    public class SimApplicationException : Exception
    {

        public SimApplicationException() : base()
        {
            //   
        }

        public SimApplicationException( string message )
            : base(message)
        {
            //
        }

        public SimApplicationException(string message, Exception innerException)
            : base(message,innerException)
        {
            //
        }

    }
}
