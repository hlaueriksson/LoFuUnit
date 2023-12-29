using FluentAssertions;
using LoFuUnit.NUnit;
using NUnit.Framework;

namespace LoFuUnit.Sample.NUnit
{
    public class TestsWithTestCaseSource
    {
        [LoFu, TestCaseSource(nameof(DivideCases))]
        public void DivideTest(int n, int d, int q)
        {
            Result = n / d;
            Expected = q;

            void assert() => Result.Should().Be(Expected);
        }

        static readonly object[] DivideCases =
        [
            new object[] { 12, 3, 4 },
            new object[] { 12, 2, 6 },
            new object[] { 12, 4, 3 }
        ];

        private int Result { get; set; }
        private int Expected { get; set; }
    }
}
