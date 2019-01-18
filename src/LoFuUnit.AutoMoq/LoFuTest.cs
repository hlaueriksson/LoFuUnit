using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace LoFuUnit.AutoMoq
{
    public abstract class LoFuTest<TSubject> : LoFuTest where TSubject : class
    {
        protected IFixture Fixture { get; }
        protected TSubject Subject => _subject ?? (_subject = Fixture.Create<TSubject>());

        private readonly Dictionary<Type, Mock> _mocks;
        private TSubject _subject;

        protected LoFuTest()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mocks = new Dictionary<Type, Mock>();
        }

        protected void Clear()
        {
            _mocks.Clear();
            _subject = null;
        }

        protected Mock<TDependency> The<TDependency>() where TDependency : class
        {
            var type = typeof(TDependency);

            return _mocks.ContainsKey(type) ? _mocks[type] as Mock<TDependency> : null;
        }

        protected Mock<TDependency> Use<TDependency>() where TDependency : class
        {
            var mock = Fixture.Freeze<Mock<TDependency>>();
            _mocks[typeof(TDependency)] = mock;

            return mock;
        }

        protected Mock<TDependency> Use<TDependency>(Mock<TDependency> mock) where TDependency : class
        {
            Fixture.Inject(mock.Object);
            _mocks[typeof(TDependency)] = mock;

            return mock;
        }

        protected TDependency Use<TDependency>(TDependency instance)
        {
            Fixture.Inject(instance);

            return instance;
        }
    }
}