
using System.Collections.Generic;
using AlertToCareAPI.ICUDatabase.Entities;
using AlertToCareAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcuConfigurationController : ControllerBase
    {
        readonly IIcuConfigurationRepository _configurationRepository;
        public IcuConfigurationController(IIcuConfigurationRepository repo)
        {
            this._configurationRepository = repo;
        }
        // GET: api/<IcuConfigurationController>
        [HttpGet("IcuWards")]
        public IEnumerable<ICU> Get()
        {
            return _configurationRepository.GetAllIcu();
        }

        [HttpGet("IcuWards/{IcuId}")]
        public ICU Get(string icuId)
        {
            var icuStore = _configurationRepository.GetAllIcu();
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    return icu;
                }
            }
            return null;
        }

        [HttpPost("IcuWards")]
        public void Post([FromBody] ICU icu)
        {
            _configurationRepository.AddIcu(icu);
        }

        [HttpPut("IcuWards/{IcuId}")]
        public void Put(string icuId, [FromBody] ICU icu)
        {
            _configurationRepository.UpdateIcu(icuId, icu);
        }

        [HttpDelete("IcuWards/{IcuId}")]
        public void Delete(string icuId)
        {
            _configurationRepository.RemoveIcu(icuId);
        }
    }
}
