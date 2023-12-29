using FluentAssertions;
using LoFuUnit.MSTest;
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
        public void AssertAsync_InconclusiveLoFuTestException()
        {
            var fixture = new FakeMsTestLoFuTest();

            Func<Task> act = async () => { await fixture.FakeTestWithExtensionThatThrowsInconclusiveLoFuTestExceptionAsync(); };
            act.Should().ThrowAsync<AssertInconclusiveException>();
        }

        [Test]
        public void GetMethodInfo()
        {
            var fixture = new FakeMsTestLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestWithExtension));

            var result = fixture.GetMethodInfo(new FakeTestContext(method.Name));

            result.Should().BeSameAs(method);
        }

        [Test]
        public void GetMethodInfo_throws()
        {
            var fixture = new FakeMsTestLoFuTest();

            fixture.Invoking(x => x.GetMethodInfo(null))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("TestContext is null.");

            fixture.Invoking(x => x.GetMethodInfo(new FakeTestContext(null)))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Test method name from TestContext is unknown.");

            fixture.Invoking(x => x.GetMethodInfo(new FakeTestContext("")))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Test method not found on test fixture type.");
        }
    }
}