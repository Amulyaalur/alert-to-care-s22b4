using DataModels;
using System.Data.SQLite;

namespace DataAccessLayer.Utils
{
    public class VitalsChecker
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
            var generateAlert = (vital.Bpm < minBpm || vital.Bpm > maxBpm) || (vital.Spo2 < minSpo2 || vital.Spo2 > maxSpo2) || (vital.RespRate < minRespRate || vital.RespRate > maxRespRate);
            return generateAlert;
        }
    }
}