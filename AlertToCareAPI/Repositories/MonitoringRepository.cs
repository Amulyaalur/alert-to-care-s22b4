using System.Collections.Generic;
using AlertToCareAPI.Models;
using System.Net.Mail;
using System;
using AlertToCareAPI.Database;

namespace AlertToCareAPI.Repositories
{
    public class MonitoringRepository : IMonitoringRepository
    {
        readonly DatabaseCreator _creator = new DatabaseCreator();
        readonly List<Vitals> _vitals;
        public MonitoringRepository()
        {
            this._vitals = _creator.GetVitalsList();
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
            var s= a + "," +b + ","+ c;
            SendMail(s);
            return s;
           }
        public string CheckSpo2(float spo2)
        {
            if (spo2 < 90)
            {
               
                return "Spo2 is low ";
              
            }
            else
                return "";

        }
        public string CheckBpm(float bpm)
        {
            if (bpm < 70)
                return "bpm is low ";
            if (bpm > 150)
                return "bpm is high ";
            else
                return "";
        }
        public string CheckRespRate(float respRate)
        {
            if (respRate < 30)
                return "respRate is low ";
            if (respRate > 95)
                return "respRate is high ";
            else
                return "";
        }
        public void SendMail(string body)
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
             try
             {
                 smtpClient.Send(mailMessage);
             }

             catch (Exception ex)
             {
                 throw ex;
             }
           
        }
        

    }
}
