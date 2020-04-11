using System.Threading.Tasks;
using MSTestLoFuTest = LoFuUnit.MSTest.LoFuTest;

namespace LoFuUnit.Tests.LoFuUnit.MSTest.Fakes
{
    public class FakeMSTestLoFuTestWithTestCleanup : MSTestLoFuTest
    {
        public bool TestFunctionWasCalled { get; private set; }

        public void FakeTest()
        {
            void A() => TestFunctionWasCalled = true;
        }

        public async Task FakeTestAsync()
        {
            await Task.CompletedTask;

            async Task A() => TestFunctionWasCalled = await Task.FromResult(true);
        }
    }
}