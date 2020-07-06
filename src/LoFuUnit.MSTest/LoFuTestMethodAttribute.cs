using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFuUnit.MSTest
{
    public class LoFuTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            // TODO: get fixture how?

            return new[] { testMethod.Invoke(null) };
        }
    }
}