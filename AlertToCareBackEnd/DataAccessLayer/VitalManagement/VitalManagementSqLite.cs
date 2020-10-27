using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.AlertManagement;
using DataAccessLayer.PatientManagement;
using DataAccessLayer.Utils;
using DataAccessLayer.Utils.Validators;
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
        public void UpdateVitalByPatientId(string patientId, Vital vital)
        {
            if (PatientManagementSqLite.CheckIfPatientIdExists(patientId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "PatientId does not exists");
            VitalDataModelValidator.ValidateVitalDataModel(vital);
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
            cmd.ExecuteNonQuery();
            con.Dispose();

            CheckVitalsAndAddToAlertsTable(vital);
        }
        private void CheckVitalsAndAddToAlertsTable(Vital vital)
        {
            var generateAlert = VitalsChecker.CheckAllVitals(vital);
            if (generateAlert) AlertManagementSqLite.AddToAlertsTable(vital.PatientId);
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