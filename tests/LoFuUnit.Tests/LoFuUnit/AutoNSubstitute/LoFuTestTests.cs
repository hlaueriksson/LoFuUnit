using AutoFixture;
using FluentAssertions;
using LoFuUnit.AutoNSubstitute;
using LoFuUnit.Tests.Fakes;
using NSubstitute;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit.AutoNSubstitute
{
    public class LoFuTestTests : LoFuTest<FakeSubject>
    {
        [SetUp]
        public void SetUp()
        {
            Clear();
        }

        [Test]
        public void The_should_return_null_before_Use()
        {
            The<IFakeDependency>().Should().BeNull();
            The<FakeDependencyBase>().Should().BeNull();
        }

        [Test]
        public void The_should_return_the_substitute_after_Use()
        {
            var substitute1 = Use<IFakeDependency>();
            var substitute2 = Use<FakeDependencyBase>();

            The<IFakeDependency>().Should().Be(substitute1);
            The<FakeDependencyBase>().Should().Be(substitute2);
        }

        [Test]
        public void The_should_return_the_substitute_after_Subject()
        {
            var result = Subject;

            var substitute = The<IFakeDependency>();
            substitute.Should().NotBeNull();

            // Verify
            result.Dependency1.Id.Should().BeEmpty();
            _ = substitute.Received().Id;
        }

        [Test]
        public void Subject_should_be_auto_mocked_with_dependencies_from_Use()
        {
            var substitute1 = Use(Substitute.For<IFakeDependency>());
            var substitute2 = Use<FakeDependencyBase>();
            var dependency3 = Use(new FakeDependency(Guid.NewGuid()));

            var result = Subject;

            result.Dependency1.Should().Be(substitute1);
            result.Dependency2.Should().Be(substitute2);
            result.Dependency3.Should().Be(dependency3);
        }

        [Test]
        public void Fixture_is_available()
        {
            var substitute1 = Fixture.Freeze<IFakeDependency>();
            var substitute2 = Fixture.Freeze<FakeDependencyBase>();
            var dependency3 = Fixture.Freeze<FakeDependency>();

            var result = Subject;

            result.Dependency1.Should().Be(substitute1);
            result.Dependency2.Should().Be(substitute2);
            result.Dependency3.Should().Be(dependency3);
        }
    }
}
