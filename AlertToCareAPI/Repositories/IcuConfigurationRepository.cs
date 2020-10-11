using System.Collections.Generic;
using System.Linq;
using AlertToCareAPI.ICUDatabase.Entities;
using AlertToCareAPI.ICUDatabase;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class IcuConfigurationRepository : IIcuConfigurationRepository
    {
        readonly IcuContext _context = new IcuContext();
        readonly IcuFieldsValidator _validator = new IcuFieldsValidator();
        public void AddIcu(ICU newState)
        {
            _validator.ValidateNewIcuId(newState.IcuId, newState);
            var icuStore= _context.IcuList.ToList();
            icuStore.Add(newState);
        }
        public void RemoveIcu(string icuId)
        {
            _validator.ValidateOldIcuId(icuId);
            var icuStore = _context.IcuList.ToList();
            for (var i = 0; i < icuStore.Count; i++)
            {
                if (icuStore[i].IcuId == icuId)
                {
                    icuStore.Remove(icuStore[i]);
                    return;
                }
            }
        }
        public void UpdateIcu(string icuId, ICU state)
        {
            _validator.ValidateOldIcuId(icuId);
            _validator.ValidateIcuRecord(state);
            var icuStore = _context.IcuList.ToList();
            for (var i = 0; i < icuStore.Count; i++)
            {
                if (icuStore[i].IcuId == icuId)
                {
                    icuStore.Insert(i, state);
                    return;
                }
            }
        }
        public IEnumerable<ICU> GetAllIcu()
        {
            var icuStore = _context.IcuList.ToList();
            return icuStore;
        }
    }
}
