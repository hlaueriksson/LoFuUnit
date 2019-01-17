using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.NUnit;

namespace LoFuUnit.Sample.NUnit
{
    public class AsyncTestsWithAttribute
    {
        private HttpClient Subject { get; set; }
        private HttpResponseMessage Response { get; set; }

        [LoFuTest]
        public async Task HttpClient()
        {
            await Task.CompletedTask;

            void given_a_HttpClient() => Subject = new HttpClient();
            async Task when_get_the_GitHub_site() => Response = await Subject.GetAsync("https://github.com");
            void then_it_should_have_success_status_code() => Response.EnsureSuccessStatusCode();
            async Task then_it_should_have_proper_content()
            {
                var content = await Response.Content.ReadAsStringAsync();
                content.Should().Contain("Built for developers");
            }
        }
    }
}