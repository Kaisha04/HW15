using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    internal class WrongMarketException : Exception
    {
        public WrongMarketException(string message ) : base(message) { }
    }
}
