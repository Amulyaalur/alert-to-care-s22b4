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
        public async Task WhenWrongUrlIsGivenReturnsBadRequest()
        {
            {
                var client = new TestClientProvider().Client;
                var response = await client.GetAsync(url + "/important");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
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
        public async Task CheckStatusCodeEqualBadRequestIfIcuWardDoNotExists()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync(url + "/ICU01");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenIcuWardIsAdded()
        {
            var client = new TestClientProvider().Client;
            var patients = new List<Patient>();
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
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestWhenUpdatingIcuDetailsIfIcuDoNotExists()
        {
            var client = new TestClientProvider().Client;
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
            var response = await client.PutAsync(url + "/I", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkWhenUpdatingIcuDetailsIfExists()
        {
            var client = new TestClientProvider().Client;
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
            var response = await client.PutAsync(url + "/ICU01", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckDeleteIcuReturnsOkIfIcuExists()
        {
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync(url + "/ICU01");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CheckDeleteIcuReturnsOkIfIcuDoNotExists()
        {
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync(url + "/ICU02");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}