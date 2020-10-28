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
            var invalidPatientId = "random";
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
            var invalidIcuId = "random";
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuOccupancy/Beds/" + invalidIcuId);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
        [Fact]
        public async void ReturnsOkWhenValidPatientIsAddedToIcu()
        {
            var patient = GetPatientObject("1","ICU1", "ICU1BED2");
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
            var patient = GetPatientObject("2","ICU1", "ICU1BED2");
            patient.PatientName = "";
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientWithExistingPatientIdIsAddedToIcu()
        {
            var patient = GetPatientObject("3","ICU1", "ICU1BED3");
            _patientManagement.AddPatient(patient);

            // adding the same patient again
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // clean up
           _patientManagement.RemovePatient(patient.PatientId);
        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientIsAddedToNonExistentIcu()
        {
            var patient = GetPatientObject("4","random", "ICU1BED3");
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientIsAddedToNonExistentBed()
        {
            var patient = GetPatientObject("5","ICU1", "random");
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async void ReturnsBadRequestWhenPatientIsAddedToOccupiedBed()
        {
            var patient = GetPatientObject("6","ICU1", "ICU1BED1");
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuOccupancy/Patient/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenUpdatingPatientDetails()
        {
            var patient = GetPatientObject("7","ICU1", "ICU1BED4");
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
        public async Task ReturnsInternalServerErrorWhenUpdatingInvalidPatientDataModel()
        {
            var patient = GetPatientObject("8", "ICU1", "ICU1BED5");
            _patientManagement.AddPatient(patient);

            // updating the patient information
            patient.Address = "";
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuOccupancy/Patient/" + patient.PatientId, content);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            // clean up
            _patientManagement.RemovePatient(patient.PatientId);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingPatientWithNonExistingPatientId()
        {
            var patient = GetPatientObject("randomId","ICU1", "ICU1BED5");
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            
            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuOccupancy/Patient/" + patient.PatientId, content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingPatientIdDoesNotMatch()
        {
            var patient = GetPatientObject("9", "ICU1", "ICU1BED5");
            patient.PatientId = "PIDTest-9";
            var content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuOccupancy/Patient/" + patient.PatientId, content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task CheckDeletePatient()
        {
            // adding a patient
            var patient = GetPatientObject("10", "ICU1", "ICU1BED6");
            _patientManagement.AddPatient(patient);
            
            // deleting the patient
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/IcuOccupancy/Patient/" + patient.PatientId);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CheckDeletePatientWithInvalidPatientId()
        {
            // deleting the patient
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/IcuOccupancy/Patient/" + "random");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        private Patient GetPatientObject(string pId, string icuId, string bedId)
        {
            var patient = new Patient
            {
                IcuId = icuId,
                Address = "randomAddress",
                Age = 12,
                BedId = bedId,
                ContactNumber = "9876543210",
                Email = "email@email.com",
                PatientId = "PIDTest" + pId,
                PatientName = "PName"
            };

            return patient;
        }
    }
}
   