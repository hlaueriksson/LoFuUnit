using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.NUnit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoFuAttribute : Attribute, IWrapSetUpTearDown
    {
        // https://github.com/nunit/docs/wiki/ICommandWrapper-Interface

        public TestCommand Wrap(TestCommand command)
        {
            return new LoFuCommand(command);
        }
    }
}