using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories
{
    public interface IPatientDbRepository
    {
        void AddPatient( Patient newState);
        void RemovePatient(string patientId);
        void UpdatePatient(string patientId, Patient state);
        IEnumerable<Patient> GetAllPatients();
    }
}
