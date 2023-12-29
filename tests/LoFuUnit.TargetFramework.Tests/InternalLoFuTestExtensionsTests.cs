using LoFuUnit.TargetFramework.Tests.Extensions;
using LoFuUnit.TargetFramework.Tests.Fakes;
using NUnit.Framework;

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
