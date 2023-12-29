using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFuUnit.MSTest
{
    /// <summary>
    /// Base class for <c>MSTest</c> test fixtures.
    /// </summary>
    public abstract class LoFuTest : LoFuUnit.LoFuTest
    {
        /// <summary>
        /// Stores information that is provided to unit tests.
        /// </summary>
        public TestContext? TestContext { get; set; }

        /// <summary>
        /// Runs the local functions in the containing test method that just executed.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>Override this method to change how the test cleanup is done.</remarks>
        [TestCleanup]
        public virtual async Task TestCleanupAsync()
        {
            if (IsAsync())
            {
                await this.AssertAsync(TestContext!).ConfigureAwait(false);
            }
            else
            {
#pragma warning disable VSTHRD103 // Call async methods when in an async method
                this.Assert(TestContext!);
#pragma warning restore VSTHRD103 // Call async methods when in an async method
            }

            bool IsAsync()
            {
                return this.GetMethodInfo(TestContext!).IsAsyncMethod();
            }
        }
    }
}
