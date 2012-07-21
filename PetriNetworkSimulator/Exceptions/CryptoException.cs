using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Exceptions
{
    public class CryptoException : Exception
    {

        public CryptoException() : base()
        {
            //   
        }

        public CryptoException( string message )
            : base(message)
        {
            //
        }

        public CryptoException(string message, Exception innerException)
            : base(message,innerException)
        {
            // 
        }

    }
}
