using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit
{
    public class InternalNamingExtensionsTests
    {
        [Test]
        public void WrappedName()
        {
            typeof(FakeTestFixture).GetMethod(nameof(FakeTestFixture.A_b_c)).WrappedName().Should().Be("<A_b_c>");
        }

        [Test]
        public void GetFormattedName()
        {
            typeof(FakeTestFixture).GetMethod(nameof(FakeTestFixture.A_b_c)).GetFormattedName().Should().Be("A b c");
            typeof(FakeTestFixture).GetMethod(nameof(FakeTestFixture.A__b__c)).GetFormattedName().Should().Be("A \"b\" c");
            typeof(FakeTestFixture).GetMethod(nameof(FakeTestFixture.A_s_c)).GetFormattedName().Should().Be("A's c");
        }

        [Test]
        public void GetFunctionName()
        {
            var testMethod = typeof(FakeTestFixture).GetMethod(nameof(FakeTestFixture.ContainingMethod));
            testMethod.ReflectedType.GetMethod("<ContainingMethod>g__A_b_c|0_0", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .GetFunctionName(testMethod).Should().Be("A_b_c");
            testMethod.ReflectedType.GetMethod("<ContainingMethod>g__A__b__c|0_1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .GetFunctionName(testMethod).Should().Be("A__b__c");
            testMethod.ReflectedType.GetMethod("<ContainingMethod>g__A_s_c|0_2", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .GetFunctionName(testMethod).Should().Be("A_s_c");
        }

        [Test]
        public void GetFormattedFunctionName()
        {
            var testMethod = typeof(FakeTestFixture).GetMethod(nameof(FakeTestFixture.ContainingMethod));
            testMethod.ReflectedType.GetMethod("<ContainingMethod>g__A_b_c|0_0", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .GetFormattedFunctionName(testMethod).Should().Be("A b c");
            testMethod.ReflectedType.GetMethod("<ContainingMethod>g__A__b__c|0_1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .GetFormattedFunctionName(testMethod).Should().Be("A \"b\" c");
            testMethod.ReflectedType.GetMethod("<ContainingMethod>g__A_s_c|0_2", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .GetFormattedFunctionName(testMethod).Should().Be("A's c");
        }

        [Test]
        public void GetFunctionName_Type()
        {
            var testMethod = typeof(FakeAsyncTestFixture).GetMethod(nameof(FakeAsyncTestFixture.ContainingMethod));
            testMethod.ReflectedType.GetNestedType("<<ContainingMethod>g__A_b_c|0_0>d", BindingFlags.NonPublic)
                .GetFunctionName(testMethod).Should().Be("A_b_c");
            testMethod.ReflectedType.GetNestedType("<<ContainingMethod>g__A__b__c|0_1>d", BindingFlags.NonPublic)
                .GetFunctionName(testMethod).Should().Be("A__b__c");
            testMethod.ReflectedType.GetNestedType("<<ContainingMethod>g__A_s_c|0_2>d", BindingFlags.NonPublic)
                .GetFunctionName(testMethod).Should().Be("A_s_c");
        }
    }

    public class FakeTestFixture
    {
        public void ContainingMethod()
        {
            void A_b_c() { }
            void A__b__c() { }
            void A_s_c() { }
        }

        public void A_b_c() { }

        public void A__b__c() { }

        public void A_s_c() { }
    }

    public class FakeAsyncTestFixture
    {
        public async Task ContainingMethod()
        {
            async Task A_b_c() { }
            async Task A__b__c() { }
            async Task A_s_c() { }
        }
    }
}