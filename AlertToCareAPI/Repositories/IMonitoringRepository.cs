using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IMonitoringRepository
    {
        public string CheckVitals(Vitals vital);
    
        public IEnumerable<Vitals> GetAllVitals();

        public void SendMail(string body);
    }

}

