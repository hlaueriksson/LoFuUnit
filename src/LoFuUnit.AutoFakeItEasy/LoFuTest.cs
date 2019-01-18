using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using FakeItEasy;

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

        protected Fake<TDependency> The<TDependency>() where TDependency : class
        {
            var type = typeof(TDependency);

            return _fakes.ContainsKey(type) ? _fakes[type] as Fake<TDependency> : null;
        }

        protected Fake<TDependency> Use<TDependency>() where TDependency : class
        {
            var fake = Fixture.Freeze<Fake<TDependency>>();
            _fakes[typeof(TDependency)] = fake;

            return fake;
        }

        protected Fake<TDependency> Use<TDependency>(Fake<TDependency> fake) where TDependency : class
        {
            Fixture.Inject(fake.FakedObject);
            _fakes[typeof(TDependency)] = fake;

            return fake;
        }

        protected TDependency Use<TDependency>(TDependency instance)
        {
            Fixture.Inject(instance);

            return instance;
        }
    }
}