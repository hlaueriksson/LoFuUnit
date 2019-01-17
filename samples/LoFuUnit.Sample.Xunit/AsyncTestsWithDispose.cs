using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LoFuUnit.Xunit;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnit.Sample.Xunit
{
    public class AsyncTestsWithDispose : IAsyncLifetime
    {
        private ITestOutputHelper Output { get; }

        public AsyncTestsWithDispose(ITestOutputHelper output) => Output = output;

        public async Task InitializeAsync() => await Task.CompletedTask;
        public async Task DisposeAsync() => await this.AssertAsync(Output as TestOutputHelper);

        private HttpClient Subject { get; set; }
        private HttpResponseMessage Response { get; set; }

        [Fact]
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