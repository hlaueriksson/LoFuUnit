using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LoFoUnit.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFoUnit.Sample.MSTest
{
    [TestClass]
    public class AsyncTestsWithTestCleanup
    {
        public TestContext TestContext { get; set; }

        [TestCleanup]
        public async Task TestCleanup() => await this.AssertAsync(TestContext);

        private HttpClient Subject { get; set; }
        private HttpResponseMessage Response { get; set; }

        [TestMethod]
        public async Task HttpClient()
        {
            await Task.CompletedTask;

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