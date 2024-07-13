using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Sample
{
    public class TestsWithExtensionMethod
    {
        private Stack<int> Subject { get; set; }

        [Test]
        public void Empty_stack()
        {
            Subject = new Stack<int>();

            this.Assert();

            void should_have_no_elements() => Subject.Count.Should().Be(0);
            void should_throw_an_exception_when_calling__Pop__() => Subject.Invoking(y => y.Pop()).Should().Throw<InvalidOperationException>();
            void should_throw_an_exception_when_calling__Peek__() => Subject.Invoking(y => y.Peek()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Non_empty_stack()
        {
            Subject = new Stack<int>([1, 2, 3]);

            this.Assert();

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
