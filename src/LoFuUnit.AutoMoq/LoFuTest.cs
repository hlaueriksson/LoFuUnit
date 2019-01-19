using AutoFixture.AutoMoq;
using LoFuUnit.Auto;

namespace LoFuUnit.AutoMoq
{
    /// <summary>
    /// Base class for <c>Moq</c> auto-mocked test fixtures.
    /// </summary>
    /// <typeparam name="TSubject">The subject under test type.</typeparam>
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject> where TSubject : class
    {
        /// <summary>
        /// Initializes a new instance of the test fixture with the <see cref="AutoFixture.IFixture" /> customized with a <see cref="AutoMoqCustomization" />.
        /// </summary>
        protected LoFuTest() : base(new AutoMoqCustomization())
        {
        }
    }
}