using System.Collections.Generic;
using AlertToCareAPI.ICUDatabase.Entities;

namespace AlertToCareAPI.Repositories
{
    public interface IMonitoringRepository
    {
        public string CheckVitals(Vitals vital);
        public IEnumerable<Vitals> GetAllVitals();
    }

}

