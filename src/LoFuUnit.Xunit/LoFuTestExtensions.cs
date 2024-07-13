using System.Reflection;
using System.Runtime.CompilerServices;
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
        /// <param name="output">A test output log writer.</param>
        /// <param name="callerMemberName">The test method name. The caller of this method will implicitly be used, so don't set this parameter explicitly.</param>
        public static void Assert(this object fixture, ITestOutputHelper output, [CallerMemberName] string callerMemberName = "")
        {
            new InternalLoFuTest(output).Assert(fixture, fixture.GetAssertTestMethod(callerMemberName));
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this extension method.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="output">A test output log writer.</param>
        /// <param name="callerMemberName">The test method name. The caller of this method will implicitly be used, so don't set this parameter explicitly.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, ITestOutputHelper output, [CallerMemberName] string callerMemberName = "")
        {
            await new InternalLoFuTest(output).AssertAsync(fixture, fixture.GetAssertAsyncTestMethod(callerMemberName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestOutputHelper"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="output">A <see cref="TestOutputHelper"/>.</param>
        /// <remarks>Derives the test method via reflection from the private <c>output.test</c> field.</remarks>
        public static void Assert(this object fixture, TestOutputHelper output)
        {
            if (fixture == null) throw new ArgumentNullException(nameof(fixture));

            var method = fixture.GetMethodInfo(output);

            new InternalLoFuTest(output).Assert(fixture, method);
        }

        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestOutputHelper"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="output">A <see cref="TestOutputHelper"/>.</param>
        /// <remarks>Derives the test method via reflection from the private <c>output.test</c> field.</remarks>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, TestOutputHelper output)
        {
            if (fixture == null) throw new ArgumentNullException(nameof(fixture));

            var method = fixture.GetMethodInfo(output);

            await new InternalLoFuTest(output).AssertAsync(fixture, method).ConfigureAwait(false);
        }

        internal static MethodInfo GetMethodInfo(this object fixture, TestOutputHelper output)
        {
            if (output == null) throw new InvalidOperationException("TestOutputHelper is null.");

            var test = output.GetType().GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(output) as ITest;
            var methodName = test?.TestCase?.TestMethod?.Method?.Name ?? throw new InvalidOperationException("Test method name from TestOutputHelper is unknown.");

            return fixture.GetType().GetMethod(methodName) ?? throw new InvalidOperationException("Test method not found on test fixture type.");
        }
    }
}
