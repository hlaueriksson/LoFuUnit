using System;

namespace LoFuUnit
{
    public class InconclusiveTestMethodException : Exception
    {
        public InconclusiveTestMethodException(string message) : base(message)
        {
        }
    }
}