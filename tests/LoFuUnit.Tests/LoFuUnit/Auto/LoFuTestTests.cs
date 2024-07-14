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

        [Test]
        public void One_should_return_a_fake()
        {
            One<int>().Should().BeInRange(int.MinValue, int.MaxValue);

            One<object>().Should().NotBeNull();

            One<IFakeDependency>().Should().NotBeNull();

            var fake = One<FakeDependency>();
            fake.Should().NotBeNull();
            fake.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Some_should_return_fakes()
        {
            Some<int>().Should().HaveCount(3);
            Some<int>(4).Should().HaveCount(4);
            Some<int>(0).Should().HaveCount(0);
            this.Invoking(x => x.Some<int>(-1)).Should().Throw<ArgumentOutOfRangeException>();

            Some<object>().Should().AllSatisfy(x => x.Should().NotBeNull());

            Some<IFakeDependency>().Should().AllSatisfy(x => x.Should().NotBeNull());

            Some<FakeDependency>().Should().AllSatisfy(x =>
            {
                x.Should().NotBeNull();
                x.Id.Should().NotBeEmpty();
            });
        }
    }
}
