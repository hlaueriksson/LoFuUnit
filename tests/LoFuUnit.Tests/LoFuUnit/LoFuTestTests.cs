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
        public void AssertAsync_object_MethodBase_throws_InvalidTestFunctionException()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInvalidTestFunctionExceptionAsync));

            fixture.Awaiting(async x => await x.AssertAsync(fixture, method))
                .Should().Throw<InvalidTestFunctionException>()
                .WithMessage("Invocation of test function *");
        }

        [Test]
        public void Assert_object_MethodBase_throws_InconclusiveTestMethodException()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveTestMethodException));

            fixture.Invoking(x => x.Assert(fixture, method))
                .Should().Throw<InconclusiveTestMethodException>()
                .WithMessage($"*{nameof(fixture.FakeTestThatThrowsInconclusiveTestMethodException)}*");
        }
        
        [Test]
        public async Task AssertAsync_object_MethodBase_throws_InconclusiveTestMethodException()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveTestMethodExceptionAsync));

            fixture.Awaiting(async x => await x.AssertAsync(fixture, method))
                .Should().Throw<InconclusiveTestMethodException>()
                .WithMessage($"*{nameof(fixture.FakeTestThatThrowsInconclusiveTestMethodExceptionAsync)}*");
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