using LoFuUnit.TargetFramework.Tests.Extensions;
using LoFuUnit.TargetFramework.Tests.Fakes;
using NUnit.Framework;

namespace LoFuUnit.TargetFramework.Tests
{
    public class LoFuTestTests
    {
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
        public async Task AssertAsync()
        {
            var fixture = new FakeLoFuTest();
            await fixture.FakeTestWithAssertAsync();

            fixture.Out.ToString().ShouldMatch(nameof(fixture.FakeTestWithAssertAsync), "A", "B", "C");
            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestWithAssertAsync), "A", "B", "C");
        }

        [Test]
        public async Task AssertAsync_should_execute_test_functions_in_declaration_order()
        {
            var fixture = new FakeLoFuTestWithManyLocalFunctions();
            await fixture.FakeTestAsync();

            var names = Enumerable.Range(0, 200).Select(x => $"A{x.ToString().PadLeft(3, '0')}").ToArray();

            fixture.Invocations.ShouldMatch(nameof(fixture.FakeTestAsync), names);
        }
    }
}
