using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Xunit
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
        /// <param name="output">A test output log writer</param>
        public static void Assert(this object fixture, ITestOutputHelper output)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            new InternalLoFuTest(output).Assert(fixture, method);
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this extension method.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="output">A test output log writer</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, ITestOutputHelper output)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(5).GetMethod();

            await new InternalLoFuTest(output).AssertAsync(fixture, method).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestOutputHelper"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="output">A <see cref="TestOutputHelper"/></param>
        /// <remarks>Derives the test method via reflection from the private <c>output.test</c> field</remarks>
        public static void Assert(this object fixture, TestOutputHelper output)
        {
            var test = output.GetType().GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(output) as ITest;
            var methodName = test?.TestCase.TestMethod.Method.Name;

            if (methodName == null) throw new InvalidOperationException("Test method name from TestOutputHelper is unknown.");

            var method = fixture.GetType().GetMethod(methodName);

            new InternalLoFuTest(output).Assert(fixture, method);
        }

        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestOutputHelper"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="output">A <see cref="TestOutputHelper"/></param>
        /// <remarks>Derives the test method via reflection from the private <c>output.test</c> field</remarks>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, TestOutputHelper output)
        {
            var test = output.GetType().GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(output) as ITest;
            var methodName = test?.TestCase?.TestMethod?.Method?.Name;

            if (methodName == null) throw new InvalidOperationException("Test method name from TestOutputHelper is unknown.");

            var method = fixture.GetType().GetMethod(methodName);

            await new InternalLoFuTest(output).AssertAsync(fixture, method).ConfigureAwait(false);
        }
    }
}