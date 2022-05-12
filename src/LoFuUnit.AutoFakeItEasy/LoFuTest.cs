using AutoFixture.AutoFakeItEasy;
using LoFuUnit.Auto;

namespace LoFuUnit.AutoFakeItEasy
{
    /// <summary>
    /// Base class for <c>FakeItEasy</c> auto-mocked test fixtures.
    /// </summary>
    /// <typeparam name="TSubject">The subject under test type.</typeparam>
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject>
        where TSubject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoFuTest{TSubject}"/> class,
        /// with the <see cref="AutoFixture.IFixture" /> customized with a <see cref="AutoFakeItEasyCustomization" />.
        /// </summary>
        protected LoFuTest()
            : base(new AutoFakeItEasyCustomization())
        {
        }
    }
}
