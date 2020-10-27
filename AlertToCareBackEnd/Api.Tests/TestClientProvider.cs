using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using AlertToCareAPI;

namespace API.Tests
{
    internal class  TestClientProvider
    {
        public HttpClient Client { get; set; }

        public TestClientProvider()
        {
            Client = new TestServer(new WebHostBuilder().UseStartup<Startup>()).CreateClient();
        }

    }
}
