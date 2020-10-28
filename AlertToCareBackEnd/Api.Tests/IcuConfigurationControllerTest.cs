using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.IcuManagement;
using DataModels;
using Xunit;

namespace API.Tests
{
    public class IcuConfigurationControllerTest
    {
        private readonly IIcuManagement _icuManagement;
        public IcuConfigurationControllerTest()
        {
            _icuManagement = new IcuManagementSqLite();
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllIcu()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuConfiguration/Icu");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllLayouts()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuConfiguration/Layouts");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task TestGetIcuByValidIcuId()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuConfiguration/Icu/ICU1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task TestGetIcuByInValidIcuId()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/IcuConfiguration/Icu/ICU-1");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsOkWhenValidIcuIsAdded()
        {
            var icu = GetIcuObject("ICUTEST1");
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuConfiguration/Icu/", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // clean up
            _icuManagement.DeleteIcuById(icu.IcuId);
        }

        [Fact]
        public async Task ReturnsInternalServerErrorWhenInValidIcuIsAdded()
        {
            var icu = GetIcuObject("ICUTEST1");
            icu.LayoutId = "";
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuConfiguration/Icu/", content);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestWhenAddingIcuIfIcuIdExists()
        {
            var icu = GetIcuObject("ICU1");
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("api/IcuConfiguration/Icu/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsOkWhenUpdatingIcuDetails()
        {
            // adding icu 
            var icu = GetIcuObject("ICUTEST2");
            _icuManagement.AddIcu(icu);

            icu.LayoutId = "LID03";
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuConfiguration/Icu/" + icu.IcuId, content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // clean up
            _icuManagement.DeleteIcuById(icu.IcuId);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingIcuDetailsIfIcuDoNotExists()
        {
            var icu = GetIcuObject("ICUTEST3");
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuConfiguration/Icu/" + "ICUTEST3", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsInternalServerErrorWhenUpdatingInvalidIcuDetails()
        {
            var icu = GetIcuObject("ICUTEST4");
            _icuManagement.AddIcu(icu);

            icu.LayoutId = "";
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");

            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/IcuConfiguration/Icu/" + icu.IcuId, content);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            // clean up
            _icuManagement.DeleteIcuById(icu.IcuId);
        }

        [Fact]
        public async Task CheckDeleteIcuReturnsOkIfIcuExists()
        {
            var icu = GetIcuObject("ICUTEST5");
            _icuManagement.AddIcu(icu);

            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/IcuConfiguration/Icu/" + icu.IcuId);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckDeleteIcuReturnsBadRequestIfIcuDoesNotExists()
        {
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/IcuConfiguration/Icu/" + "random");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private Icu GetIcuObject(string icuId)
        {
            var icu = new Icu()
            {
                IcuId = icuId,
                LayoutId = "LID01",
                BedsCount = 2
            };
            return icu;
        }
    }
}

