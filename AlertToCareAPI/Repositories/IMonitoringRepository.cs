using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IMonitoringRepository
    {
        public string CheckVitals(Vitals vital);
    
        public IEnumerable<Vitals> GetAllVitals();
        public string CheckSpo2(float spo2);
        public string CheckBpm(float bpm);
        public string CheckRespRate(float respRate);
    }

}

