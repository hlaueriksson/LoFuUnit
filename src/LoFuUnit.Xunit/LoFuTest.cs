using Xunit.Abstractions;

namespace LoFuUnit.Xunit
{
    /// <summary>
    /// Base class for <c>Xunit</c> test fixtures.
    /// </summary>
    public abstract class LoFuTest : LoFuUnit.LoFuTest
    {
        /// <summary>
        /// Test output log writer.
        /// </summary>
        protected ITestOutputHelper Output { get; }

        /// <summary>
        /// Initializes a new instance of the test fixture with a <see cref="ITestOutputHelper"/>.
        /// </summary>
        /// <param name="output">A test output log writer</param>
        protected LoFuTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Writes the specified message, followed by the current line terminator, to the test output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        protected override void Log(string message)
        {
            Output.WriteLine(message);
        }
    }
}