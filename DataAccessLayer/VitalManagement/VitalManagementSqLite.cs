using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.Utils;
using DataModels;

namespace DataAccessLayer.VitalManagement
{
    public class VitalManagementSqLite:IVitalManagement
    {
        public IEnumerable<Vital> GetAllPatientsVitals()
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT p.PatientId, Bpm, Spo2, RespRate
                                FROM Patients p
                                INNER JOIN Vitals v on v.PatientId = p.PatientId"
            };

            var reader = cmd.ExecuteReader();
            var listOfAllPatientsVitals = new List<Vital>();

            while (reader.Read())
            {
                listOfAllPatientsVitals.Add(new Vital()
                {
                    PatientId = reader.GetString(0),
                    Bpm = reader.GetFloat(1),
                    Spo2 = reader.GetFloat(2),
                    RespRate = reader.GetFloat(3)
                });
            }
            con.Close();
            return listOfAllPatientsVitals;
        }
        public void UpdateVitalByPatientId(string patientId, Vital vital)
        {
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
            con.Close();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }

            CheckVitalsAndAddToAlertsTable(vital);
        }

        private void CheckVitalsAndAddToAlertsTable(Vital vital)
        {
            CheckIfVitalsAreOutOfRange(vital);
        }

        private void CheckIfVitalsAreOutOfRange(Vital vital)
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

            var minBpm = reader.GetFloat(0);
            var maxBpm = reader.GetFloat(1);
            var minSpo2 = reader.GetFloat(2);
            var maxSpo2 = reader.GetFloat(3);
            var minRespRate = reader.GetFloat(4);
            var maxRespRate = reader.GetFloat(5);

            var generateAlert = false;

            if (generateAlert)
            {
                AddToAlertsTable(vital.PatientId);
            }

        }
        private void AddToAlertsTable(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"INSERT INTO Alerts (
                                                   LayoutId,
                                                   IcuId,
                                                   BedId,
                                                   PatientId
                                               )
                                               VALUES (
                                                   'LayoutId',
                                                   'IcuId',
                                                   'BedId',
                                                   'PatientId'
                                               )"
            };
            cmd.Parameters.AddWithValue("@PatientId", "");
            cmd.Prepare();
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
            con.Close();
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
            con.Close();
        }
    }
}