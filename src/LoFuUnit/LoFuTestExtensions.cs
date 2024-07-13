using System.Runtime.CompilerServices;

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
        /// <param name="callerMemberName">The test method name. The caller of this method will implicitly be used, so don't set this parameter explicitly.</param>
        public static void Assert(this object fixture, [CallerMemberName] string callerMemberName = "")
        {
            new InternalLoFuTest().Assert(fixture, fixture.GetAssertTestMethod(callerMemberName));
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this extension method.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="callerMemberName">The test method name. The caller of this method will implicitly be used, so don't set this parameter explicitly.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, [CallerMemberName] string callerMemberName = "")
        {
            await new InternalLoFuTest().AssertAsync(fixture, fixture.GetAssertAsyncTestMethod(callerMemberName)).ConfigureAwait(false);
        }
    }
}
