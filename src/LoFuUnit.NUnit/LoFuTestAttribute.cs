using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.NUnit
{
    public class LoFuTestAttribute : TestAttribute, IWrapSetUpTearDown
    {
        // https://github.com/nunit/docs/wiki/ICommandWrapper-Interface

        public TestCommand Wrap(TestCommand command)
        {
            return new LoFuCommand(command);
        }
    }
}