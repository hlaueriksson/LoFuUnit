using FluentAssertions;
using LoFuUnit.NUnit;
using NUnit.Framework;

namespace LoFuUnit.Sample.NUnit
{
    public class TestsWithTestCase
    {
        [LoFu]
        [TestCase(12, 3, 4)]
        [TestCase(12, 2, 6)]
        [TestCase(12, 4, 3)]
        public void DivideTest(int n, int d, int q)
        {
            Result = n / d;
            Expected = q;

            void assert() => Result.Should().Be(Expected);
        }

        private int Result { get; set; }
        private int Expected { get; set; }
    }
}