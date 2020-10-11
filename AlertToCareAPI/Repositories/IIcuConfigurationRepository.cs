using System;
using System.Collections.Generic;
using AlertToCareAPI.ICUDatabase.Entities;

namespace AlertToCareAPI.Repositories
{
    public interface IIcuConfigurationRepository
    {
        void AddIcu(ICU newState);
        void RemoveIcu(String icuId);
        void UpdateIcu(String icuId, ICU state);
        IEnumerable<ICU> GetAllIcu();
    }
}
