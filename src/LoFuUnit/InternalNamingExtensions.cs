using System.Reflection;
using System.Text.RegularExpressions;

namespace LoFuUnit
{
    internal static class InternalNamingExtensions
    {
        private const string Gt = "<";
        private const string Lt = ">";
        private const string Prefix = "g__";
        private const char Suffix = '|';

        private static readonly Regex _quoteRegex = new(@"(?<quoted>__(?<inner>\w+?)__)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        internal static string WrappedName(this MethodBase testMethod)
        {
            return Gt + testMethod.Name + Lt + Prefix;
        }

        internal static string GetFormattedName(this MethodBase testMethod)
        {
            return testMethod.Name.ToFormat();
        }

        internal static string GetFunctionName(this MethodInfo testFunction, MethodBase testMethod)
        {
            var start = Gt + testMethod.Name + Lt + Prefix;
            var result = testFunction.Name.Substring(start.Length);
            return result.Remove(result.LastIndexOf(Suffix));
        }

        internal static string GetFunctionName(this Type testFunction, MethodBase testMethod)
        {
            var start = Gt + Gt + testMethod.Name + Lt + Prefix;
            var result = testFunction.Name.Substring(start.Length);
            return result.Remove(result.LastIndexOf(Suffix));
        }

        internal static string GetFormattedFunctionName(this MethodInfo testFunction, MethodBase testMethod)
        {
            return testFunction.GetFunctionName(testMethod).ToFormat();
        }

        private static string ToFormat(this string name)
        {
            name = ReplaceDoubleUnderscoresWithQuotes(name);
            name = ReplaceUnderscoreEssWithPossessive(name);
            name = ReplaceSingleUnderscoresWithSpaces(name);

            return name;
        }

        private static string ReplaceUnderscoreEssWithPossessive(string specificationName)
        {
            return specificationName.Replace("_s_", "'s ");
        }

        private static string ReplaceSingleUnderscoresWithSpaces(string specificationName)
        {
            return specificationName.Replace("_", " ");
        }

        private static string ReplaceDoubleUnderscoresWithQuotes(string specificationName)
        {
            return _quoteRegex.Replace(specificationName, " \"${inner}\" ");
        }
    }
}
