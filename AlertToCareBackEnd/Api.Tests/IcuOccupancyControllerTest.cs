
using System;
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
    public class IcuOccupancyControllerTest
    {
        private readonly IPatientManagement _patientManagement;
        public IcuOccupancyControllerTest()
        { 
            _patientManagement = new PatientManagementSqLite();
        }
        [Fact]
        public async Task CheckStatusCodeEqualsOkGetAllPatients()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Patients");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestGetPatientByIdWithValidPatientId()
        {
            var client = new TestClientProvider().Client;
            var validPatientId = "PID1";
            var response = await client.GetAsync("api/IcuOccupancy/Patient/" + validPatientId);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestGetPatientByIdWithInValidPatientId()
        {
            var client = new TestClientProvider().Client;
            var invalidPatientId = "PID999";
            var response = await client.GetAsync("api/IcuOccupancy/Patient/" + invalidPatientId);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void GetAllAvailableBedsByIcuIdReturnsOkTest()
        {
            var validIcuId = "ICU1";
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Beds/" + validIcuId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Fact]
        public async void GetAllAvailableBedsByIcuIdReturnsBadRequestTest()
        {
            var invalidIcuId = "ICU9999";
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Beds/" + invalidIcuId);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
        [Fact]
        public async void ReturnsOkWhenValidPatientIsAddedToIcu()
        {
            var patient = GetPatientObject();
            patient.BedId = "ICU1BED3";
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // clean up
            _patientManagement.RemovePatient(patient.PatientId);

        }
        [Fact]
        public async void ReturnsBadRequestWhenInValidPatientModelIsAddedToIcu()
        {
            var patient = GetPatientObject();
            patient.PatientName = "";
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientWithExistingPatientIdIsAddedToIcu()
        {
            var patient = GetPatientObject();
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            _ =  client.PostAsync("api/IcuOccupancy/Patient/", content);

            // adding the same patient again
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // clean up
            _ = client.DeleteAsync("api/IcuOccupancy/Patient/" + patient.PatientId);
        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientIsAddedToNonExistentIcu()
        {
            var patient = GetPatientObject();
            patient.IcuId = "random";
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenUpdatingPatientDetails()
        {
            var patient = GetPatientObject();
            patient.BedId = "ICU1BED6";
            _patientManagement.AddPatient(patient);

            // updating the patient information
            patient.Age = 99;
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuOccupancy/Patient/" + patient.PatientId, content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // clean up
            _patientManagement.RemovePatient(patient.PatientId);
        }
        [Fact]
        public async Task CheckDeletePatient()
        {
            // adding a patient
            var patient = GetPatientObject();
            patient.BedId = "ICU1BED5";
            _patientManagement.AddPatient(patient);
            // deleting the patient

            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/IcuOccupancy/Patient/" + patient.PatientId);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestForGettingPatientWithNonExistingId()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Patient/PID090");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        private Patient GetPatientObject()
        {
            var patient = new Patient
            {
                IcuId = "ICU1",
                Address = "randomAddress",
                Age = 12,
                BedId = "ICU1BED2",
                ContactNumber = "9876543210",
                Email = "email@email.com",
                PatientId = "PIDTestRand" + new Random().Next(2000, 4000),
                PatientName = "PName"
            };

            return patient;
        }
    }
}
   