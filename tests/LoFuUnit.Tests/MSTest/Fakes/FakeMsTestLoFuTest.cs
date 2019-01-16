using System.Threading.Tasks;
using LoFoUnit.MSTest;
using LoFuUnit.Tests.Fakes;

namespace LoFuUnit.Tests.MSTest.Fakes
{
    public class FakeMsTestLoFuTest : FakeLoFuTestBase
    {
        public void FakeTestWithExtension()
        {
            this.Assert(new FakeTestContext(nameof(FakeTestWithExtension)));

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestWithExtensionAsync()
        {
            await this.AssertAsync(new FakeTestContext(nameof(FakeTestWithExtensionAsync)));

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }
    }
}