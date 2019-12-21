using System;

namespace LoFuUnit
{
    public class InvalidTestFunctionException : Exception
    {
        public InvalidTestFunctionException(string message) : base(message)
        {
        }
    }
}