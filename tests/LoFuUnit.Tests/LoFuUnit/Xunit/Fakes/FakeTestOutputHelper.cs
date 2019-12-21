using System.Text;
using Xunit.Abstractions;

namespace LoFuUnit.Tests.LoFuUnit.Xunit.Fakes
{
    public class FakeTestOutputHelper : ITestOutputHelper
    {
        private StringBuilder Output { get; }

        public FakeTestOutputHelper(StringBuilder output) => Output = output;

        public void WriteLine(string message) => Output.AppendLine(message);

        public void WriteLine(string format, params object[] args) => throw new System.NotImplementedException();
    }
}