using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.AlertManagement;
using DataModels;
using Newtonsoft.Json;
using Xunit;
namespace API.Tests
{
    public class PatientMonitoringControllerTest
    {
        [Fact]
        public async Task TestGetAllVitals()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring/Vitals");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestGetAllAlerts()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring/Alerts");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateVitalsByValidPatientIdReturnOk()
        {
            var vital = new Vital()
            {
                Bpm = 180,
                PatientId = "PID4",
                RespRate = 41,
                Spo2 = 98
            };

            var setter = new ClientSetUp();
            var content = new StringContent(JsonConvert.SerializeObject(vital), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/PatientMonitoring/Vital/" + vital.PatientId, content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateVitalsByInValidPatientIdReturnBadRequest()
        {
            var vital = new Vital()
            {
                Bpm = 180,
                PatientId = "PID11111",
                RespRate = 41,
                Spo2 = 98
            };

            var setter = new ClientSetUp();
            var content = new StringContent(JsonConvert.SerializeObject(vital), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/PatientMonitoring/Vital/" + vital.PatientId, content);
           // response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestToggleAlert()
        {
            /*var vital = new Vital()
            {
                Bpm = 180,
                PatientId = "PID1",
                RespRate = 41,
                Spo2 = 120
            };*/

            var setter = new ClientSetUp();
           // var content = new StringContent(JsonConvert.SerializeObject(), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/PatientMonitoring/Alert/6",null);
            // response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAlert()
        {
            var x=new AlertManagementSqLite();
            var pid = "PID7";
            var alertId=0;
            AlertManagementSqLite.AddToAlertsTable(pid);

            var alerts = x.GetAllAlerts();
            foreach (var alert in alerts)
            {
                if (alert.PatientId == pid)
                {
                    alertId = alert.AlertId;
                }
            }
            var setter = new ClientSetUp();
            // var content = new StringContent(JsonConvert.SerializeObject(), Encoding.UTF8, "application/json");
            var response = await setter.Client.DeleteAsync("api/PatientMonitoring/Alert/"+alertId);
             response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
