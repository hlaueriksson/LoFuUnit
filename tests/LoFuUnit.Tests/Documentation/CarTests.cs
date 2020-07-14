using FluentAssertions;
using LoFuUnit.NUnit;
using NUnit.Framework;

namespace LoFuUnit.Tests.Documentation
{
    public class Given_a_car
    {
        Car subject;
        bool is_stopped;

        [SetUp]
        public void SetUp() =>
            subject = new Car();

        [LoFu, Test]
        public void when_stopped()
        {
            is_stopped = subject.Stop();

            void should_turn_off_the_engine() =>
                is_stopped.Should().BeTrue();
        }
    }

    public class Car
    {
        public bool Stop()
        {
            return true;
        }
    }
}