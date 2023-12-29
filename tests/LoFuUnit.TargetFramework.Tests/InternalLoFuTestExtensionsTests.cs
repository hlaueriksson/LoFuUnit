using NUnit.Framework;
using LoFuUnit.TargetFramework.Tests.Fakes;
using LoFuUnit.TargetFramework.Tests.Extensions;

namespace LoFuUnit.TargetFramework.Tests
{
    public class InternalLoFuTestExtensionsTests
    {
        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeLoFuTest();
            await fixture.FakeTestWithInternalExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithInternalExtensionAsync), "A", "B", "C");
        }
    }
}
