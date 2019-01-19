using System;
using System.Collections.Generic;
using AutoFixture;

namespace LoFuUnit.Auto
{
    public abstract class LoFuTestBase<TSubject> : LoFuTest where TSubject : class
    {
        protected IFixture Fixture { get; }
        protected TSubject Subject => _subject ?? (_subject = Fixture.Create<TSubject>());

        private readonly ICustomization _customization;
        private readonly Dictionary<Type, object> _mocks;
        private TSubject _subject;

        protected LoFuTestBase(ICustomization customization)
        {
            _customization = customization;
            Fixture = new Fixture().Customize(_customization);
            _mocks = new Dictionary<Type, object>();
        }

        protected void Clear()
        {
            Fixture.Customizations.Clear();
            Fixture.Customize(_customization);
            _mocks.Clear();
            _subject = null;
        }

        protected TDependency The<TDependency>() where TDependency : class
        {
            var type = typeof(TDependency);

            return _mocks.ContainsKey(type) ? _mocks[type] as TDependency : null;
        }

        protected TDependency Use<TDependency>() where TDependency : class
        {
            var mock = Fixture.Freeze<TDependency>();
            _mocks[typeof(TDependency)] = mock;

            return mock;
        }

        protected TDependency Use<TDependency>(TDependency instance)
        {
            Fixture.Inject(instance);
            _mocks[typeof(TDependency)] = instance;

            return instance;
        }
    }
}