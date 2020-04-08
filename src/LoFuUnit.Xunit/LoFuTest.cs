using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Xunit
{
    /// <summary>
    /// Base class for <c>Xunit</c> test fixtures.
    /// </summary>
    public abstract class LoFuTest : LoFuUnit.LoFuTest, IAsyncLifetime
    {
        /// <summary>
        /// Test output log writer.
        /// </summary>
        protected ITestOutputHelper Output { get; }

        /// <summary>
        /// Initializes a new instance of the test fixture with a <see cref="ITestOutputHelper"/>.
        /// </summary>
        /// <param name="output">A test output log writer</param>
        protected LoFuTest(ITestOutputHelper output) => Output = output;

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>Override this method to change how the test setup is done.</remarks>
        public virtual async Task InitializeAsync() => await Task.CompletedTask;

        /// <summary>
        /// Runs the local functions in the containing test method that just executed.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>Override this method to change how the test cleanup is done.</remarks>
        public virtual async Task DisposeAsync() => await this.AssertAsync(Output as TestOutputHelper);

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