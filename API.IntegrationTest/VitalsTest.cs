using System.Net;
using System.Threading.Tasks;
using Xunit;
namespace API.IntegrationTest
{

    public class VitalsTest
    {
        [Fact]
        public async Task TestVitals()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestAlert()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring/WrongId");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        

    }
}

