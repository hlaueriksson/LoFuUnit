using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit
{
    public class LoFuTestTests
    {
        [Test]
        public void Assert_object_MethodBase()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTest));
            fixture.Assert(fixture, method);

            fixture.Out.ToString().ShouldMatch(nameof(fixture.FakeTest), "A", "B", "C");
            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTest), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync_object_MethodBase()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestAsync));
            await fixture.AssertAsync(fixture, method);

            fixture.Out.ToString().ShouldMatch(nameof(fixture.FakeTestAsync), "A", "B", "C");
            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestAsync), "A", "B", "C");
        }

        [Test]
        public void Assert_object_MethodBase_throws_InconclusiveLoFuTestException()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestException));

            fixture.Invoking(x => x.Assert(fixture, method))
                .Should().Throw<InconclusiveLoFuTestException>()
                .WithMessage($"*{nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestException)}*");
        }

        [Test]
        public async Task AssertAsync_object_MethodBase_throws_InconclusiveLoFuTestException()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsync));

            fixture.Awaiting(async x => await x.AssertAsync(fixture, method))
                .Should().Throw<InconclusiveLoFuTestException>()
                .WithMessage($"*{nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsync)}*");
        }

        [Test]
        public void AssertAsync_object_MethodBase_throws_InconclusiveLoFuTestException_because_of_async_void()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsyncVoid));

            fixture.Awaiting(async x => await x.AssertAsync(fixture, method))
                .Should().Throw<InconclusiveLoFuTestException>()
                .WithMessage("Invocation of test function *");
        }

        [Test]
        public void Assert()
        {
            var fixture = new FakeLoFuTest();
            fixture.FakeTestWithAssert();

            fixture.Out.ToString().ShouldMatch(nameof(fixture.FakeTestWithAssert), "A", "B", "C");
            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithAssert), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeLoFuTest();
            await fixture.FakeTestWithAssertAsync();

            fixture.Out.ToString().ShouldMatch(nameof(fixture.FakeTestWithAssertAsync), "A", "B", "C");
            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithAssertAsync), "A", "B", "C");
        }
    }
}