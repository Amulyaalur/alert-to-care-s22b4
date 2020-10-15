using System.Collections.Generic;
using AlertToCareAPI.Database;
using AlertToCareAPI.Models;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class IcuConfigurationRepository : IIcuConfigurationRepository
    {
        private readonly DatabaseManager _creator = new DatabaseManager();
        private readonly IcuFieldsValidator _validator = new IcuFieldsValidator();


        public void AddIcu(Icu newState)
        {
            var icuList = _creator.ReadIcuDatabase();
            _validator.ValidateNewIcuId(newState.IcuId, newState, icuList);
            icuList.Add(newState);
            _creator.WriteToIcuDatabase(icuList);
        }
        public void RemoveIcu(string icuId)
        {
            var icuList = _creator.ReadIcuDatabase();
            _validator.ValidateOldIcuId(icuId, icuList);
            for (var i = 0; i < icuList.Count; i++)
            {
                if (icuList[i].IcuId == icuId)
                {
                    icuList.Remove(icuList[i]);
                    _creator.WriteToIcuDatabase(icuList);
                    return;
                }
            }
        }
        public void UpdateIcu(string icuId, Icu state)
        {
            var icuList = _creator.ReadIcuDatabase();
            _validator.ValidateOldIcuId(icuId, icuList);
            _validator.ValidateIcuRecord(state);
            
            for (var i = 0; i < icuList.Count; i++)
            {
                if (icuList[i].IcuId == icuId)
                {
                    icuList.Insert(i, state);
                    _creator.WriteToIcuDatabase(icuList);
                    return;
                }
            }
        }
        public IEnumerable<Icu> GetAllIcu()
        {
            var icuList = _creator.ReadIcuDatabase();
            return icuList;
        }
    }
}
