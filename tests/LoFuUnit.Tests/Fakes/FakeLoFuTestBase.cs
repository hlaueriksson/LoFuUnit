using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoFuUnit.Tests.Fakes
{
    public abstract class FakeLoFuTestBase : LoFuTest
    {
        public StringBuilder Out { get; } = new StringBuilder();
        public List<MethodBase> Invocations { get; } = new List<MethodBase>();

        protected override void Log(string message) => Out.AppendLine(message);

        protected void Record()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            Invocations.Add(method);
        }

        protected async Task RecordAsync()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(5).GetMethod();

            Invocations.Add(method);

            await Task.CompletedTask;
        }
    }
}