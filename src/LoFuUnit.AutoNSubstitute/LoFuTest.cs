using AutoFixture.AutoNSubstitute;
using LoFuUnit.Auto;

namespace LoFuUnit.AutoNSubstitute
{
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject> where TSubject : class
    {
        protected LoFuTest() : base(new AutoNSubstituteCustomization())
        {
        }
    }
}