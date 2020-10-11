using System.Collections.Generic;
using AlertToCareAPI.ICUDatabase.Entities;
using AlertToCareAPI.ICUDatabase;
namespace AlertToCareAPI.Repositories
{
    public class MonitoringRepository : IMonitoringRepository
    {

        readonly IcuContext _db;
        public MonitoringRepository(IcuContext db)
        {
               _db = db;
           /* _db.Add(new Vitals
            {
                Mrn = new Guid("69AA3BA5-D51E-465E-8447-ECAA1939739A"),
                Spo2 = 10,
                Bpm=12,
                RespRate=134
            }) ; */
        }
        public IEnumerable<Vitals> GetAllVitals()
        {
            return _db.Vitals;
        }
        public string CheckVitals(Vitals vital)
           {
            var a=CheckSpo2(vital.Spo2);
            var b=CheckBpm(vital.Bpm);
            var c=CheckRespRate(vital.RespRate);
            var s=a+ b+c;
            return s;
           }
        public string CheckSpo2(float spo2)
        {
                if (spo2 < 90)
                 return "Spo2 is low";
                return "";

        }
        public string CheckBpm(float bpm)
        {
            if (bpm < 70)
                return "bpm is low";
            if (bpm > 150)
                return "bpm is high";
            else
                return "";
        }
        public string CheckRespRate(float respRate)
        {
            if (respRate < 30)
                return "respRate is low";
            if (respRate > 95)
                return "respRate is high";
            else
                return "";
        }
    }
}
