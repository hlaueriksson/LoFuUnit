using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.LoFuUnit.MSTest.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit.MSTest
{
    public class LoFuTestExtensionsTests
    {
        [Test]
        public void Assert()
        {
            var fixture = new FakeMsTestLoFuTest();
            fixture.FakeTestWithExtension();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtension), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeMsTestLoFuTest();
            await fixture.FakeTestWithExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtensionAsync), "A", "B", "C");
        }

        [Test]
        public void Assert_InconclusiveLoFuTestException()
        {
            var fixture = new FakeMsTestLoFuTest();

            fixture.Invoking(x => x.FakeTestWithExtensionThatThrowsInconclusiveLoFuTestException())
                .Should().Throw<AssertInconclusiveException>();
        }

        [Test]
        public async Task AssertAsync_InconclusiveLoFuTestException()
        {
            var fixture = new FakeMsTestLoFuTest();

            fixture.Awaiting(async x => await x.FakeTestWithExtensionThatThrowsInconclusiveLoFuTestExceptionAsync())
                .Should().Throw<AssertInconclusiveException>();
        }
    }
}