using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.IcuManagement;
using DataModels;


namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcuConfigurationController : ControllerBase
    {
        private readonly IIcuManagement _icuDb;
        public IcuConfigurationController(IIcuManagement icu)
        {
            _icuDb = icu;
        }

        [HttpGet("Icu")]
        public IActionResult Get()
        {
            return Ok(_icuDb.GetAllIcu());
        }

        [HttpGet("Icu/{IcuId}")]
        public IActionResult Get(string icuId)
        {
            try
            {
                return Ok(_icuDb.GetIcuById(icuId));
            }
            catch (SQLiteException exception)
            {
                return StatusCode(500, exception);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Icu")]
        public IActionResult Post([FromBody] Icu icu)
        {
            try
            {
                _icuDb.AddIcu(icu);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return StatusCode(500, exception);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Icu/{IcuId}")]
        public IActionResult Put(string icuId, [FromBody] Icu icu)
        {
            try
            {
                return Ok(_icuDb.UpdateIcuById(icuId, icu));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("Icu/{IcuId}")]
        public IActionResult Delete(string icuId)
        {
            try
            {
                return Ok(_icuDb.DeleteIcuById(icuId));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
