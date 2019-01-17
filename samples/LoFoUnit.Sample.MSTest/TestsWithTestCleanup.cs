using System;
using System.Collections.Generic;
using FluentAssertions;
using LoFoUnit.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFoUnit.Sample.MSTest
{
    [TestClass]
    public class TestsWithTestCleanup
    {
        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void TestCleanup() => this.Assert(TestContext);

        private Stack<int> Subject { get; set; }

        [TestMethod]
        public void Empty_stack()
        {
            Subject = new Stack<int>();

            void should_have_no_elements() => Subject.Count.Should().Be(0);
            void should_throw_an_exception_when_calling__Pop__() => Subject.Invoking(y => y.Pop()).Should().Throw<InvalidOperationException>();
            void should_throw_an_exception_when_calling__Peek__() => Subject.Invoking(y => y.Peek()).Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Non_empty_stack()
        {
            Subject = new Stack<int>(new[] { 1, 2, 3 });

            void should_return_the_top_element_when_calling__Peek__() => Subject.Peek().Should().Be(3);
            void should_not_remove_the_top_element_when_calling__Peek__()
            {
                Subject.Peek();
                Subject.Should().Contain(3);
            }
            void should_return_the_top_element_when_calling__Pop__() => Subject.Pop().Should().Be(3);
            void should_remove_the_top_element_when_calling__Pop__()
            {
                Subject.Pop();
                Subject.Should().NotContain(3);
            }
        }
    }
}