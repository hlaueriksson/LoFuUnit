using System.Threading.Tasks;
using LoFuUnit.Tests.Fakes;
using LoFuUnit.Xunit;
using NSubstitute;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Tests.LoFuUnit.Xunit.Fakes
{
    public class FakeXunitLoFuTest : FakeLoFuTestBase
    {
        public void FakeTestWithITestOutputHelperExtension()
        {
            this.Assert(new FakeTestOutputHelper(Out));

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestWithITestOutputHelperExtensionAsync()
        {
            await this.AssertAsync(new FakeTestOutputHelper(Out));

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public void FakeTestWithTestOutputHelperExtension()
        {
            var method = GetType().GetMethod(nameof(FakeTestWithTestOutputHelperExtension));

            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns(method.Name);

            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            this.Assert(testOutputHelper);

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public void FakeTestWithTestOutputHelperExtensionFail()
        {
            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns((string)null);

            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            this.Assert(testOutputHelper);

            void A() => Record();
            void B() => Record();
            void C() => Record();
        }

        public async Task FakeTestWithTestOutputHelperExtensionAsync()
        {
            var method = GetType().GetMethod(nameof(FakeTestWithTestOutputHelperExtensionAsync));

            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns(method.Name);

            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            await this.AssertAsync(testOutputHelper);

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }

        public async Task FakeTestWithTestOutputHelperExtensionFailAsync()
        {
            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns((string)null);

            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            await this.AssertAsync(testOutputHelper);

            async Task A() => await RecordAsync();
            void B() => Record();
            async Task C() => await RecordAsync();
        }
    }
}