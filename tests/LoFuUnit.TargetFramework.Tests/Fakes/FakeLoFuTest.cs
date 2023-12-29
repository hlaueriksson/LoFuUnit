namespace LoFuUnit.TargetFramework.Tests.Fakes
{
    public class FakeLoFuTest : FakeLoFuTestBase
    {
        public async Task FakeTestAsync()
        {
            await Task.CompletedTask;

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public async Task FakeTestWithAssertAsync()
        {
            await AssertAsync();

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public async Task FakeTestWithExtensionAsync()
        {
            await LoFuTestExtensions.AssertAsync(this);

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public async Task FakeTestWithInternalExtensionAsync()
        {
            await this.AssertAsync(GetType().GetMethod(nameof(FakeTestWithInternalExtensionAsync)));

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }
    }
}
