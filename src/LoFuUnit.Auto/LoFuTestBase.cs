using System.Reflection;
using AutoFixture;

namespace LoFuUnit.Auto
{
    /// <summary>
    /// Base class for auto-mocked test fixtures.
    /// </summary>
    /// <typeparam name="TSubject">The type of the subject under test.</typeparam>
    public abstract class LoFuTestBase<TSubject> : LoFuTest
        where TSubject : class
    {
        private readonly ICustomization _customization;
        private readonly Dictionary<Type, object> _mocks;
        private TSubject? _subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoFuTestBase{TSubject}"/> class,
        /// with a <see cref="ICustomization"/> for the <see cref="IFixture" />.
        /// </summary>
        /// <param name="customization">A customization of an <see cref="IFixture" />.</param>
        protected LoFuTestBase(ICustomization customization)
        {
            _customization = customization;
            _mocks = [];
            Fixture = new Fixture().Customize(_customization);
        }

        /// <summary>
        /// Provides object creation services.
        /// </summary>
        protected IFixture Fixture { get; }

        /// <summary>
        /// The auto-mocked subject under test.
        /// </summary>
        protected TSubject Subject => _subject ??= Fixture.Create<TSubject>();

        /// <summary>
        /// Clears the test fixture:
        /// <list type="bullet">
        /// <item><description>The <see cref="IFixture" /> is reset from customizations.</description></item>
        /// <item><description>The known mocks returned by <see cref="The{TDependency}" /> are cleared.</description></item>
        /// <item><description>The <see cref="Subject" /> is reset.</description></item>
        /// </list>
        /// </summary>
        protected void Clear()
        {
            Fixture.Customizations.Clear();
            Fixture.Customize(_customization);
            _mocks.Clear();
            _subject = null;
        }

        /// <summary>
        /// Customize the <see cref="IFixture" /> and the test fixture to use this type of mock:
        /// <list type="bullet">
        /// <item><description>Freezes the type for the <see cref="IFixture" /> and creates a mock.</description></item>
        /// <item><description>Saves the mock so that it is known by the <see cref="The{TDependency}" /> method.</description></item>
        /// </list>
        /// </summary>
        /// <typeparam name="TDependency">The type to use.</typeparam>
        /// <returns>The mock.</returns>
        protected TDependency Use<TDependency>()
            where TDependency : class
        {
            var mock = Fixture.Freeze<TDependency>();
            _mocks[typeof(TDependency)] = mock;

            return mock;
        }

        /// <summary>
        /// Customize the <see cref="IFixture" /> and the test fixture to use this type of mock:
        /// <list type="bullet">
        /// <item><description>Injects the mock to the <see cref="IFixture" />.</description></item>
        /// <item><description>Saves the mock so that it is known by the <see cref="The{TDependency}" /> method.</description></item>
        /// </list>
        /// </summary>
        /// <typeparam name="TDependency">The type to use.</typeparam>
        /// <param name="instance">The mock instance.</param>
        /// <returns>The mock.</returns>
        protected TDependency Use<TDependency>(TDependency instance)
        {
            Fixture.Inject(instance);
            _mocks[typeof(TDependency)] = instance!;

            return instance;
        }

        /// <summary>
        /// Returns the mock for the specified type, or <c>null</c> if the type is unknown.
        /// </summary>
        /// <typeparam name="TDependency">The type of mock.</typeparam>
        /// <returns>The mock, or <c>null</c>.</returns>
        protected virtual TDependency? The<TDependency>()
            where TDependency : class
            => The(typeof(TDependency)) as TDependency;

#pragma warning disable SA1202 // Elements should be ordered by access
        internal object? The(Type type)
#pragma warning restore SA1202 // Elements should be ordered by access
        {
            // Frozen or injected
            if (_mocks.TryGetValue(type, out object? value))
            {
                return value;
            }

            // Auto-mocked
            if (_subject != null)
            {
                var field = _subject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(x => x.FieldType == type);
                if (field != null)
                {
                    return field.GetValue(_subject);
                }

                var property = _subject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(x => x.PropertyType == type);
                if (property != null)
                {
                    return property.GetValue(_subject);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates one fake of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of fake.</typeparam>
        /// <returns>One <typeparamref name="T" />.</returns>
        protected T One<T>()
            => Fixture.Create<T>();

        /// <summary>
        /// Creates three fakes of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of fakes.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/>.</returns>
        protected IEnumerable<T> Some<T>()
            => Some<T>(3);

        /// <summary>
        /// Creates some fakes of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of fakes.</typeparam>
        /// <param name="count">The number of fakes.</param>
        /// <returns>An <see cref="IEnumerable{T}"/>.</returns>
        protected IEnumerable<T> Some<T>(int count)
            => Fixture.CreateMany<T>(count);
    }
}
