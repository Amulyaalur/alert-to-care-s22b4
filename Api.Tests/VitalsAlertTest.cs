using System.Net;
using System.Threading.Tasks;
using Xunit;
namespace API.Tests
{

    public class VitalsAlertTest
    {
        [Fact]
        public async Task TestVitals()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}

