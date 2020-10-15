using System;
using System.Collections.Generic;
using AlertToCareAPI.Models;
using AlertToCareAPI.Database;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class PatientDbRepository : IPatientDbRepository
    {
        private readonly DatabaseManager _creator=new DatabaseManager();
        private readonly PatientFieldsValidator _validator = new PatientFieldsValidator();

        public void AddPatient(Patient newState)
        {
            var patients = _creator.ReadPatientDatabase();
            _validator.ValidateNewPatientId(newState.PatientId, newState, patients);
            patients.Add(newState);
            _creator.WriteToPatientsDatabase(patients);
            ChangeBedStatus(newState.BedId, true);
        }
        public void RemovePatient(string patientId)
        {
            var patients = _creator.ReadPatientDatabase();
            _validator.ValidateOldPatientId(patientId, patients);
            for (var i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId == patientId)
                {
                    patients.Remove(patients[i]);
                    _creator.WriteToPatientsDatabase(patients);
                    ChangeBedStatus(patients[i].BedId, false);
                    return;
                }
            }
            
        }
        public void UpdatePatient(string patientId, Patient state)
        {
            var patients = _creator.ReadPatientDatabase();
            _validator.ValidateOldPatientId(patientId, patients);
            _validator.ValidatePatientRecord(state);

            for (var i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId == patientId)
                {
                    patients.Insert(i, state);
                    _creator.WriteToPatientsDatabase(patients);
                    return;
                }
            }
           
        }
        public IEnumerable<Patient> GetAllPatients()
        {
            var patients = _creator.ReadPatientDatabase();
            return patients;
        }
        public void ChangeBedStatus(string bedId, bool status)
        {
            var icuList = _creator.ReadIcuDatabase();
            foreach (var icu in icuList)
            {
                foreach (var bed in icu.Beds)
                {
                    if (bed.BedId == bedId)
                    {
                        bed.Status = status;
                        _creator.WriteToIcuDatabase(icuList);
                        return;
                    }
                }
            }
            throw new Exception("Invalid data field");
        }
    }
}
