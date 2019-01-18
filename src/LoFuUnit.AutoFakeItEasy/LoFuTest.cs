using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;

namespace LoFuUnit.AutoFakeItEasy
{
    public abstract class LoFuTest<TSubject> : LoFuTest where TSubject : class
    {
        protected IFixture Fixture { get; }
        protected TSubject Subject => _subject ?? (_subject = Fixture.Create<TSubject>());

        private readonly Dictionary<Type, object> _fakes;
        private TSubject _subject;

        protected LoFuTest()
        {
            Fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
            _fakes = new Dictionary<Type, object>();
        }

        protected void Clear()
        {
            _fakes.Clear();
            _subject = null;
        }

        protected TDependency The<TDependency>() where TDependency : class
        {
            var type = typeof(TDependency);

            return _fakes.ContainsKey(type) ? _fakes[type] as TDependency : null;
        }

        protected TDependency Use<TDependency>() where TDependency : class
        {
            var fake = Fixture.Freeze<TDependency>();
            _fakes[typeof(TDependency)] = fake;

            return fake;
        }

        protected TDependency Use<TDependency>(TDependency instance)
        {
            Fixture.Inject(instance);
            _fakes[typeof(TDependency)] = instance;

            return instance;
        }
    }
}