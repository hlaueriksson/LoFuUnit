using System;
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
        public void AssertAsync_object_MethodBase_throws()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestFailAsync));

            fixture.Awaiting(async x => await x.AssertAsync(fixture, method))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Invocation of async local function failed.");
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

        [Test]
        public void Assert_with_nested_local_functions()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestWithNestedLocalFunctions));
            fixture.Assert(fixture, method);

            fixture.Out.ToString().ShouldMatch(nameof(fixture.FakeTestWithNestedLocalFunctions), "A", "Level 0", "Level 1", "Level 2");
            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithNestedLocalFunctions), "A", "B", "C");
        }
    }
}