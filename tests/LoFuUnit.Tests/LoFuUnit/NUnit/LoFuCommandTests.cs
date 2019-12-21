using FluentAssertions;
using LoFuUnit.NUnit;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using ReflectionMagic;

namespace LoFuUnit.Tests.LoFuUnit.NUnit
{
    public class LoFuCommandTests
    {
        [Test]
        public void Assert()
        {
            var fixture = new FakeLoFuTest();

            var method = new TestMethod(new MethodWrapper(fixture.GetType(), nameof(fixture.FakeTest)));
            var context = GetContext(fixture, method);

            var command = new LoFuCommand(new EmptyTestCommand(method));
            command.Execute(context);

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTest), "A", "B", "C");
        }

        [Test]
        public void AssertAsync()
        {
            var fixture = new FakeLoFuTest();

            var method = new TestMethod(new MethodWrapper(fixture.GetType(), nameof(fixture.FakeTestAsync)));
            var context = GetContext(fixture, method);

            var command = new LoFuCommand(new EmptyTestCommand(method));
            command.Execute(context);

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestAsync), "A", "B", "C");
        }

        [Test]
        public void ResultState_Failure()
        {
            var fixture = new FakeLoFuTest();

            var method = new TestMethod(new MethodWrapper(fixture.GetType(), nameof(fixture.FakeTest)));
            var context = GetContext(fixture, method, ResultState.Failure);

            var command = new LoFuCommand(new EmptyTestCommand(method));
            command.Execute(context);

            fixture.Invocations.Should().BeEmpty();
        }

        private static TestExecutionContext GetContext(object fixture, TestMethod testMethod, ResultState result = null)
        {
            var testResult = new TestCaseResult(null);
            testResult.AsDynamic().ResultState = result ?? ResultState.Success;

            testMethod.Parent = new TestFixture(new TypeWrapper(fixture.GetType()))
            {
                Fixture = fixture
            };

            return new TestExecutionContext
            {
                CurrentResult = testResult,
                CurrentTest = testMethod
            };
        }
    }
}