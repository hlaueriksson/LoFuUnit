using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.NUnit
{
    /// <summary>
    /// Runs the local functions in the containing test method marked by this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LoFuAttribute : Attribute, IWrapSetUpTearDown
    {
        // https://github.com/nunit/docs/wiki/ICommandWrapper-Interface

        /// <summary>
        /// Wraps a command and returns the result.
        /// </summary>
        /// <param name="command">The command to be wrapped.</param>
        /// <returns>The wrapped command.</returns>
        public TestCommand Wrap(TestCommand command)
        {
            return new LoFuCommand(command);
        }
    }
}
