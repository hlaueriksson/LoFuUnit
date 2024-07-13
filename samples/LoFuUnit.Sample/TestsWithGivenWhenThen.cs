using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Sample
{
    public class TestsWithGivenWhenThen : LoFuTest
    {
        private Stack<int> Subject { get; set; }
        private int Result { get; set; }

        [Test]
        public void Empty_stack()
        {
            Assert();

            void given_an_empty_stack() => Subject = new Stack<int>();
            void then_it_should_have_no_elements() => Subject.Count.Should().Be(0);
            void and_it_should_throw_an_exception_when_calling__Pop__() => Subject.Invoking(y => y.Pop()).Should().Throw<InvalidOperationException>();
            void and_it_should_throw_an_exception_when_calling__Peek__() => Subject.Invoking(y => y.Peek()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Non_empty_stack()
        {
            Assert();

            void given_a_non_empty_stack() => Subject = new Stack<int>([1, 2, 3]);
            void when_calling_peek() => Result = Subject.Peek();
            void then_it_returns_the_top_element() => Result.Should().Be(3);
            void but_it_does_not_remove_the_top_element() => Subject.Should().Contain(3);
            void when_calling_pop() => Result = Subject.Pop();
            void then_it_returns_the_top_element_() => Result.Should().Be(3);
            void and_it_removes_the_top_element() => Subject.Should().NotContain(Result);
        }
    }
}
