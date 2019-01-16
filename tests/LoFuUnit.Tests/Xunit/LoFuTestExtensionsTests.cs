using System;
using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.Xunit.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.Xunit
{
    public class LoFuTestExtensionsTests
    {
        [Test]
        public void Assert_ITestOutputHelper()
        {
            var fixture = new FakeXunitLoFuTest();
            fixture.FakeTestWithITestOutputHelperExtension();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithITestOutputHelperExtension), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync_ITestOutputHelper()
        {
            var fixture = new FakeXunitLoFuTest();
            await fixture.FakeTestWithITestOutputHelperExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithITestOutputHelperExtensionAsync), "A", "B", "C");
        }

        [Test]
        public void Assert_TestOutputHelper()
        {
            var fixture = new FakeXunitLoFuTest();
            fixture.FakeTestWithTestOutputHelperExtension();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithTestOutputHelperExtension), "A", "B", "C");
        }

        [Test]
        public void Assert_TestOutputHelper_throws()
        {
            var fixture = new FakeXunitLoFuTest();

            fixture.Invoking(x => fixture.FakeTestWithTestOutputHelperExtensionFail())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Test method name from TestOutputHelper is unknown.");
        }

        [Test]
        public async Task AssertAsync_TestOutputHelper()
        {
            var fixture = new FakeXunitLoFuTest();
            await fixture.FakeTestWithTestOutputHelperExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithTestOutputHelperExtensionAsync), "A", "B", "C");
        }

        [Test]
        public void AssertAsync_TestOutputHelper_throws()
        {
            var fixture = new FakeXunitLoFuTest();

            fixture.Awaiting(async x => await fixture.FakeTestWithTestOutputHelperExtensionFailAsync())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Test method name from TestOutputHelper is unknown.");
        }
    }
}