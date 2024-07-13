using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Sample
{
    public class TestsWithArrangeActAssert : LoFuTest
    {
        private Stack<int> Subject { get; set; }
        private int Result { get; set; }

        [Test]
        public void Empty_stack()
        {
            Assert();

            void arrange() => Subject = new Stack<int>();
            void assert_is_empty() => Subject.Count.Should().Be(0);
            void assert__Pop__throws() => Subject.Invoking(y => y.Pop()).Should().Throw<InvalidOperationException>();
            void assert__Peek__throws() => Subject.Invoking(y => y.Peek()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Peek()
        {
            Assert();

            void arrange() => Subject = new Stack<int>([1, 2, 3]);
            void act() => Result = Subject.Peek();
            void assert_return_value() => Result.Should().Be(3);
            void assert_element_non_removal() => Subject.Should().Contain(3);
        }

        [Test]
        public void Pop()
        {
            Assert();

            void arrange() => Subject = new Stack<int>([1, 2, 3]);
            void act() => Result = Subject.Pop();
            void assert_return_value() => Result.Should().Be(3);
            void assert_element_removal() => Subject.Should().NotContain(Result);
        }
    }
}
