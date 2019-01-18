using System;

namespace LoFuUnit.Tests.AutoMoq.Fakes
{
    public class FakeSubject
    {
        public IFakeDependency Dependency1 { get; }
        public FakeDependencyBase Dependency2 { get; }
        public FakeDependency Dependency3 { get; }

        public FakeSubject(IFakeDependency dependency1, FakeDependencyBase dependency2, FakeDependency dependency3)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
            Dependency3 = dependency3;
        }
    }

    public interface IFakeDependency
    {
        Guid Id { get; set; }
    }

    public abstract class FakeDependencyBase : IFakeDependency
    {
        public Guid Id { get; set; }

        protected FakeDependencyBase(Guid id)
        {
            Id = id;
        }
    }

    public class FakeDependency : FakeDependencyBase
    {
        public FakeDependency(Guid id) : base(id)
        {
        }
    }
}