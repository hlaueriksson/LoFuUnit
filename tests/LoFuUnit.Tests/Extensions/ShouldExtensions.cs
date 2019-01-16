using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;

namespace LoFuUnit.Tests.Extensions
{
    public static class ShouldExtensions
    {
        public static void ShouldMatch(this string log, string fixtureMethodName, params string[] localFunctionNames)
        {
            var pattern = $@"{fixtureMethodName}\s*{string.Join(@"\s*", localFunctionNames)}\s*";

            log.Should().MatchRegex(pattern);
        }

        public static void ShouldMatch(this List<MethodBase> invocations, string fixtureMethodName, params string[] localFunctionNames)
        {
            invocations.Count.Should().Be(localFunctionNames.Length, "because all expected local functions should be invoked");

            var methods = invocations.Select(x => x.Name).ToArray();
            var patterns = localFunctionNames.Select(x => $@"<{fixtureMethodName}>g__{x}\|[0-9]*_[0-9]*").ToArray();

            for (var i = 0; i < methods.Length; i++)
            {
                methods.ElementAt(i).Should().MatchRegex(patterns.ElementAt(i));
            }
        }
    }
}