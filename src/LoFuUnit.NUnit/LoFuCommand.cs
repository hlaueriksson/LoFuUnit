using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.NUnit
{
    /// <summary>
    /// Runs the local functions in the containing test method after the inner command has run.
    /// </summary>
    public class LoFuCommand : AfterTestCommand
    {
        /// <summary>
        /// Constructs a command that runs the local functions in the containing test method derived from the test execution context.
        /// </summary>
        /// <param name="innerCommand"></param>
        public LoFuCommand(TestCommand innerCommand) : base(innerCommand)
        {
            AfterTest = (context) =>
            {
                var result = context.CurrentResult;

                if (result.ResultState != ResultState.Success) return;

                var fixture = GetFixture(context.CurrentTest);
                var method = context.CurrentTest.Method.MethodInfo;

                if (IsAsync())
                {
                    fixture.AssertAsync(method).ConfigureAwait(false).GetAwaiter().GetResult();
                }
                else
                {
                    fixture.Assert(method);
                }

                bool IsAsync()
                {
                    return method.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
                }
            };

            object GetFixture(ITest test) => test.Fixture ?? GetFixture(test.Parent);
        }
    }
}