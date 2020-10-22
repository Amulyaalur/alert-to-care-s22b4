using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.PatientManagement
{
    public interface IPatientManagement
    {
        public IEnumerable<Patient> GetAllPatients();
        public Patient GetPatientById(string patientId);
        public void AddPatient(Patient patient);
        public void RemovePatient(string patientId);
        public void UpdatePatient(string patientId, Patient patient);
        
    }
}