using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit
{
    public class LoFuTestExtensionsTests
    {
        [Test]
        public void Assert()
        {
            var fixture = new FakeLoFuTest();
            fixture.FakeTestWithExtension();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtension), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeLoFuTest();
            await fixture.FakeTestWithExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtensionAsync), "A", "B", "C");
        }
    }
}