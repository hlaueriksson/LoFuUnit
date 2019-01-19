using AutoFixture.AutoMoq;
using LoFuUnit.Auto;

namespace LoFuUnit.AutoMoq
{
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject> where TSubject : class
    {
        protected LoFuTest() : base(new AutoMoqCustomization())
        {
        }
    }
}