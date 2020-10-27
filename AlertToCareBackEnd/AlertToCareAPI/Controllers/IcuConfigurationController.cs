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
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
        }

        [HttpGet(template: "Layouts")]
        public IActionResult GetAllLayouts()
        {
            return Ok(_layoutDb.GetAllLayouts());
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
        }
    }
}
