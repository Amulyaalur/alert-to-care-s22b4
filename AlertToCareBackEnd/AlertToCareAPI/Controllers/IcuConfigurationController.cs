using System;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.IcuManagement;
using DataAccessLayer.LayoutManagement;
using DataModels;

namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcuConfigurationController : ControllerBase
    {
        private readonly IIcuManagement _icuDb;
        private readonly ILayoutManagement _layoutDb;
        public IcuConfigurationController(IIcuManagement icu, ILayoutManagement layoutDb)
        {
            _icuDb = icu;
            _layoutDb = layoutDb;
        }

        [HttpGet("Icu")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_icuDb.GetAllIcu());
            }
            
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
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
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
            catch(Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet(template: "Layouts")]
        public IActionResult GetAllLayouts()
        {
            try
            {
                return Ok(_layoutDb.GetAllLayouts());
            }
            catch(Exception exception)
            {
                return StatusCode(400, exception.Message);
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
                return new ObjectResult(exception.Message) {StatusCode = 400};
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("Icu/{IcuId}")]
        public IActionResult Put(string icuId, [FromBody] Icu icu)
        {
            try
            {
                _icuDb.UpdateIcuById(icuId, icu);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpDelete("Icu/{IcuId}")]
        public IActionResult Delete(string icuId)
        {
            try
            {
                _icuDb.DeleteIcuById(icuId);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}
