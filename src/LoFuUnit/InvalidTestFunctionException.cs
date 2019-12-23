using System;

namespace LoFuUnit
{
    /// <summary>
    /// The exception that is thrown when a test function is invalid.
    /// </summary>
    public class InvalidTestFunctionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTestFunctionException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidTestFunctionException(string message) : base(message)
        {
        }
    }
}