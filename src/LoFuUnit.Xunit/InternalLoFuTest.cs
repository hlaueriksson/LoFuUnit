using Xunit.Abstractions;

namespace LoFuUnit.Xunit
{
    internal class InternalLoFuTest(ITestOutputHelper output) : LoFuTest(output)
    {
    }
}
