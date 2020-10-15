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
       
        [Fact]
        public async Task CheckStatusCodeEqualOkGetAllIcuWards()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuConfiguration/IcuWards");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task WhenWrongUrlIsGivenReturnsBadRequest()
        {
            {
                ClientSetUp setter = new ClientSetUp();
                var response = await setter.Client.GetAsync("api/IcuConfiguration/IcuWards/important");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task CheckStatusCodeEqualOkIfIcuWardExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuConfiguration/IcuWards/ICU01");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CheckStatusCodeEqualBadRequestIfIcuWardDoNotExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.GetAsync("api/IcuConfiguration/IcuWards/ICU04");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenIcuWardIsAdded()
        {
            ClientSetUp setter = new ClientSetUp();
            var beds = new List<Bed>()
            {
                new Bed
                {
                    BedId = "BID8",
                    Status = false,
                    IcuId = "ICU02"
                },
                new Bed
                {
                    BedId = "BID9",
                    Status = false,
                    IcuId = "ICU02"
                },
                new Bed
                {
                    BedId = "BID10",
                    Status = false,
                    IcuId = "ICU02"
                },
                new Bed
                {
                    BedId = "BID11",
                    Status = false,
                    IcuId = "ICU02"
                },
                new Bed
                {
                    BedId = "BID12",
                    Status = false,
                    IcuId = "ICU02"
                }
            };

            var icu = new Icu()
            {
                IcuId = "ICU02",
                LayoutId = "LID01",
                BedsCount = 5,
                Beds = beds
            };
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuConfiguration/IcuWards", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingIcuDetailsIfIcuDoNotExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var beds = new List<Bed>()
            {
                new Bed
                {
                    BedId = "B01",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "B02",
                    Status = true,
                    IcuId = "ICU01"
                }
            };

            var icu = new Icu()
            {
                IcuId = "ICU05",
                LayoutId = "LID04",
                BedsCount = 2,
                Beds = beds
            };

            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/IcuConfiguration/IcuWards/I", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenUpdatingIcuDetailsIfExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var beds = new List<Bed>()
            {
                new Bed
                {
                    BedId = "BID1",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "BID2",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "BID3",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "BID4",
                    Status = false,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "BID5",
                    Status = false,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "BID6",
                    Status = false,
                    IcuId = "ICU01"
                },
                new Bed
                {
                    BedId = "BID7",
                    Status = false,
                    IcuId = "ICU01"
                }
            };

            var icu = new Icu()
            {
                IcuId = "ICU01",
                LayoutId = "LID01",
                BedsCount = 7,
                Beds = beds
            };

            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PutAsync("api/IcuConfiguration/IcuWards/ICU01", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckDeleteIcuReturnsOkIfIcuExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.DeleteAsync("api/IcuConfiguration/IcuWards/ICU01");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CheckDeleteIcuReturnsOkIfIcuDoNotExists()
        {
            ClientSetUp setter = new ClientSetUp();
            var response = await setter.Client.DeleteAsync("api/IcuConfiguration/IcuWards/ICU02");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsBadRequestWhenIcuWardWithOldIdIsAdded()
        {
            ClientSetUp setter = new ClientSetUp(); 
            var beds = new List<Bed>()
            {
                new Bed
                {
                    BedId = "ID4",
                    Status = false,
                    IcuId = "ICU02"
                },
                new Bed
                {
                    BedId = "ID5",
                    Status = false,
                    IcuId = "ICU02"
                }
            };

            var icu = new Icu()
            {
                IcuId = "ICU01",
                LayoutId = "LID02",
                BedsCount = 2,
                Beds = beds
            };
            var content = new StringContent(JsonConvert.SerializeObject(icu), Encoding.UTF8, "application/json");
            var response = await setter.Client.PostAsync("api/IcuConfiguration/IcuWards", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

}