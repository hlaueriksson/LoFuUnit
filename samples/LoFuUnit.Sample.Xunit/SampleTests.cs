using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace LoFuUnit.Sample.Xunit
{
    public class SampleTests : LoFuUnit.Xunit.LoFuTest
    {
        public SampleTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void passing_test()
        {
            void should_be_green() => (1 + 1).Should().Be(2);
        }

        [Fact]
        public async Task passing_async_test()
        {
            await Task.CompletedTask;

            async Task should_be_green() => (await Task.FromResult(1 + 1)).Should().Be(2);
        }

        [Fact]
        public void failing_test()
        {
            void should_be_red() => (1 + 1).Should().Be(3);
        }

        [Fact]
        public void inconclusive_test()
        {
            var result = 1 + 1; // use a variable declared at test fixture scope, i.e. a field

            void should_be_yellow() => result.Should().Be(2);
        }
    }
}