using FluentAssertions;
using LoFuUnit.AutoMoq;
using LoFuUnit.Tests.Fakes;
using NUnit.Framework;

namespace LoFuUnit.Tests.LoFuUnit.Auto
{
    public class FakeLoFuTestBaseTests : LoFuTest<FakeSubject>
    {
        [SetUp]
        public void SetUp()
        {
            Clear();
        }

        [Test]
        public void Clear_should_reset_the_Fixture()
        {
            Use<IFakeDependency>();
            Fixture.Customizations.Count.Should().Be(2);

            Clear();

            Fixture.Customizations.Count.Should().Be(1);
        }

        [Test]
        public void Clear_should_reset_the_mocks()
        {
            Use<IFakeDependency>();
            The<IFakeDependency>().Should().NotBeNull();

            Clear();

            The<IFakeDependency>().Should().BeNull();
        }

        [Test]
        public void Clear_should_reset_the_Subject()
        {
            var result = Subject;
            Subject.Should().Be(result);

            Clear();

            Subject.Should().NotBe(result);
        }
    }
}