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
            _context.Add<ICU>(newState);
            _context.SaveChanges();
        }
        public void RemoveIcu(string icuId)
        {
            _validator.ValidateOldIcuId(icuId);
            var icuStore = _context.IcuList.ToList();
            for (var i = 0; i < icuStore.Count; i++)
            {
                if (icuStore[i].IcuId == icuId)
                {
                    _context.Remove(_context.IcuList.Single(a => a.IcuId == icuId));
                    _context.SaveChanges();
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
                    _context.Remove(_context.IcuList.Single(a => a.IcuId == icuId));
                    _context.Add<ICU>(state);
                    _context.SaveChanges();
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
