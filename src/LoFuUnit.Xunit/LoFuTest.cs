using Xunit.Abstractions;

namespace LoFuUnit.Xunit
{
    public abstract class LoFuTest : LoFuUnit.LoFuTest
    {
        protected ITestOutputHelper Output { get; }

        protected LoFuTest(ITestOutputHelper output)
        {
            Output = output;
        }

        protected override void Log(string message)
        {
            Output.WriteLine(message);
        }
    }
}