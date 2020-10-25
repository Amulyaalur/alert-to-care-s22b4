using System;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataModels;
using Xunit;

namespace API.Tests
{
    public class IcuConfigurationControllerTest
    {
        public IcuConfigurationControllerTest()
        {
            var icu = new Icu()
            {
                IcuId = "ICUTest",
                LayoutId = "LID04",
                BedsCount = 2,

            };
            var x=AddIcu(icu);
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllIcuWards()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuConfiguration/Icu");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenWrongUrlIsGivenReturnsBadRequest()
        {
            {
                ClientSetUp setter = new ClientSetUp();
                var response = await setter.Client.GetAsync("api/IcuConfiguration/Icu/important");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkIfIcuWardExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuConfiguration/Icu/ICU1");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckStatusCodeEqualBadRequestIfIcuWardDoNotExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuConfiguration/Icu/ICU-1");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void ReturnsOkWhenIcuWardIsAdded()
        {

            var random = new Random().Next();
            var icu = new Icu() {BedsCount = 4, IcuId = random.ToString(), LayoutId = "LID01"};
            var response = AddIcu(icu);
            
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);

            var respo =DeleteIcu(icu.IcuId);
            Assert.Equal(HttpStatusCode.OK, respo.Result.StatusCode);

        }



        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingIcuDetailsIfIcuDoNotExists()
        {
            var setter = new ClientSetUp();

            var icu = new Icu()
            {
                IcuId = "ICU--1",
                LayoutId = "LID04",
                BedsCount = 2,

            };
           // var x=AddIcu(icu);
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/IcuConfiguration/Icu/asd", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

          // var d=DeleteIcu(icu.IcuId);
        }

        [Fact]
        public void ReturnsOkWhenUpdatingIcuDetailsIfExists()
        {
            //ClientSetUp setter = new ClientSetUp();


            var icu = new Icu()
            {
                IcuId = "ICU01",
                LayoutId = "LID01",
                BedsCount = 7,

            };
            var x = AddIcu(icu);
          //  Thread.Sleep(500);

            var response = UpdateIcu(icu);


            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
          //  Thread.Sleep(500);
          //  var d=  DeleteIcu(icu.IcuId);
        }

        [Fact]
        public void CheckDeleteIcuReturnsOkIfIcuExists()
        {
            var icu = new Icu()
            {
                IcuId = "ICUTest",
                LayoutId = "LID04",
                BedsCount = 2,

            };
           
            var response = DeleteIcu(icu.IcuId);
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Fact]
        public void CheckDeleteIcuReturnsBadRequestIfIcuDoNotExists()
        {
            /*ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.DeleteAsync("api/IcuConfiguration/Icu/ICU");*/
            var response=DeleteIcu("randomIcu");
            Assert.Equal(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }

        [Fact]
        public void ReturnsBadRequestWhenIcuWardWithOldIdIsAdded()
        {
            //ClientSetUp setter = new ClientSetUp();
            

            var icu = new Icu()
            {
                IcuId = "ICU1",
                LayoutId = "LID02",
                BedsCount = 2,
               
            };
            var response = AddIcu(icu);
            /*var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuConfiguration/IcuWards", content);*/
            Assert.Equal(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }

        private async Task<HttpResponseMessage> AddIcu(Icu icu)
        {
            var setter = new ClientSetUp();
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuConfiguration/Icu", content);
           // response.EnsureSuccessStatusCode();
            return response;

        }

        private async Task<HttpResponseMessage> DeleteIcu(string icuId)
        {
            var setter = new ClientSetUp();
            var response = await setter.Client.DeleteAsync("api/IcuConfiguration/Icu/"+icuId);
           // response.EnsureSuccessStatusCode();
          //  Assert.Equal(HttpStatusCode.OK, response.StatusCode);
          return response;
        }
        private async Task<HttpResponseMessage> UpdateIcu(Icu icu)
        {
            var setter = new ClientSetUp();
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/IcuConfiguration/Icu/" + icu.IcuId, content);
         //   response.EnsureSuccessStatusCode();
            //  Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            return response;
        }
    }
}

