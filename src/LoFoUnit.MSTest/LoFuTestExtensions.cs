using System.Threading.Tasks;
using LoFuUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFoUnit.MSTest
{
    public static class LoFuTestExtensions
    {
        public static void Assert(this object fixture, TestContext testContext)
        {
            var method = fixture.GetType().GetMethod(testContext.TestName);

            new InternalLoFuTest().Assert(fixture, method);
        }

        public static async Task AssertAsync(this object fixture, TestContext testContext)
        {
            var method = fixture.GetType().GetMethod(testContext.TestName);

            await new InternalLoFuTest().AssertAsync(fixture, method).ConfigureAwait(false);
        }
    }
}