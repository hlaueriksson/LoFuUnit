using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Tests.Integration
{
    public class LoFuTestRegressionTests : LoFuTest
    {
        [SetUp]
        public void SetUp()
        {
            Subject = new FailSubject();
        }

        // Message:
        //  System.ArgumentOutOfRangeException : StartIndex cannot be less than zero. (Parameter 'startIndex')
        // Stack Trace:
        //  String.Remove(Int32 startIndex)
        //  InternalNamingExtensions.GetFunctionName(MethodInfo testFunction, MethodBase testMethod)
        //  LoFuTest.AssertAsync(Object testFixture, MethodBase testMethod)
        //  InternalLoFuTestExtensions.AssertAsync(Object fixture, MethodBase method)
        //  <.ctor>b__0_0(TestExecutionContext context)
        //  AfterTestCommand.Execute(TestExecutionContext context)
        //  SimpleWorkItem.PerformWork()
        [Test]
        public async Task when_invoking_test_fixture_member_with_lambda_expression()
        {
            await AssertAsync();

            async Task should_not_crash()
            {
                Func<Task> act = () => Subject.FailAsync();
                await act.Should().ThrowAsync<Exception>();
            }
        }

        FailSubject Subject;
    }
}
