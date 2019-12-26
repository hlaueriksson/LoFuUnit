using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LoFuUnit
{
    internal static class InternalNamingExtensions
    {
        static readonly Regex QuoteRegex = new Regex(@"(?<quoted>__(?<inner>\w+?)__)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        const string Gt = "<";
        const string Lt = ">";
        const string Prefix = "g__";

        const char Suffix = '|';

        internal static string WrappedName(this MethodBase testMethod)
        {
            return Gt + testMethod.Name + Lt;
        }

        internal static string GetFormattedName(this MethodBase testMethod)
        {
            return testMethod.Name.ToFormat();
        }

        internal static string GetFunctionName(this MethodInfo testFunction, MethodBase testMethod)
        {
            var start = Gt + testMethod.Name + Lt + Prefix;
            var result = testFunction.Name.Substring(start.Length);
            result = result.Remove(result.LastIndexOf(Suffix));

            return result;
        }

        internal static string GetFunctionName(this Type testFunction, MethodBase testMethod)
        {
            var start = Gt + Gt + testMethod.Name + Lt + Prefix;
            var result = testFunction.Name.Substring(start.Length);
            result = result.Remove(result.LastIndexOf(Suffix));

            return result;
        }

        internal static string GetFormattedFunctionName(this MethodInfo testFunction, MethodBase testMethod)
        {
            return testFunction.GetFunctionName(testMethod).ToFormat();
        }

        static string ToFormat(this string name)
        {
            name = ReplaceDoubleUnderscoresWithQuotes(name);
            name = ReplaceUnderscoreEssWithPossessive(name);
            name = ReplaceSingleUnderscoresWithSpaces(name);

            return name;
        }

        static string ReplaceUnderscoreEssWithPossessive(string specificationName)
        {
            specificationName = specificationName.Replace("_s_", "'s ");
            return specificationName;
        }

        static string ReplaceSingleUnderscoresWithSpaces(string specificationName)
        {
            specificationName = specificationName.Replace("_", " ");
            return specificationName;
        }

        static string ReplaceDoubleUnderscoresWithQuotes(string specificationName)
        {
            specificationName = QuoteRegex.Replace(specificationName, " \"${inner}\" ");

            return specificationName;
        }
    }
}