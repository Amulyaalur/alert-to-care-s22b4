using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareAPI.Repository
{
    public interface IMonitoringRepository
    {
        public string CheckVitals(Entities.Vitals vital);
    
    public IEnumerable<Entities.Vitals> GetAllVitals();
    }

}

