using System.Threading.Tasks;
using Xunit.Abstractions;
using XunitLoFuTest = LoFuUnit.Xunit.LoFuTest;

namespace LoFuUnit.Tests.LoFuUnit.Xunit.Fakes
{
    public class FakeXunitLoFuTestWithIAsyncLifetime : XunitLoFuTest
    {
        public bool TestFunctionWasCalled { get; private set; }

        public FakeXunitLoFuTestWithIAsyncLifetime(ITestOutputHelper output) : base(output) { }

        public void FakeTest()
        {
            void A() => TestFunctionWasCalled = true;
        }

        public async Task FakeTestAsync()
        {
            async Task A() => TestFunctionWasCalled = await Task.FromResult(true);
        }
    }
}