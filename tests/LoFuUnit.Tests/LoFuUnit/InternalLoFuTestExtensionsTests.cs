using FluentAssertions;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit
{
    public class InternalLoFuTestExtensionsTests
    {
        [Test]
        public void Assert()
        {
            var fixture = new FakeLoFuTest();
            fixture.FakeTestWithInternalExtension();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithInternalExtension), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeLoFuTest();
            await fixture.FakeTestWithInternalExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithInternalExtensionAsync), "A", "B", "C");
        }

        [Test]
        public void IsAsync()
        {
            var fixture = new FakeLoFuTest();

            var method = fixture.GetType().GetMethod(nameof(fixture.FakeTest));
            method.IsAsync().Should().BeFalse();

            method = fixture.GetType().GetMethod(nameof(fixture.FakeTestAsync));
            method.IsAsync().Should().BeTrue();
        }
    }
}