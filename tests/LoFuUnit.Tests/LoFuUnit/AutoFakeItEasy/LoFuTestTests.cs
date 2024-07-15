using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using LoFuUnit.AutoFakeItEasy;
using LoFuUnit.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit.AutoFakeItEasy
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
            The<Fake<FakeDependency>>().Should().BeNull();
        }

        [Test]
        public void The_should_return_the_fake_after_Use()
        {
            var fake1 = Use<IFakeDependency>();
            var fake2 = Use<FakeDependencyBase>();

            The<IFakeDependency>().Should().Be(fake1);
            The<FakeDependencyBase>().Should().Be(fake2);
        }

        [Test]
        public void The_should_return_the_fake_after_Subject()
        {
            var result = Subject;

            var fake = The<IFakeDependency>();
            fake.Should().NotBeNull();

            // Verify
            result.Dependency1.Id.Should().BeEmpty();
            A.CallTo(() => fake.Id).MustHaveHappened();
        }

        [Test]
        public void The_should_return_the_Fake_T_after_Subject()
        {
            var result = Subject;

            var fake = The<Fake<IFakeDependency>>();
            fake.Should().NotBeNull();
            fake.Should().BeOfType<Fake<IFakeDependency>>();
            fake.FakedObject.Should().NotBeNull();

            // Verify
            result.Dependency1.Id.Should().BeEmpty();
            fake.CallsTo(x => x.Id).MustHaveHappened();
        }

        [Test]
        public void Subject_should_be_auto_mocked_with_dependencies_from_Use()
        {
            var fake1 = Use(A.Fake<IFakeDependency>());
            var fake2 = Use<FakeDependencyBase>();
            var dependency3 = Use(new FakeDependency(Guid.NewGuid()));

            var result = Subject;

            result.Dependency1.Should().Be(fake1);
            result.Dependency2.Should().Be(fake2);
            result.Dependency3.Should().Be(dependency3);
        }

        [Test]
        public void Use_alternative_syntax()
        {
            var fake1 = Use(new Fake<IFakeDependency>());
            fake1.CallsTo(x => x.Id).Returns(Guid.NewGuid());

            var result = Subject;

            result.Dependency1.Should().Be(The<Fake<IFakeDependency>>().FakedObject);
            result.Dependency1.Id.Should().NotBeEmpty();
            fake1.CallsTo(x => x.Id).MustHaveHappened();
        }

        [Test]
        public void Fixture_is_available()
        {
            var fake1 = Fixture.Freeze<IFakeDependency>();
            var fake2 = Fixture.Freeze<FakeDependencyBase>();
            var dependency3 = Fixture.Freeze<FakeDependency>();

            var result = Subject;

            result.Dependency1.Should().Be(fake1);
            result.Dependency2.Should().Be(fake2);
            result.Dependency3.Should().Be(dependency3);
        }
    }
}
