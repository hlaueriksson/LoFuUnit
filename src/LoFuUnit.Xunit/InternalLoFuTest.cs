using Xunit.Abstractions;

namespace LoFuUnit.Xunit
{
    internal class InternalLoFuTest : LoFuTest
    {
        public InternalLoFuTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}