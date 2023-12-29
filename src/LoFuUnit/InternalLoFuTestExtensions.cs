using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LoFuUnit
{
    internal static class InternalLoFuTestExtensions
    {
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
