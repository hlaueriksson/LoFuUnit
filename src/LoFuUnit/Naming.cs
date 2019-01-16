using System.Text.RegularExpressions;

namespace LoFuUnit
{
    internal static class Naming
    {
        static readonly Regex QuoteRegex = new Regex(@"(?<quoted>__(?<inner>\w+?)__)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        internal static string ToFormat(this string name)
        {
            name = ReplaceDoubleUnderscoresWithQuotes(name);
            name = ReplaceUnderscoreEssWithPossessive(name);
            name = ReplaceSingleUnderscoresWithSpaces(name);

            return name;
        }

        internal static string ToFormat(this string name, string methodName)
        {
            const string prefix = "g__";
            const char suffix = '|';

            var result = name.Substring(methodName.Length + prefix.Length);
            result = result.Remove(result.LastIndexOf(suffix));

            return result.ToFormat();
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