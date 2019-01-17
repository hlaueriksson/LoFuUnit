using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace LoFuUnit.Sample.Xunit
{
    public class AsyncTests
    {
        private ITestOutputHelper Output { get; }

        public AsyncTests(ITestOutputHelper output) => Output = output;

        private HttpClient Subject { get; set; }
        private HttpResponseMessage Response { get; set; }

        [Fact]
        public async Task HttpClient()
        {
            await this.AssertAsync(Output);

            void given_a_HttpClient() => Subject = new HttpClient();
            async Task when_getting_the_GitHub_site() => Response = await Subject.GetAsync("https://github.com");
            void then_it_should_have_success_status_code() => Response.EnsureSuccessStatusCode();
            async Task then_it_should_have_proper_content()
            {
                var content = await Response.Content.ReadAsStringAsync();
                content.Should().Contain("Built for developers");
            }
        }
    }
}