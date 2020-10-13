using AlertToCareAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;


namespace API.Tests
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; set; }

        public TestClientProvider()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
