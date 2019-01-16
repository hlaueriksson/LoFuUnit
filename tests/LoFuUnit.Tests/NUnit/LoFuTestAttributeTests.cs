using FluentAssertions;
using LoFuUnit.NUnit;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace LoFuUnit.Tests.NUnit
{
    public class LoFuTestAttributeTests
    {
        [Test]
        public void Wrap()
        {
            var fixture = new FakeLoFuTest();
            var method = new TestMethod(new MethodWrapper(fixture.GetType(), nameof(fixture.FakeTest)));
            var command = new EmptyTestCommand(method);

            var result = new LoFuTestAttribute().Wrap(command);

            result.Should().BeOfType<LoFuCommand>();
        }
    }
}