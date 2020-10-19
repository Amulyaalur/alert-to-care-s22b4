using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer
{
    public interface IPatientManagement
    {
        void AddPatient(Patient newState);
        void RemovePatient(string patientId);
        void UpdatePatient(string patientId, Patient state);
        IEnumerable<Patient> GetAllPatients();
    }
}