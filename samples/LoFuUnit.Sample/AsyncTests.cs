using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace LoFuUnit.Sample
{
    public class AsyncTests : LoFuTest
    {
        private HttpClient Subject { get; set; }
        private HttpResponseMessage Response { get; set; }

        [Test]
        public async Task HttpClient()
        {
            await AssertAsync();

            void given_a_HttpClient() => Subject = new HttpClient();
            async Task when_getting_the_GitHub_site() => Response = await Subject.GetAsync("https://github.com");
            void then_it_should_have_success_status_code() => Response.EnsureSuccessStatusCode();
            async Task then_it_should_have_proper_content()
            {
                var content = await Response.Content.ReadAsStringAsync();
                content.Should().Contain("Let’s build from here");
            }
        }
    }
}
