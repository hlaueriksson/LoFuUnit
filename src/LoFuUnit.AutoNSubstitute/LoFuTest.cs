using AutoFixture.AutoNSubstitute;
using LoFuUnit.Auto;

namespace LoFuUnit.AutoNSubstitute
{
    /// <summary>
    /// Base class for <c>NSubstitute</c> auto-mocked test fixtures.
    /// </summary>
    /// <typeparam name="TSubject">The subject under test type.</typeparam>
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject> where TSubject : class
    {
        /// <summary>
        /// Initializes a new instance of the test fixture with the <see cref="AutoFixture.IFixture" /> customized with a <see cref="AutoNSubstituteCustomization" />.
        /// </summary>
        protected LoFuTest() : base(new AutoNSubstituteCustomization())
        {
        }
    }
}