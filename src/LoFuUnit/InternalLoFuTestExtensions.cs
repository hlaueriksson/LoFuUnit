using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LoFoUnit.MSTest"),
           InternalsVisibleTo("LoFuUnit.NUnit"),
           InternalsVisibleTo("LoFuUnit.Xunit"),
           InternalsVisibleTo("LoFuUnit.Tests")]

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
    }
}