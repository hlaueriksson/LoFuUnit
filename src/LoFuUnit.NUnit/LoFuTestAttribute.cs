using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.NUnit
{
    /// <summary>
    /// Marks the method as callable from the NUnit test runner and runs the local functions in the containing test method.
    /// </summary>
    public class LoFuTestAttribute : TestAttribute, IWrapSetUpTearDown
    {
        // https://github.com/nunit/docs/wiki/ICommandWrapper-Interface

        /// <summary>
        /// Wraps a command and returns the result.
        /// </summary>
        /// <param name="command">The command to be wrapped</param>
        /// <returns>The wrapped command</returns>
        public TestCommand Wrap(TestCommand command)
        {
            return new LoFuCommand(command);
        }
    }
}