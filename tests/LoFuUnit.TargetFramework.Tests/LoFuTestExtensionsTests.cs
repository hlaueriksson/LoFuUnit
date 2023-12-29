using LoFuUnit.TargetFramework.Tests.Extensions;
using LoFuUnit.TargetFramework.Tests.Fakes;
using NUnit.Framework;

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
