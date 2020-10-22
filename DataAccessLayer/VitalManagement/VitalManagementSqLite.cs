using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.AlertManagement;
using DataAccessLayer.Utils;
using DataModels;

namespace DataAccessLayer.VitalManagement
{
    public class VitalManagementSqLite:IVitalManagement
    {
        public IEnumerable<object> GetAllPatientsVitals()
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT p.PatientName, p.PatientId, Bpm, Spo2, RespRate
                                FROM Patients as p
                                INNER JOIN Vitals as v on v.PatientId = p.PatientId"
            };

            var reader = cmd.ExecuteReader();
            var listOfAllPatientsVitals = new List<object>();

            while (reader.Read())
            {
                listOfAllPatientsVitals.Add(new
                {
                    name = reader.GetString(0),
                    PatientId = reader.GetString(1),
                    Bpm = reader.GetFloat(2),
                    Spo2 = reader.GetFloat(3),
                    RespRate = reader.GetFloat(4)
                });

            }
            reader.Dispose();
            con.Dispose();
            return listOfAllPatientsVitals;
        }
        public bool UpdateVitalByPatientId(string patientId, Vital vital)
        {
            ////add vital data model check
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"UPDATE Vitals
                                   SET 
                                       Bpm = @Bpm,
                                       Spo2 = @Spo2,
                                       RespRate = @RespRate
                                 WHERE PatientId = @PatientId"
            };
            cmd.Parameters.AddWithValue("@Bpm", vital.Bpm);
            cmd.Parameters.AddWithValue("@Spo2", vital.Spo2);
            cmd.Parameters.AddWithValue("@RespRate", vital.RespRate);
            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            var rowsAffected = cmd.ExecuteNonQuery();
            con.Dispose();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }

            
            return CheckVitalsAndAddToAlertsTable(vital);
        }
        private bool CheckVitalsAndAddToAlertsTable(Vital vital)
        {
            return CheckIfVitalsAreOutOfRange(vital);
        }
        private bool CheckIfVitalsAreOutOfRange(Vital vital)
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

            if (generateAlert) AlertManagementSqLite.AddToAlertsTable(vital.PatientId);
            return generateAlert;

        }
        public static void AddPatientIntoVitalsTable(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"INSERT INTO Vitals (PatientId) VALUES (@PatientId);"
            };

            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
        }
        public static void DeletePatientFromVitalsTable(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Vitals WHERE PatientId = @PatientId"
            };

            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
        }
    }
}