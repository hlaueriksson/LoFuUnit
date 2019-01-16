using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Xunit
{
    public static class LoFuTestExtensions
    {
        public static void Assert(this object fixture, ITestOutputHelper output)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            new InternalLoFuTest(output).Assert(fixture, method);
        }

        public static async Task AssertAsync(this object fixture, ITestOutputHelper output)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(5).GetMethod();

            await new InternalLoFuTest(output).AssertAsync(fixture, method).ConfigureAwait(false);
        }

        public static void Assert(this object fixture, TestOutputHelper output)
        {
            var test = output.GetType().GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(output) as ITest;
            var methodName = test?.TestCase.TestMethod.Method.Name;

            if (methodName == null) throw new InvalidOperationException("Test method name from TestOutputHelper is unknown.");

            var method = fixture.GetType().GetMethod(methodName);

            new InternalLoFuTest(output).Assert(fixture, method);
        }

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