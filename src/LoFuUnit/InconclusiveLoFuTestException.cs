using System;

namespace LoFuUnit
{
    /// <summary>
    /// The exception that is thrown when a test is inconclusive.
    /// </summary>
    public class InconclusiveLoFuTestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InconclusiveLoFuTestException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InconclusiveLoFuTestException(string message) : base(message)
        {
        }
    }
}