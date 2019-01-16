using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit
{
    public class NamingTests
    {
        [Test]
        public void ToFormat_class_name()
        {
            "A_b_c".ToFormat().Should().Be("A b c");
            "A__b__c".ToFormat().Should().Be("A \"b\" c");
            "A_s_c".ToFormat().Should().Be("A's c");
        }

        [Test]
        public void ToFormat_local_function_name()
        {
            "<ContainingMethod>g__A_b_c|0_0".ToFormat("<ContainingMethod>").Should().Be("A b c");
            "<ContainingMethod>g__A__b__c|0_0".ToFormat("<ContainingMethod>").Should().Be("A \"b\" c");
            "<ContainingMethod>g__A_s_c|0_0".ToFormat("<ContainingMethod>").Should().Be("A's c");
        }
    }
}