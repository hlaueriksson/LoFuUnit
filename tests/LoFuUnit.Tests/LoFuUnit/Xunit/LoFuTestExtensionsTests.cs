using System;
using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Xunit;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.LoFuUnit.Xunit.Fakes;
using NSubstitute;
using NUnit.Framework;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Tests.LoFuUnit.Xunit
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

            fixture.Invoking(x => x.FakeTestWithTestOutputHelperExtensionFail())
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

            Func<Task> act = async () => { await fixture.FakeTestWithTestOutputHelperExtensionFailAsync(); };
            act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Test method name from TestOutputHelper is unknown.");
        }

        [Test]
        public void GetMethodInfo() 
        {
            var fixture = new FakeXunitLoFuTest();
            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTestWithITestOutputHelperExtension));
            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns(method.Name);
            var testOutputHelper = new TestOutputHelper();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);

            var result = fixture.GetMethodInfo(testOutputHelper);

            result.Should().BeSameAs(method);
        }

        [Test]
        public void GetMethodInfo_throws() 
        {
            var fixture = new FakeXunitLoFuTest();

            fixture.Invoking(x => x.GetMethodInfo(null))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("TestOutputHelper is null.");

            var testOutputHelper = new TestOutputHelper();
            var test = Substitute.For<ITest>();
            test.TestCase.TestMethod.Method.Name.Returns((string) null);
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);
            fixture.Invoking(x => x.GetMethodInfo(testOutputHelper))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Test method name from TestOutputHelper is unknown.");

            testOutputHelper = new TestOutputHelper();
            test = Substitute.For<ITest>();
            testOutputHelper.Initialize(Substitute.For<IMessageBus>(), test);
            fixture.Invoking(x => x.GetMethodInfo(testOutputHelper))
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Test method not found on test fixture type.");
        }
    }
}