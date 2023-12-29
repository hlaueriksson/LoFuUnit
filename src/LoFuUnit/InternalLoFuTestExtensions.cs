using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LoFuUnit
{
    internal static class InternalLoFuTestExtensions
    {
        internal static MethodBase GetTestMethodForAssert(this object fixture, string callerMemberName)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(2).GetMethod();

            if (!method.Name.Contains(callerMemberName)) throw new InvalidOperationException($"Test method '{callerMemberName}' not found in StackTrace.");

            return method;
        }

#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods
        internal static MethodBase GetTestMethodForAssertAsync(this object fixture, string callerMemberName)
#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
        {
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

#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods
        internal static bool IsAsync(this MethodInfo method)
#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
        {
            return method.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
        }
    }
}
