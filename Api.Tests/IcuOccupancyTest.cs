
using AlertToCareAPI.ICUDatabase.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class IcuOccupancyTest
    {
        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllPatients()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Patients");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetPatientById()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Patients/MRN001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestAddPatient()
        {
            var client = new TestClientProvider().Client;
            Vitals vital = new Vitals()
                { PatientId = "MRN004", Bpm = 100, Spo2 = 120, RespRate = 90 };
            PatientAddress address = new PatientAddress()
                { PatientId = "MRN004", HouseNo = "20", Street = "main", City = "Dehradun", State = "UK", Pincode = "249001" };
            Patient patient = new Patient()
            {
                PatientId = "MRN004",
                PatientName = "Anita",
                Age = 50,
                ContactNo = "9123456789",
                BedId = "B11.2",
                IcuId = "I2",
                Vitals = vital,
                Address = address
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/IcuOccupancy/Patients", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CheckDeletePatient()
        {
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/IcuOccupancy/Patients/MRN001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
