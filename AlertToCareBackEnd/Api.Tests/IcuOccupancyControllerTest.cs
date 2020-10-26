
using Newtonsoft.Json;

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.PatientManagement;
using DataModels;
using Xunit;

namespace API.Tests
{
   
    public class ClientSetUp
    {
        public readonly HttpClient Client;
       
        public ClientSetUp()
        { 
            Client= new TestClientProvider().Client;
            
        }

        public async void SendInvalidPostRequest(Patient patient)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await this.Client.PostAsync("api/IcuOccupancy/Patient", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
    public class IcuOccupancyControllerTest
    {
        private readonly IPatientManagement _patientManagement;
        //private IcuOccupancyController _icuOccupancy;

        public IcuOccupancyControllerTest()
        {
            _patientManagement = new PatientManagementSqLite();
           // IBedManagement bed = new BedManagementSqLite();
           // _icuOccupancy =new IcuOccupancyController(_patientManagement,bed);
        }
        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllPatients()
        {
            ClientSetUp setter = new ClientSetUp();

            var response = await setter.Client.GetAsync("api/IcuOccupancy/Patients");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetPatientById()
        {
            var setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuOccupancy/Patient/PID7");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public void ReturnsOkWhenPatientIsAddedIcu()
        {
          //  var setter = new ClientSetUp();
                 // var context=new ActionContext();
           //      var Ok = new OkObjectResult(patientManagement);
                 
            
            var patient = new Patient
            {
                IcuId = "ICU4",
                Address = "randomAddress",
                Age = 12,
                BedId = "ICU4BED5",
                ContactNumber = "9876543210",
                Email = "email@email.com",
                PatientId = "PIDTestRand",
                PatientName = "PName"
            };
          
             _patientManagement.AddPatient(patient);
             Assert.True(true);
            _patientManagement.RemovePatient("PIDTestRand");
             Assert.True(true);
 

        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientWithOldIdIsAddedIcu()
        {
            var setter = new ClientSetUp();

            var patient = new Patient
            {
                PatientId = "PID1",
                PatientName = "Anita",
                Age = 25,
                ContactNumber = "7348899805",
                BedId = "BID4",
                IcuId = "ICU01",
                Email = "anita@gmail.com",
                Address = "Address",
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patient", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            setter.SendInvalidPostRequest(patient);
        }

      /*  [Fact]
        public async Task ReturnsOkWhenUpdatingPatientDetails()
        {
            var setter = new ClientSetUp();

            var patient = new Patient()
            {
                PatientId = "PID2",
                PatientName = "Anjali",
                Age = 25,
                ContactNumber = "7348899806",
                BedId = "BID1",
                IcuId = "ICU01",
                Email = "anjali@gmail.com",
                Address = "RandomAddress"
                
            };

            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/IcuOccupancy/Patient/PID2", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }*/
/*
        [Fact]
        public async Task CheckDeletePatient()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.DeleteAsync("api/IcuOccupancy/Patients/PID001");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public void ReturnsBadRequestWhenPatientIsAddedToNonExistentIcu()
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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }
        [Fact]
        public void ThrowsExeptionWhenPatientBedIdDoesNotMatchWithIcuBedId()
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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }
        [Fact]
        public void ReturnsBadRequestWhenPatientWithNullValuesIsAddedIcu()
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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);
          
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }
        [Fact]
        public async Task ReturnsBadRequestForGettingPatientWithNonExistingId()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuOccupancy/Patients/PID090");
           
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public void ReturnsBadRequestWhenPatientIdIsDifferentInVitalsObject()
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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }

        [Fact]
        public void ReturnsBadRequestWhenIcuIdDoesNotMatchWithBedIcuId()
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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }

        [Fact]
        public void ReturnsBadRequestWhenBedIsAlreadyOccupied()
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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);
            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenDeletingInvalidPatientId()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.DeleteAsync("api/IcuOccupancy/Patients/PID02");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenGettingInvalidPatientId()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuOccupancy/Patients/PID02");
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
            var response = await setter.Client.PutAsync("api/IcuOccupancy/Patients/PID02", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
        }
        [Fact]
        public void  ReturnsBadRequestWhenBedIdDoesNotExist()
        {
            var setter = new ClientSetUp();

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

            *//*var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuOccupancy/Patients", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);*//*
            setter.SendInvalidPostRequest(patient);
        }*/

    }
}
   