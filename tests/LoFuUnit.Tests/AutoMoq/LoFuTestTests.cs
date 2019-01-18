using System;
using AutoFixture;
using FluentAssertions;
using LoFuUnit.AutoMoq;
using LoFuUnit.Tests.AutoMoq.Fakes;
using Moq;
using NUnit.Framework;

namespace LoFuUnit.Tests.AutoMoq
{
    public class LoFuTestTests : LoFuTest<FakeSubject>
    {
        [SetUp]
        public void SetUp()
        {
            Clear();
        }

        [Test]
        public void Clear_should_reset_the_Subject()
        {
            var result = Subject;
            Subject.Should().Be(result);

            Clear();

            Subject.Should().NotBe(result);
        }

        [Test]
        public void The_should_return_null_before_Use()
        {
            The<IFakeDependency>().Should().BeNull();
            The<FakeDependencyBase>().Should().BeNull();
        }

        [Test]
        public void The_should_return_the_Mock_after_Use()
        {
            var mock1 = Use<IFakeDependency>();
            var mock2 = Use<FakeDependencyBase>();

            The<IFakeDependency>().Should().Be(mock1);
            The<FakeDependencyBase>().Should().Be(mock2);
        }

        [Test]
        public void Subject_should_be_auto_mocked_with_dependencies_from_Use()
        {
            var mock1 = Use(new Mock<IFakeDependency>());
            var mock2 = Use<FakeDependencyBase>();
            var dependency3 = Use(new FakeDependency(Guid.NewGuid()));

            var result = Subject;

            result.Dependency1.Should().Be(mock1.Object);
            result.Dependency2.Should().Be(mock2.Object);
            result.Dependency3.Should().Be(dependency3);
        }

        [Test]
        public void Fixture_is_available()
        {
            var mock1 = Fixture.Freeze<Mock<IFakeDependency>>();
            var mock2 = Fixture.Freeze<Mock<FakeDependencyBase>>();
            var dependency3 = Fixture.Freeze<FakeDependency>();

            var result = Subject;

            result.Dependency1.Should().Be(mock1.Object);
            result.Dependency2.Should().Be(mock2.Object);
            result.Dependency3.Should().Be(dependency3);
        }
    }
}