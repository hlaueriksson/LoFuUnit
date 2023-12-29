using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoFuUnit.TargetFramework.Tests.Fakes
{
    public abstract class FakeLoFuTestBase : LoFuTest
    {
        public StringBuilder Out { get; } = new StringBuilder();
        public List<MethodBase> Invocations { get; } = new List<MethodBase>();

        protected override void Log(string message) => Out.AppendLine(message);

        protected void Record([CallerMemberName] string callerMemberName = "")
        {
            Invocations.Add(this.GetTestMethodForAssert(callerMemberName));
        }

        protected async Task RecordAsync([CallerMemberName] string callerMemberName = "")
        {
            Invocations.Add(this.GetTestMethodForAssertAsync(callerMemberName));

            await Task.CompletedTask;
        }
    }
}
