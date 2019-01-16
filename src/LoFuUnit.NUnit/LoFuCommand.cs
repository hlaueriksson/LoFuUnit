using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.NUnit
{
    public class LoFuCommand : AfterTestCommand
    {
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