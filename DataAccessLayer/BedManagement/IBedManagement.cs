using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.BedManagement
{
    public interface IBedManagement
    {
        public IEnumerable<Bed> GetAllAvailableBedsByIcuId(string icuId);
    }
}
