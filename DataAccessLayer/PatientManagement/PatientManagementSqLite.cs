using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.BedManagement;
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
        public bool RemovePatient(string patientId)
        {
            VitalManagementSqLite.DeletePatientFromVitalsTable(patientId);
            BedManagementSqLite.ChangeBedStatusToFalseByPatientId(patientId);

            var rowsAffected = RemovePatientQuery(patientId);

            if (rowsAffected == 0)
            {
                throw new Exception();
            }
            return true;
        }
        private int RemovePatientQuery(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Patients WHERE PatientId = @PatientId"
            };

            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            var rowsAffected = cmd.ExecuteNonQuery();
            con.Dispose();
            return rowsAffected;
        }
        public void UpdatePatient(string patientId, Patient patient)
        {
           PatientDataModelValidator.ValidatePatientDataModel(patient);
           BedManagementSqLite.ChangeBedStatusToFalseByPatientId(patientId);
           var _ = RemovePatientQuery(patientId);
           AddPatient(patient);
        }
    }
}
