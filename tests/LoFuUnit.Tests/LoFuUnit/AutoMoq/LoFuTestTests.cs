using AutoFixture;
using FluentAssertions;
using LoFuUnit.AutoMoq;
using LoFuUnit.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit.AutoMoq
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
            The<Mock<IFakeDependency>>().Should().BeNull();
            The<Mock<FakeDependencyBase>>().Should().BeNull();
        }

        [Test]
        public void The_should_return_the_mock_after_Use()
        {
            var mock1 = Use<Mock<IFakeDependency>>();
            var mock2 = Use<Mock<FakeDependencyBase>>();

            The<Mock<IFakeDependency>>().Should().Be(mock1);
            The<Mock<FakeDependencyBase>>().Should().Be(mock2);
        }

        [Test]
        public void Subject_should_be_auto_mocked_with_dependencies_from_Use()
        {
            var mock1 = Use(new Mock<IFakeDependency>());
            var mock2 = Use<Mock<FakeDependencyBase>>();
            var dependency3 = Use(new FakeDependency(Guid.NewGuid()));

            var result = Subject;

            result.Dependency1.Should().Be(mock1.Object);
            result.Dependency2.Should().Be(mock2.Object);
            result.Dependency3.Should().Be(dependency3);
        }

        [Test]
        public void Use_alternative_syntax()
        {
            var mock1 = Use(Mock.Of<IFakeDependency>(x => x.Id == Guid.NewGuid()));

            var result = Subject;

            result.Dependency1.Should().Be(The<IFakeDependency>());
            result.Dependency1.Id.Should().NotBeEmpty();
            Mock.Get(mock1).Verify(x => x.Id);
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