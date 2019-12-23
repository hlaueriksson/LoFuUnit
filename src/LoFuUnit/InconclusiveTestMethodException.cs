using System;

namespace LoFuUnit
{
    /// <summary>
    /// The exception that is thrown when a test method is inconclusive.
    /// </summary>
    public class InconclusiveTestMethodException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InconclusiveTestMethodException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InconclusiveTestMethodException(string message) : base(message)
        {
        }
    }
}