using System;
using System.IO;
using LoFuUnit.NUnit;
using NUnit.Framework;

namespace LoFuUnit.Sample.NUnit
{
    public class TestsWithTheory
    {
        [DatapointSource]
        public double[] Values = { 0.0, 1.0, -1.0, 42.0 };

        [LoFu, Theory]
        public void SquareRootDefinition(double num)
        {
            Value = num;

            void definitionː() => Log.WriteLine("\t\tGiven a non-negative number, the square root of that number is always non-negative and, when multiplied by itself, gives the original number.");

            void assume() => Assume.That(Value >= 0.0);

            void sqrt() => Sqrt = Math.Sqrt(Value);

            void should_be_greater_or_equal_to_0() => Assert.That(Sqrt >= 0.0);
            void should_get_original_number_when_multiplied_by_itself() => Assert.That(Sqrt * Sqrt, Is.EqualTo(Value).Within(0.000001));
        }

        private TextWriter Log { get; } = Console.Out;
        private double Value { get; set; }
        private double Sqrt { get; set; }
    }
}