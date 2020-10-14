using System.Collections.Generic;
using AlertToCareAPI.Database;
using AlertToCareAPI.Models;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class IcuConfigurationRepository : IIcuConfigurationRepository
    {
        readonly DatabaseManager _creator = new DatabaseManager();
        private readonly List<ICU> _icuList;
        readonly IcuFieldsValidator _validator;


        public IcuConfigurationRepository()
        {

            this._icuList = _creator.GetIcuList();
            this._validator = new IcuFieldsValidator();
        }

        public void AddIcu(ICU newState)
        {
            _validator.ValidateNewIcuId(newState.IcuId, newState, _icuList);
            _icuList.Add(newState);
            _creator.UpdateIcuList(_icuList);
        }
        public void RemoveIcu(string icuId)
        {
            _validator.ValidateOldIcuId(icuId, _icuList);
            for (var i = 0; i < _icuList.Count; i++)
            {
                if (_icuList[i].IcuId == icuId)
                {
                    _icuList.Remove(_icuList[i]);
                    _creator.UpdateIcuList(_icuList);
                    return;
                }
            }
        }
        public void UpdateIcu(string icuId, ICU state)
        {
            _validator.ValidateOldIcuId(icuId, _icuList);
            _validator.ValidateIcuRecord(state);
            
            for (var i = 0; i < _icuList.Count; i++)
            {
                if (_icuList[i].IcuId == icuId)
                {
                    _icuList.Insert(i, state);
                    _creator.UpdateIcuList(_icuList);
                    return;
                }
            }
        }
        public IEnumerable<ICU> GetAllIcu()
        {

            return _icuList;
        }
    }
}
