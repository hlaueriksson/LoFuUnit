using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Tests.Integration
{
    public class LoFuTestTests : LoFuTest
    {
        [SetUp]
        public void SetUp()
        {
            Subject = 1;
        }

        [Test]
        public void when_Assert()
        {
            Assert();

            void should_invoke_test_function_that_access_class_members() => (Subject + Subject).Should().Be(2);
            void should_invoke_test_function_that_use_local_variables() => (1 + 1).Should().Be(2);
        }

        [Test]
        public async Task when_AssertAsync()
        {
            await AssertAsync();

            async Task should_invoke_async_test_function_that_access_class_members()
            {
                var delay = Subject + Subject;
                await Task.Delay(delay);
                delay.Should().Be(2);
            }

            async Task should_invoke_async_test_function_that_use_local_variables()
            {
                var delay = 1 + 1;
                await Task.Delay(delay);
                delay.Should().Be(2);
            }

            void should_invoke_test_function_that_access_class_members() => (Subject + Subject).Should().Be(2);
            void should_invoke_test_function_that_use_local_variables() => (1 + 1).Should().Be(2);
        }

        [Test, Ignore("InconclusiveLoFuTestException")]
        public void when_Assert_on_inconclusive_test_method()
        {
            var result = 1 + 1;

            Assert();

            void should_not_invoke_test_function_that_access_test_method_variables() => result.Should().Be(2);
            void should_invoke_test_function_that_access_class_members() => (Subject + Subject).Should().Be(2);
            void should_invoke_test_function_that_use_local_variables() => (1 + 1).Should().Be(2);
        }

        [Test, Ignore("InconclusiveLoFuTestException")]
        public async Task when_AssertAsync_on_inconclusive_test_method()
        {
            var result = 1 + 1;

            await AssertAsync();

            async Task should_not_invoke_async_test_function_that_access_test_method_variables()
            {
                await Task.Delay(result);
                result.Should().Be(2);
            }

            async Task should_invoke_async_test_function_that_access_class_members()
            {
                var delay = Subject + Subject;
                await Task.Delay(delay);
                delay.Should().Be(2);
            }

            async Task should_invoke_async_test_function_that_use_local_variables()
            {
                var delay = 1 + 1;
                await Task.Delay(delay);
                delay.Should().Be(2);
            }

            void should_not_invoke_test_function_that_access_test_method_variables() => result.Should().Be(2);
            void should_invoke_test_function_that_access_class_members() => (Subject + Subject).Should().Be(2);
            void should_invoke_test_function_that_use_local_variables() => (1 + 1).Should().Be(2);
        }

        [Test, Ignore("InconclusiveLoFuTestException")]
        public async Task when_AssertAsync_on_invalid_test_function()
        {
            await AssertAsync();

            async void should_not_invoke_async_test_function_that_returns_void()
            {
                await Task.CompletedTask;
            }
        }

        int Subject;
    }
}