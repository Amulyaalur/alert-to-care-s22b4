using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.VitalManagement
{
    public interface IVitalManagement
    {
        IEnumerable<object> GetAllPatientsVitals();
        public void UpdateVitalByPatientId(string patientId, Vital vital);
    }
}