using System.Threading.Tasks;
using FluentAssertions;

namespace LoFuUnit.Tests.Fakes
{
    public class FakeLoFuTest : FakeLoFuTestBase
    {
        public void FakeTest()
        {
            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestAsync()
        {
            await Task.CompletedTask;

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public void FakeTestWithAssert()
        {
            Assert();

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestWithAssertAsync()
        {
            await AssertAsync();

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public void FakeTestWithExtension()
        {
            LoFuTestExtensions.Assert(this);

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestWithExtensionAsync()
        {
            await LoFuTestExtensions.AssertAsync(this);

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public void FakeTestWithInternalExtension()
        {
            this.Assert(GetType().GetMethod(nameof(FakeTestWithInternalExtension)));

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestWithInternalExtensionAsync()
        {
            await this.AssertAsync(GetType().GetMethod(nameof(FakeTestWithInternalExtensionAsync)));

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public async Task FakeTestThatThrowsInvalidTestFunctionExceptionAsync()
        {
            await Task.CompletedTask;

            async void Fail() => await RecordAsync(); // should return Task
        }

        public void FakeTestThatThrowsInconclusiveTestMethodException()
        {
            var result = 1 + 1;

            void Fail() => result.Should().Be(2); // should not access test method variables
        }

        public async Task FakeTestThatThrowsInconclusiveTestMethodExceptionAsync()
        {
            var result = 1 + 1;
            await Task.CompletedTask;

            async Task Fail() => await Task.Delay(result); // should not access test method variables
        }
    }
}