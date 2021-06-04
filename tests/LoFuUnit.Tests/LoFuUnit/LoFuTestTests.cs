using System;
using System.Linq;
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
        public void AssertAsync_object_MethodBase_throws_InconclusiveLoFuTestException()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsync));

            Func<Task> act = async () => { await fixture.AssertAsync(fixture, method); };
            act.Should().Throw<InconclusiveLoFuTestException>()
                .WithMessage($"*{nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsync)}*");
        }

        [Test]
        public void AssertAsync_object_MethodBase_throws_InconclusiveLoFuTestException_because_of_async_void()
        {
            var fixture = new FakeLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsyncVoid));

            Func<Task> act = async () => { await fixture.AssertAsync(fixture, method); };
            act.Should().Throw<InconclusiveLoFuTestException>()
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

        [Test]
        public void Assert_should_execute_test_functions_in_declaration_order()
        {
            var fixture = new FakeLoFuTestWithManyLocalFunctions();
            fixture.FakeTest();

            var names = Enumerable.Range(0, 200).Select(x => $"A{x.ToString().PadLeft(3, '0')}").ToArray();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTest), names);
        }

        [Test]
        public async Task AssertAsync_should_execute_test_functions_in_declaration_order()
        {
            var fixture = new FakeLoFuTestWithManyLocalFunctions();
            await fixture.FakeTestAsync();

            var names = Enumerable.Range(0, 200).Select(x => $"A{x.ToString().PadLeft(3, '0')}").ToArray();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestAsync), names);
        }

        [Test]
        public void Assert_throws_InconclusiveLoFuTestException_with_message_about_invalid_test_functions_in_declaration_order()
        {
            var fixture = new FakeLoFuTestWithManyLocalFunctions();
            
            var names = Enumerable.Range(0, 200).Select(x => $"Fail{x.ToString().PadLeft(3, '0')}").ToArray();

            fixture.Invoking(x => x.FakeTestThatThrowsInconclusiveLoFuTestException())
                .Should().Throw<InconclusiveLoFuTestException>()
                .WithMessage("*" + string.Join("*", names) + "*");
        }

        [Test]
        public void AssertAsync_throws_InconclusiveLoFuTestException_with_message_about_invalid_test_functions_in_declaration_order()
        {
            var fixture = new FakeLoFuTestWithManyLocalFunctions();

            var names = Enumerable.Range(0, 200).Select(x => $"Fail{x.ToString().PadLeft(3, '0')}").ToArray();

            Func<Task> act = async () => { await fixture.FakeTestThatThrowsInconclusiveLoFuTestExceptionAsync(); };
            act.Should().Throw<InconclusiveLoFuTestException>()
                .WithMessage("*" + string.Join("*", names) + "*");
        }
    }
}