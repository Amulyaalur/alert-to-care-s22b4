
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace API.IntegrationTest
{
    public class IcuOccupancyTest
    {
        [Fact]
        public async Task CheckStatusCodeEqualOKGetAllPatients()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Patients");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CheckStatusCodeEqualOKGetPatientById()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Patients/MRN001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
