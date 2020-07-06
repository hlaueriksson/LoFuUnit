using System.Reflection;
using Xunit.Sdk;

namespace LoFuUnit.Xunit
{
    public class LoFuAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
        }

        public override void After(MethodInfo methodUnderTest)
        {
            // TODO: get fixture how?
        }
    }
}