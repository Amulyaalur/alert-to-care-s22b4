using DataModels;
using System.Data.SQLite;

namespace DataAccessLayer.Utils
{
    public static class VitalsChecker
    {
        public static bool CheckAllVitals(Vital vital)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT 
                                       MinBpm,
                                       MaxBpm,
                                       MinSpo2,
                                       MaxSpo2,
                                       MinRespRate,
                                       MaxRespRate
                                  FROM Vitals
                                  WHERE PatientId = @PatientId"
            };
            cmd.Parameters.AddWithValue("@PatientId", vital.PatientId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            reader.Read();


            var minBpm = reader.GetFloat(0);
            var maxBpm = reader.GetFloat(1);
            var minSpo2 = reader.GetFloat(2);
            var maxSpo2 = reader.GetFloat(3);
            var minRespRate = reader.GetFloat(4);
            var maxRespRate = reader.GetFloat(5);

            reader.Dispose();
            con.Dispose();
           
            
            
            var generateAlert = CheckBpm(vital.Bpm, minBpm, maxBpm) || CheckSpo2(vital.Spo2, minSpo2, maxSpo2) || CheckRespRate(vital.RespRate, minRespRate, maxRespRate);
            return generateAlert;
        }

        private static bool CheckRespRate(in float vitalRespRate, in float minRespRate, in float maxRespRate)
        {
           return  vitalRespRate < minRespRate || vitalRespRate > maxRespRate;
        }

        private static bool CheckSpo2(in float vitalSpo2, in float minSpo2, in float maxSpo2)
        {
            return vitalSpo2 < minSpo2 || vitalSpo2 > maxSpo2;
        }

        private static bool CheckBpm(in float vitalBpm, in float minBpm, in float maxBpm)
        {
            return vitalBpm < minBpm || vitalBpm > maxBpm;
        }
    }
}