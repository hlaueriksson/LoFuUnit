using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Tests.LoFuUnit.Xunit.Fakes;
using NSubstitute;
using NUnit.Framework;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Tests.LoFuUnit.Xunit
{
    public class LoFuTestTests
    {
        [Test]
        public async Task InitializeAsync()
        {
            var fixture = new FakeXunitLoFuTestWithIAsyncLifetime(new TestOutputHelper());
            await fixture.InitializeAsync();
        }
        
        [Test]
        public async Task DisposeAsync_with_sync_test_method()
        {
            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns(nameof(FakeXunitLoFuTestWithIAsyncLifetime.FakeTest));
            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            var fixture = new FakeXunitLoFuTestWithIAsyncLifetime(testOutputHelper);
            await fixture.DisposeAsync();

            fixture.TestFunctionWasCalled.Should().BeTrue();
        }
        
        [Test]
        public async Task DisposeAsync_with_async_test_method()
        {
            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns(nameof(FakeXunitLoFuTestWithIAsyncLifetime.FakeTestAsync));
            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            var fixture = new FakeXunitLoFuTestWithIAsyncLifetime(testOutputHelper);
            await fixture.DisposeAsync();

            fixture.TestFunctionWasCalled.Should().BeTrue();
        }
    }
}