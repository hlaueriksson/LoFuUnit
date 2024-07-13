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
        /// Initializes a new instance of the <see cref="LoFuTest"/> class,
        /// with a <see cref="ITestOutputHelper"/>.
        /// </summary>
        /// <param name="output">A test output log writer.</param>
        protected LoFuTest(ITestOutputHelper output) => Output = output;

        /// <summary>
        /// Test output log writer.
        /// </summary>
        protected ITestOutputHelper Output { get; }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>Override this method to change how the test setup is done.</remarks>
        public virtual async Task InitializeAsync() => await Task.CompletedTask.ConfigureAwait(false);

        /// <summary>
        /// Runs the local functions in the containing test method that just executed.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>Override this method to change how the test cleanup is done.</remarks>
        public virtual async Task DisposeAsync()
        {
            if (IsAsyncMethod())
            {
                await this.AssertAsync((TestOutputHelper)Output).ConfigureAwait(false);
            }
            else
            {
#pragma warning disable VSTHRD103 // Call async methods when in an async method
                this.Assert((TestOutputHelper)Output);
#pragma warning restore VSTHRD103 // Call async methods when in an async method
            }

            bool IsAsyncMethod()
            {
                return this.GetMethodInfo((TestOutputHelper)Output).IsAsyncMethod();
            }
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
