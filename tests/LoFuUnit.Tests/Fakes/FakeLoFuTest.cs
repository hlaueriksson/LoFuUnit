using System.Threading.Tasks;

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

        public async Task FakeTestFailAsync()
        {
            await Task.CompletedTask;

            async void Fail() => await RecordAsync(); // Task
        }

        public void FakeTestWithNestedLocalFunctions()
        {
            void A()
            {
                var level = 0;

                Log($"\t\tLevel {level}");
                Record();

                B();

                void B()
                {
                    level++;

                    Log($"\t\tLevel {level}");
                    Record();

                    C($"\t\tLevel {++level}");

                    void C(string message)
                    {
                        Log(message);
                        Record();
                    }
                }
            }
        }
    }
}