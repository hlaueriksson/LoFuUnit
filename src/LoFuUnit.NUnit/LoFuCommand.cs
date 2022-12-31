using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;
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
        /// Initializes a new instance of the <see cref="LoFuCommand"/> class.
        /// Constructs a command that runs the local functions in the containing test method derived from the test execution context.
        /// </summary>
        /// <param name="innerCommand">The inner command.</param>
        public LoFuCommand(TestCommand innerCommand)
            : base(innerCommand)
        {
            AfterTest = (context) =>
            {
                var result = context.CurrentResult;

                if (result.ResultState != ResultState.Success) return;

                var fixture = GetFixture(context.CurrentTest);
                var method = context.CurrentTest.Method!.MethodInfo;

                try
                {
                    if (IsAsync())
                    {
                        fixture.AssertAsync(method).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                    else
                    {
                        fixture.Assert(method);
                    }
                }
                catch (InconclusiveLoFuTestException e)
                {
                    throw new InconclusiveException(e.Message, e);
                }

                bool IsAsync()
                {
                    return method.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
                }
            };

            object GetFixture(ITest test) => test.Fixture ?? GetFixture(test.Parent!);
        }
    }
}
