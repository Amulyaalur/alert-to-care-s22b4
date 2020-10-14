using AlertToCareAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class IcuOccupancyControllerTest
    {
        readonly string url = "api/IcuOccupancy/Patients";
        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllPatients()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetPatientById()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync(url+"/PID001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenPatientIsAddedIcu()
        {
            var client = new TestClientProvider().Client;

            var patient = new Patient()
                       {
                        PatientId = "PID004",
                        PatientName = "Anita",
                        Age = 25,
                        ContactNo = "7348899805",
                        BedId = "BID4",
                        IcuId = "ICU01",
                        Email = "anita@gmail.com",
                        Address = new PatientAddress() {
                            HouseNo = "97",
                            Street = "joshiyara",
                            City = "Uttarkashi",
                            State = "Uttarakand",
                            Pincode = "249193"
                        },
                        Vitals = new Vitals()
                        {
                            PatientId = "PID004",
                            Spo2 = 100,
                            Bpm = 70,
                            RespRate = 120
                        }
                    };
            
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenUpdatingPatientDetails()
        {
            var client = new TestClientProvider().Client;

            var patient = new Patient()
            {
                PatientId = "PID001",
                PatientName = "Anjali",
                Age = 25,
                ContactNo = "7348899806",
                BedId = "BID1",
                IcuId = "ICU01",
                Email = "anjali@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "97",
                    Street = "joshiyara",
                    City = "Uttarkashi",
                    State = "Uttarakand",
                    Pincode = "249193"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID001",
                    Spo2 = 100,
                    Bpm = 70,
                    RespRate = 120
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url +"/PID001", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckDeletePatient()
        {
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync(url +"/PID001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenPatientIsAddedToNonExistentIcu()
        {
            var client = new TestClientProvider().Client;

            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID4",
                IcuId = "01",
                Email = "anita@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "97",
                    Street = "joshiyara",
                    City = "Uttarkashi",
                    State = "Uttarakand",
                    Pincode = "249193"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID004",
                    Spo2 = 100,
                    Bpm = 70,
                    RespRate = 120
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
   