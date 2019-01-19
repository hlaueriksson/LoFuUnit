using AutoFixture.AutoFakeItEasy;
using LoFuUnit.Auto;

namespace LoFuUnit.AutoFakeItEasy
{
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject> where TSubject : class
    {
        protected LoFuTest() : base(new AutoFakeItEasyCustomization())
        {
        }
    }
}