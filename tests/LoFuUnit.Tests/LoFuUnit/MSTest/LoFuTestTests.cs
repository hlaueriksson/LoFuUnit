using FluentAssertions;
using LoFuUnit.Tests.LoFuUnit.MSTest.Fakes;
using LoFuUnit.Tests.LoFuUnit.Xunit.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit.MSTest
{
    public class LoFuTestTests
    {
        [Test]
        public async Task TestCleanup_with_sync_test_method()
        {
            var fixture = new FakeMSTestLoFuTestWithTestCleanup
            {
                TestContext = new FakeTestContext(nameof(FakeXunitLoFuTestWithIAsyncLifetime.FakeTest))
            };
            await fixture.TestCleanupAsync();

            fixture.TestFunctionWasCalled.Should().BeTrue();
        }

        [Test]
        public async Task TestCleanup_with_async_test_method()
        {
            var fixture = new FakeMSTestLoFuTestWithTestCleanup
            {
                TestContext = new FakeTestContext(nameof(FakeXunitLoFuTestWithIAsyncLifetime.FakeTestAsync))
            };
            await fixture.TestCleanupAsync();

            fixture.TestFunctionWasCalled.Should().BeTrue();
        }
    }
}