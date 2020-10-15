using System.Collections.Generic;
using AlertToCareAPI.Models;
using AlertToCareAPI.Database;

namespace AlertToCareAPI.Repositories
{
    public class MonitoringRepository : IMonitoringRepository
    {
        private readonly DatabaseManager _creator = new DatabaseManager();
        private readonly List<Vitals> _vitals;
        public MonitoringRepository()
        {
            this._vitals = _creator.ReadVitalsDatabase();
        }
      
        public IEnumerable<Vitals> GetAllVitals()
        {
            return _vitals;
        }
        public string CheckVitals(Vitals vital)
           {
            var a=CheckSpo2(vital.Spo2);
            var b=CheckBpm(vital.Bpm);
            var c=CheckRespRate(vital.RespRate);
            var s= a + b + c;
            // SendMail(s);
            return s;
           }
        private static string CheckSpo2(float spo2)
        {
            if (spo2 < 90)
            {
               
                return "Spo2 is low, ";
              
            }
            else
                return "";

        }
        private static string CheckBpm(float bpm)
        {
            if (bpm < 70)
                return "bpm is low, ";
            if (bpm > 150)
                return "bpm is high, ";
            else
                return "";
        }
        private static string CheckRespRate(float respRate)
        {
            if (respRate < 30)
                return "respRate is low. ";
            if (respRate > 95)
                return "respRate is high. ";
            else
                return "";
        }
        /*public void SendMail(string body)
        {
             var mailMessage = new MailMessage("alerttocare@gmail.com", "alerttocare@gmail.com");
             mailMessage.Body = body;
             var smtpClient = new SmtpClient("smtp.gmail.com", 587);
             smtpClient.UseDefaultCredentials = true;
             smtpClient.Credentials = new System.Net.NetworkCredential()
             {
                 UserName = "alerttocare@gmail.com",
                 Password = "admin@1234"
             };

             smtpClient.EnableSsl=true;
             smtpClient.Send(mailMessage);
        }*/
    }
}
