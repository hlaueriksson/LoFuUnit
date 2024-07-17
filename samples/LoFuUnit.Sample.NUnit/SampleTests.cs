using FluentAssertions;

namespace LoFuUnit.Sample.NUnit
{
    public class SampleTests
    {
        [LoFu, Test]
        public void passing_test()
        {
            void should_be_green() => (1 + 1).Should().Be(2);
        }

        [LoFuTest]
        public void passing_test_with_LoFuTest_attribute()
        {
            void should_be_green() => (1 + 1).Should().Be(2);
        }

        [LoFu, Test]
        public async Task passing_async_test()
        {
            await Task.CompletedTask;

            async Task should_be_green() => (await Task.FromResult(1 + 1)).Should().Be(2);
        }

        [LoFu, Test]
        public void failing_test()
        {
            void should_be_red() => (1 + 1).Should().Be(3);
        }

        [LoFu, Test]
        public void inconclusive_test()
        {
            var result = 1 + 1; // use a variable declared at test fixture scope, i.e. a field

            void should_be_yellow() => result.Should().Be(2);
        }
    }
}