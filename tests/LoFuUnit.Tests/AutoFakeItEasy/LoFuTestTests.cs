using System;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using LoFuUnit.AutoFakeItEasy;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.AutoFakeItEasy
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
            var fake1 = Use<IFakeDependency>();
            var fake2 = Use<FakeDependencyBase>();

            The<IFakeDependency>().Should().Be(fake1);
            The<FakeDependencyBase>().Should().Be(fake2);
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