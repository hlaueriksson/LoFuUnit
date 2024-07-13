using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LoFuUnit
{
    internal static class InternalLoFuTestExtensions
    {
        internal static MethodBase GetAssertTestMethod(this object fixture, string callerMemberName)
        {
            if (!fixture.HasMethod(callerMemberName)) throw new InvalidOperationException($"Test method '{callerMemberName}' not found in Fixture {fixture}.");

            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(2).GetMethod();

            if (!method.Name.Contains(callerMemberName)) throw new InvalidOperationException($"Test method '{callerMemberName}' not found in StackTrace.");

            return method;
        }

        internal static MethodBase GetAssertAsyncTestMethod(this object fixture, string callerMemberName)
        {
            if (!fixture.HasMethod(callerMemberName)) throw new InvalidOperationException($"Test method '{callerMemberName}' not found in Fixture {fixture}.");

            var stackTrace = new StackTrace();

            for (int i = 6; i <= 8; i++)
            {
                var method = stackTrace.GetFrame(i).GetMethod();

                if (method.Name.Contains(callerMemberName))
                {
                    return method;
                }
            }

            throw new InvalidOperationException($"Test method '{callerMemberName}' not found in StackTrace.");
        }

        internal static void Assert(this object fixture, MethodBase method)
        {
            new InternalLoFuTest().Assert(fixture, method);
        }

        internal static async Task AssertAsync(this object fixture, MethodBase method)
        {
            await new InternalLoFuTest().AssertAsync(fixture, method).ConfigureAwait(false);
        }

        internal static bool IsAsyncMethod(this MethodInfo method)
        {
            return method.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
        }

        internal static bool HasMethod(this object fixture, string callerMemberName)
        {
            return fixture.GetType().GetMethod(callerMemberName) != null;
        }
    }
}
