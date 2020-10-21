using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.IcuManagement
{
    public interface IIcuManagement
    {
        public IEnumerable<Icu> GetAllIcu();
        public Icu GetIcuById(string icuId);
        public void AddIcu(Icu icu);
        public bool UpdateIcuById(string icuId, Icu icu);
        public bool DeleteIcuById(string icuId);

    }
}