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
        public async Task when_running_this_test_method()
        {
            await AssertAsync();

            void should_invoke_test_function_that_access_class_members() => (Subject + Subject).Should().Be(2);
            void should_invoke_test_function_that_use_local_variables() => (1 + 1).Should().Be(2);
        }

        int Subject;
    }
}