using System.Reflection;
using AutoFixture.AutoMoq;
using LoFuUnit.Auto;
using Moq;

namespace LoFuUnit.AutoMoq
{
    /// <summary>
    /// Base class for <c>Moq</c> auto-mocked test fixtures.
    /// </summary>
    /// <typeparam name="TSubject">The subject under test type.</typeparam>
    public abstract class LoFuTest<TSubject> : LoFuTestBase<TSubject>
        where TSubject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoFuTest{TSubject}"/> class,
        /// with the <see cref="AutoFixture.IFixture" /> customized with a <see cref="AutoMoqCustomization" />.
        /// </summary>
        protected LoFuTest()
            : base(new AutoMoqCustomization())
        {
        }

        /// <inheritdoc />
        protected override TDependency? The<TDependency>()
            where TDependency : class
        {
            var result = base.The<TDependency>();
            if (result != null) return result;

            var type = typeof(TDependency);
            if (IsMock(type))
            {
                var mockedType = typeof(TDependency).GetGenericArguments()[0];
                var mock = The(mockedType);
                if (mock == null) return null;

                return mock.GetType().GetProperty(nameof(IMocked<object>.Mock))?.GetValue(mock, null) as TDependency;
            }

            return null;

            static bool IsMock(Type type)
            {
                return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Mock<>);
            }
        }
    }
}
