using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.VitalManagement
{
    public interface IVitalManagement
    {
        IEnumerable<Vital> GetAllPatientsVitals();
        public void UpdateVitalByPatientId(string patientId, Vital vital);
    }
}