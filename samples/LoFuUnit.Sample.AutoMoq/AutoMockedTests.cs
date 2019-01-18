using FluentAssertions;
using LoFuUnit.AutoMoq;
using LoFuUnit.NUnit;
using Moq;
using NUnit.Framework;

namespace LoFuUnit.Sample.AutoMoq
{
    public class AutoMockedTests : LoFuTest<HelloWorld>
    {
        [SetUp]
        public void SetUp()
        {
            Clear();

            Use<IFoo>().Setup(x => x.GetFoo()).Returns("Hello");
            Use(new Mock<IBar>()).Setup(x => x.GetBar()).Returns("World");
            Use("!");
        }

        [LoFuTest]
        public void GetMessage()
        {
            Result = Subject.GetMessage();

            void should_invoke_IFoo_GetMessage() => The<IFoo>().Verify(x => x.GetFoo());
            void should_invoke_IBar_GetMessage() => The<IBar>().Verify(x => x.GetBar(), Times.Once());
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