using FakeItEasy;
using FluentAssertions;
using LoFuUnit.AutoFakeItEasy;
using LoFuUnit.NUnit;
using NUnit.Framework;

namespace LoFuUnit.Sample.AutoFakeItEasy
{
    public class AutoMockedTests : LoFuTest<HelloWorld>
    {
        [SetUp]
        public void SetUp()
        {
            Clear();

            A.CallTo(() => Use<IFoo>().GetFoo()).Returns("Hello");
            Use<Fake<IBar>>().CallsTo(x => x.GetBar()).Returns("World");
            Use("!");
        }

        [LoFuTest]
        public void GetMessage()
        {
            Result = Subject.GetMessage();

            void should_invoke_IFoo_GetMessage() => A.CallTo(() => The<IFoo>().GetFoo()).MustHaveHappened();
            void should_invoke_IBar_GetMessage() => The<Fake<IBar>>().CallsTo(x => x.GetBar()).MustHaveHappened(Repeated.Exactly.Once);
            void should_return_combined_message() => Result.Should().Be("Hello, World!");
        }

        string Result { get; set; }
    }

    public class HelloWorld
    {
        private readonly IFoo _foo;
        private readonly IBar _bar;
        private readonly string _suffix;

        public HelloWorld(IFoo foo, IBar bar, string suffix)
        {
            _foo = foo;
            _bar = bar;
            _suffix = suffix;
        }

        public string GetMessage() => string.Join(", ", _foo.GetFoo(), _bar.GetBar()) + _suffix;
    }

    public interface IFoo
    {
        string GetFoo();
    }

    public interface IBar
    {
        string GetBar();
    }
}
