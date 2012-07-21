using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Exceptions
{
    public class PetriNetworkException : Exception
    {

        public PetriNetworkException() : base()
        {
            //   
        }

        public PetriNetworkException( string message )
            : base(message)
        {
            //
        }

        public PetriNetworkException(string message, Exception innerException)
            : base(message,innerException)
        {
            //
        }


    }
}
