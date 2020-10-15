
using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IIcuConfigurationRepository
    {
        void AddIcu(Icu newState);
        void RemoveIcu(string icuId);
        void UpdateIcu(string icuId, Icu state);
        IEnumerable<Icu> GetAllIcu();
    }
}
