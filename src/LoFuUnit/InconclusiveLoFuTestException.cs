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
        public InconclusiveLoFuTestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InconclusiveLoFuTestException"/> class.
        /// </summary>
        public InconclusiveLoFuTestException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InconclusiveLoFuTestException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InconclusiveLoFuTestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
