using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.PatientManagement
{
    public interface IPatientManagement
    {
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(string patientId);
        void AddPatient(Patient patient);
        bool RemovePatient(string patientId);
        void UpdatePatient(string patientId, Patient patient);
        
    }
}