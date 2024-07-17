using FluentAssertions;

namespace LoFuUnit.Sample.MSTest
{
    [TestClass]
    public class SampleTestsWithTestCleanup
    {
        public TestContext TestContext { get; set; }

        [TestCleanup]
        public async Task TestCleanup() => await this.AssertAsync(TestContext);

        [TestMethod]
        public void passing_test()
        {
            void should_be_green() => (1 + 1).Should().Be(2);
        }

        [TestMethod]
        public async Task passing_async_test()
        {
            await Task.CompletedTask;

            async Task should_be_green() => (await Task.FromResult(1 + 1)).Should().Be(2);
        }

        [TestMethod]
        public void failing_test()
        {
            void should_be_red() => (1 + 1).Should().Be(3);
        }

        [TestMethod]
        public void inconclusive_test()
        {
            var result = 1 + 1; // use a variable declared at test fixture scope, i.e. a field

            void should_be_yellow() => result.Should().Be(2);
        }
    }
}