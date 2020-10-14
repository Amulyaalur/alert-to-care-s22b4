
using AlertToCareAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class IcuConfigurationControllerTest
    {
        readonly string url = "api/IcuConfiguration/IcuWards";
        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllIcuWards()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkIfIcuWardExists()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync(url + "/ICU01");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task ReturnsOkWhenIcuWardIsAdded()
        {
            var client = new TestClientProvider().Client;
            List<Patient> patients = new List<Patient>();
           
               var patient1=new Patient()
               { 
                   PatientId = "PID005",
                PatientName = "Anirudh",
                Age = 45,
                ContactNo = "9806458790",
                BedId = "BID8",
                IcuId = "ICU02",
                Email = "anirudh@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "A1",
                    Street = "rajpur",
                    City = "Dehradun",
                    State = "Uttarakand",
                    Pincode = "249001"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID005",
                    Spo2 = 102,
                    Bpm = 17,
                    RespRate = 120
                }
            };
            patients.Add(patient1);
            var icu = new ICU()
            {
                IcuId = "ICU02",
                LayoutId = "LID01",
                BedsCount = 8,
                Patients = patients
            };
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenUpdatingIcuDetails()
        {
            var client = new TestClientProvider().Client;
            List<Patient> patients = new List<Patient>();
            var patient1=new Patient()
            { 
            PatientId = "PID005",
                PatientName = "Anirudh",
                Age = 45,
                ContactNo = "9806458790",
                BedId = "BID8",
                IcuId = "ICU02",
                Email = "anirudh@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "A1",
                    Street = "rajpur",
                    City = "Dehradun",
                    State = "Uttarakand",
                    Pincode = "249001"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID005",
                    Spo2 = 102,
                    Bpm = 17,
                    RespRate = 90
                }
            };
            
            var icu = new ICU()
            {
                IcuId = "ICU02",
                LayoutId = "LID01",
                BedsCount = 9,
                Patients = patients
            };

            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url +"/ICU02", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckDeleteICU()
        {
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync(url + "/ICU01");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
    }
}
