using System.Diagnostics;
using System.Threading.Tasks;

namespace LoFuUnit
{
    /// <summary>
    /// Extension methods for test fixtures.
    /// </summary>
    public static class LoFuTestExtensions
    {
        /// <summary>
        /// Runs the local functions in the containing test method that invoked this extension method.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        public static void Assert(this object fixture)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(Configuration.StackTraceFrameIndexForAssert()).GetMethod();

            new InternalLoFuTest().Assert(fixture, method);
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this extension method.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(Configuration.StackTraceFrameIndexForAssertAsync()).GetMethod();

            await new InternalLoFuTest().AssertAsync(fixture, method).ConfigureAwait(false);
        }
    }
}