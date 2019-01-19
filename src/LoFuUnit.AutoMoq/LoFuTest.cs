﻿using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace LoFuUnit.AutoMoq
{
    public abstract class LoFuTest<TSubject> : LoFuTest where TSubject : class
    {
        protected IFixture Fixture { get; }
        protected TSubject Subject => _subject ?? (_subject = Fixture.Create<TSubject>());

        private readonly Dictionary<Type, object> _mocks;
        private TSubject _subject;

        protected LoFuTest()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mocks = new Dictionary<Type, object>();
        }

        protected void Clear()
        {
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