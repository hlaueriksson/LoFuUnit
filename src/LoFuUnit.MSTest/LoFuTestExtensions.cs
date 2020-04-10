using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFuUnit.MSTest
{
    /// <summary>
    /// Extension methods for test fixtures.
    /// </summary>
    public static class LoFuTestExtensions
    {
        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestContext"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="testContext">The current <see cref="TestContext"/></param>
        /// <remarks>Derives the test method from <c>testContext.TestName</c></remarks>
        public static void Assert(this object fixture, TestContext testContext)
        {
            var method = fixture.GetMethodInfo(testContext);

            try
            {
                new InternalLoFuTest().Assert(fixture, method);
            }
            catch (InconclusiveLoFuTestException e)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Inconclusive(e.Message);
            }
        }

        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestContext"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="testContext">The current <see cref="TestContext"/></param>
        /// <remarks>Derives the test method from <c>testContext.TestName</c></remarks>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, TestContext testContext)
        {
            var method = fixture.GetMethodInfo(testContext);

            try
            {
                await new InternalLoFuTest().AssertAsync(fixture, method).ConfigureAwait(false);
            }
            catch (InconclusiveLoFuTestException e)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Inconclusive(e.Message);
            }
        }

        internal static MethodInfo GetMethodInfo(this object fixture, TestContext testContext)
        {
            if (testContext == null) throw new InvalidOperationException("TestContext is null.");
            if (testContext.TestName == null) throw new InvalidOperationException("Test method name from TestContext is unknown.");

            var method = fixture.GetType().GetMethod(testContext.TestName);

            if (method == null) throw new InvalidOperationException("Test method not found on test fixture type.");

            return method;
        }
    }
}