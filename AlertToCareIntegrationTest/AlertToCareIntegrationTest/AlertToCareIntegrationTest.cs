using System.Collections.Generic;
using System.Net;
using RestSharp;
using RestSharp.Serialization.Json;
using Xunit;

namespace AlertToCareIntegrationTest
{
    public class AlertToCareIntegrationTest
    {
        private readonly RestClient _restClient;
        private RestRequest _restRequest;
        private readonly JsonDeserializer _jsonDeserializer;

        public AlertToCareIntegrationTest()
        {
            _restClient = new RestClient("http://localhost:61575/api");
            
            _jsonDeserializer = new JsonDeserializer();
        }

        [Fact]
        public void IntegrationTest1()
        {
            _restRequest = new RestRequest("IcuOccupancy/Patients");
            var result = _restClient.Get(_restRequest);
            var output = _jsonDeserializer.Deserialize<List<Patient>>(result);
            Assert.True(output.Count > 0);
            Assert.True(result.StatusCode==HttpStatusCode.OK);
        }

        [Fact]
        public void IntegrationTest2()
        {
            //Add a new Patient
            _restRequest = new RestRequest("IcuOccupancy/Patient");
            var patient = new Patient
            {
                IcuId = "ICU4",
                Address = "randomAddress",
                Age = 21,
                BedId = "ICU4BED1",
                ContactNumber = "9876543210",
                Email = "email@email.com",
                PatientId = "IntegrationTestPatient",
                PatientName = "IntegrationTest"
            };
            _restRequest.AddJsonBody(patient);
            var result = _restClient.Post(_restRequest);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            //Delete the Patient
            _restRequest = new RestRequest("IcuOccupancy/Patient/IntegrationTestPatient");
            result = _restClient.Delete(_restRequest);
            Assert.True(result.StatusCode==HttpStatusCode.OK);

        }

    }
}
