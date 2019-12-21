using System.Collections;
using System.Data;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFuUnit.Tests.LoFuUnit.MSTest.Fakes
{
    public class FakeTestContext : TestContext
    {
        public override string TestName { get; }

        public FakeTestContext(string methodName) => TestName = methodName;

        public override void WriteLine(string message) => throw new System.NotImplementedException();

        public override void WriteLine(string format, params object[] args) => throw new System.NotImplementedException();

        public override void AddResultFile(string fileName) => throw new System.NotImplementedException();

        public override void BeginTimer(string timerName) => throw new System.NotImplementedException();

        public override void EndTimer(string timerName) => throw new System.NotImplementedException();

        public override IDictionary Properties { get; }
        public override DataRow DataRow { get; }
        public override DbConnection DataConnection { get; }
    }
}