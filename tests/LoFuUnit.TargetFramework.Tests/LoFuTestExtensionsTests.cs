using NUnit.Framework;
using LoFuUnit.TargetFramework.Tests.Fakes;
using LoFuUnit.TargetFramework.Tests.Extensions;

namespace LoFuUnit.TargetFramework.Tests
{
    public class LoFuTestExtensionsTests
    {
        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeLoFuTest();
            await fixture.FakeTestWithExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtensionAsync), "A", "B", "C");
        }
    }
}
