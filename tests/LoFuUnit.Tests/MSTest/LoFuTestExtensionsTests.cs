using System.Threading.Tasks;
using LoFuUnit.Tests.Extensions;
using LoFuUnit.Tests.MSTest.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.MSTest
{
    public class LoFuTestExtensionsTests
    {
        [Test]
        public void Assert()
        {
            var fixture = new FakeMsTestLoFuTest();
            fixture.FakeTestWithExtension();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtension), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync()
        {
            var fixture = new FakeMsTestLoFuTest();
            await fixture.FakeTestWithExtensionAsync();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithExtensionAsync), "A", "B", "C");
        }
    }
}