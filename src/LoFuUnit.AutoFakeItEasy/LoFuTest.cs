using System.Reflection;
using AutoFixture.AutoFakeItEasy;
using FakeItEasy;
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

        /// <inheritdoc />
        protected override TDependency? The<TDependency>()
            where TDependency : class
        {
            var result = base.The<TDependency>();
            if (result != null) return result;

            var type = typeof(TDependency);
            if (IsFake(type))
            {
                var fakedType = typeof(TDependency).GetGenericArguments()[0];
                var fake = The(fakedType);
                if (fake == null) return null;

                result = Activator.CreateInstance(type) as TDependency;
                var field = type.GetField($"<{nameof(Fake<object>.FakedObject)}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(result, fake);

                return result;
            }

            return null;

            static bool IsFake(Type type)
            {
                return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Fake<>);
            }
        }
    }
}
