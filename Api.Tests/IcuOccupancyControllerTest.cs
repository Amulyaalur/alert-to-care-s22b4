using AlertToCareAPI.Models;
using Newtonsoft.Json;

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ClientSetUp
    {
        public HttpClient client;
        public ClientSetUp()
        { 
            this.client= new TestClientProvider().Client;
        }

    }
    public class IcuOccupancyControllerTest
    {   

  
        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllPatients()
        {
            ClientSetUp setter = new ClientSetUp();

            var response = await setter.client.GetAsync("api/IcuOccupancy/Patients");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetPatientById()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.client.GetAsync("api/IcuOccupancy/Patients/PID001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenPatientIsAddedIcu()
        {
            ClientSetUp setter = new ClientSetUp();
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenPatientWithOldIdIsAddedIcu()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID001",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID4",
                IcuId = "ICU01",
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
          
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsOkWhenUpdatingPatientDetails()
        {
            ClientSetUp setter = new ClientSetUp();

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
            var response = await setter.client.PutAsync("api/IcuOccupancy/Patients/PID001", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckDeletePatient()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.client.DeleteAsync("api/IcuOccupancy/Patients/PID001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenPatientIsAddedToNonExistentIcu()
        {
            ClientSetUp setter = new ClientSetUp();

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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ThrowsExeptionWhenPatientBedIdDoesNotMatchWithIcuBedId()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID9",
                IcuId = "ICU01",
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenPatientWithNullValuesIsAddedIcu()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = null,
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID4",
                IcuId = "ICU01",
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
          
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestForGettingPatientWithNonExistingId()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.client.GetAsync("api/IcuOccupancy/Patients/PID090");
           
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenPatientIdIsDifferentInVitalsObject()
        {
            ClientSetUp setter = new ClientSetUp();
            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID4",
                IcuId = "ICU01",
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
                    PatientId = "PID006",
                    Spo2 = 100,
                    Bpm = 70,
                    RespRate = 120
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestWhenIcuIdDoesNotMatchWithBedIcuId()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID50",
                IcuId = "ICU01",
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestWhenBedIsAlreadyOccupied()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID3",
                IcuId = "ICU01",
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);
            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenDeletingInvalidPatientId()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.client.DeleteAsync("api/IcuOccupancy/Patients/PID02");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenGettingInvalidPatientId()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.client.GetAsync("api/IcuOccupancy/Patients/PID02");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingInvalidPatientId()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID02",
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
                    PatientId = "PID02",
                    Spo2 = 100,
                    Bpm = 70,
                    RespRate = 120
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.client.PutAsync("api/IcuOccupancy/Patients/PID02", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenBedIdDoesNotExist()
        {
            ClientSetUp setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID004",
                PatientName = "Anita",
                Age = 25,
                ContactNo = "7348899805",
                BedId = "BID67",
                IcuId = "ICU01",
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
            var response = await setter.client.PostAsync("api/IcuOccupancy/Patients", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
   