using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    internal class DataFormatExeption : Exception
    {
        public DataFormatExeption(string text): base (text) { }
    }
}
