using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace LoFuUnit.AutoNSubstitute
{
    public abstract class LoFuTest<TSubject> : LoFuTest where TSubject : class
    {
        protected IFixture Fixture { get; }
        protected TSubject Subject => _subject ?? (_subject = Fixture.Create<TSubject>());

        private readonly Dictionary<Type, object> _substitutes;
        private TSubject _subject;

        protected LoFuTest()
        {
            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _substitutes = new Dictionary<Type, object>();
        }

        protected void Clear()
        {
            _substitutes.Clear();
            _subject = null;
        }

        protected TDependency The<TDependency>() where TDependency : class
        {
            var type = typeof(TDependency);

            return _substitutes.ContainsKey(type) ? _substitutes[type] as TDependency : null;
        }

        protected TDependency Use<TDependency>() where TDependency : class
        {
            var substitute = Fixture.Freeze<TDependency>();
            _substitutes[typeof(TDependency)] = substitute;

            return substitute;
        }

        protected TDependency Use<TDependency>(TDependency instance)
        {
            Fixture.Inject(instance);
            _substitutes[typeof(TDependency)] = instance;

            return instance;
        }
    }
}