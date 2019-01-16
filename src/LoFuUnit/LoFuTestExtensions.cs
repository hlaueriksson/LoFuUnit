using System.Diagnostics;
using System.Threading.Tasks;

namespace LoFuUnit
{
    public static class LoFuTestExtensions
    {
        public static void Assert(this object fixture)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            new InternalLoFuTest().Assert(fixture, method);
        }

        public static async Task AssertAsync(this object fixture)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(5).GetMethod();

            await new InternalLoFuTest().AssertAsync(fixture, method).ConfigureAwait(false);
        }
    }
}