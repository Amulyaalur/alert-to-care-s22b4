using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.IcuManagement
{
    public interface IIcuManagement
    {
        public IEnumerable<Icu> GetAllIcu();
        public Icu GetIcuById(string icuId);
        public void AddIcu(Icu icu);
        public void UpdateIcuById(string icuId, Icu icu);
        public void DeleteIcuById(string icuId);

    }
}