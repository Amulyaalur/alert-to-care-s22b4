using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.BedManagement;
using DataAccessLayer.IcuManagement;
using DataAccessLayer.Utils;
using DataAccessLayer.Utils.Validators;
using DataAccessLayer.VitalManagement;
using DataModels;

namespace DataAccessLayer.PatientManagement
{
    public class PatientManagementSqLite:IPatientManagement
    {
        public IEnumerable<Patient> GetAllPatients()
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT PatientId,
                                        PatientName,
                                        Age,
                                        ContactNumber,
                                        Email,
                                        BedId,
                                        IcuId,
                                        Address
                                FROM Patients"
            };

            var reader = cmd.ExecuteReader();
            var listOfPatients = new List<Patient>();

            while (reader.Read())
            {
                listOfPatients.Add(new Patient()
                {
                    PatientId = reader.GetString(0),
                    PatientName = reader.GetString(1),
                    Age = reader.GetInt32(2),
                    ContactNumber = reader.GetString(3),
                    Email = reader.GetString(4),
                    BedId = reader.GetString(5),
                    IcuId = reader.GetString(6),
                    Address = reader.GetString(7)
                });
            }
            reader.Dispose();
            con.Dispose();
            return listOfPatients;
        }
        public Patient GetPatientById(string patientId)
        {
            CommonFieldValidator.StringValidator(patientId);
            if (CheckIfPatientIdExists(patientId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "PatientId does not exists");
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT PatientId,
                                        PatientName,
                                        Age,
                                        ContactNumber,
                                        Email,
                                        BedId,
                                        IcuId,
                                        Address
                                FROM Patients
                                WHERE PatientId = @patientId"
            };
            cmd.Parameters.AddWithValue("@patientId", patientId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var patient = new Patient()
            {
                PatientId = reader.GetString(0),
                PatientName = reader.GetString(1),
                Age = reader.GetInt32(2),
                ContactNumber = reader.GetString(3),
                Email = reader.GetString(4),
                BedId = reader.GetString(5),
                IcuId = reader.GetString(6),
                Address = reader.GetString(7)
            };
            reader.Dispose();
            con.Dispose();

            return patient;
        }
        public void AddPatient(Patient patient)
        {
            PatientDataModelValidator.ValidatePatientDataModel(patient);
            if (CheckIfPatientIdExists(patient.PatientId) > 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "PatientId already exists");
            if (IcuManagementSqLite.CheckIfIcuIdExists(patient.IcuId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "IcuId does not exists");
            CheckBedError(patient);
            
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"INSERT INTO Patients (
                                                        PatientId,
                                                        PatientName,
                                                        Age,
                                                        ContactNumber,
                                                        Email,
                                                        BedId,
                                                        IcuId,
                                                        Address
                                                      )
                                                        VALUES (
                                                        @PatientId,
                                                        @PatientName,
                                                        @Age,
                                                        @ContactNumber,
                                                        @Email,
                                                        @BedId,
                                                        @IcuId,
                                                        @Address
                                                         )"

            };

            cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
            cmd.Parameters.AddWithValue("@PatientName", patient.PatientName);
            cmd.Parameters.AddWithValue("@Age", patient.Age);
            cmd.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);
            cmd.Parameters.AddWithValue("@Email", patient.Email);
            cmd.Parameters.AddWithValue("@BedId", patient.BedId);
            cmd.Parameters.AddWithValue("@IcuId", patient.IcuId);
            cmd.Parameters.AddWithValue("@Address", patient.Address);
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();

            VitalManagementSqLite.AddPatientIntoVitalsTable(patient.PatientId);
            BedManagementSqLite.ChangeBedStatusToTrueByBedId(patient.BedId);
        }

        private static void CheckBedError(Patient patient)
        {
            if (BedManagementSqLite.CheckIfBedIdExists(patient.BedId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "BedId does not exists");
            if (BedManagementSqLite.CheckIfBedIsAvailableByBedIdAndIcuId(patient.IcuId, patient.BedId)) throw new SQLiteException(SQLiteErrorCode.NotFound, message: "Bed is not Available");
        }

        public void RemovePatient(string patientId)
        {
            if (CheckIfPatientIdExists(patientId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "PatientId does not exists");
            VitalManagementSqLite.DeletePatientFromVitalsTable(patientId);
            BedManagementSqLite.ChangeBedStatusToFalseByPatientId(patientId);

            ExecuteRemovePatientQuery(patientId);
        }
        private void ExecuteRemovePatientQuery(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Patients WHERE PatientId = @PatientId"
            };

            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
        }
        public void UpdatePatient(string patientId, Patient patient)
        {
            CommonFieldValidator.StringValidator(patientId);
            if (CheckIfPatientIdExists(patientId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "PatientId does not exists");
            if (!patientId.Equals(patient.PatientId)) throw new ArgumentException(message:"PatientIds does not match");

            BedManagementSqLite.ChangeBedStatusToFalseByPatientId(patientId); 
            
            ExecuteRemovePatientQuery(patientId);

            AddPatient(patient);
        }
        public static long CheckIfPatientIdExists(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT COUNT(*) from Patients WHERE PatientId = @PatientId"
            };

            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            var count = (long)cmd.ExecuteScalar();
            con.Dispose();
            return count;
        }
    }
}
