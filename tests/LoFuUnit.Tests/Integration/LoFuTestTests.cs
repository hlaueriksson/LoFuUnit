using System;
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
        public void when_Assert_with_nested_local_functions()
        {
            Assert();

            void should_invoke_nested_local_functions_that_return_non_void()
            {
                var result = add(1, 1);
                result.Should().Be(2);

                int add(int a, int b) => a + b;
            }
        }

        [Test]
        public void when_Assert_with_Action()
        {
            Assert();

            void should_invoke_local_function_with_Action_expression()
            {
                var subject = new FailSubject();

                Action act = subject.Fail;
                act.Should().Throw<Exception>();
            }
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

        [Test]
        public async Task when_AssertAsync_with_nested_local_functions()
        {
            await AssertAsync();

            async Task should_invoke_nested_local_functions_that_return_Task_TResult()
            {
                var delay = await add(1, 1);
                await Task.Delay(delay);

                async Task<int> add(int a, int b) => await Task.FromResult(a + b);
            }

            void should_invoke_nested_local_functions_that_return_non_void()
            {
                var result = add(1, 1);
                result.Should().Be(2);

                int add(int a, int b) => a + b;
            }
        }

        [Test]
        public async Task when_AssertAsync_with_Func_Task()
        {
            await AssertAsync();

            async Task should_invoke_local_function_with_Func_Task_expression()
            {
                var subject = new FailSubject();

                Func<Task> act = subject.FailAsync;
                await act.Should().ThrowAsync<Exception>();
            }
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
