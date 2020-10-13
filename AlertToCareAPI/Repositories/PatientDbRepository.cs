using System;
using System.Collections.Generic;
using AlertToCareAPI.ICUDatabase.Entities;
using AlertToCareAPI.ICUDatabase;
using System.Linq;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class PatientDbRepository : IPatientDbRepository
    {
        readonly IcuContext _context;
        public PatientDbRepository(IcuContext db)
        {
            _context = db;
        }
        readonly PatientFieldsValidator _validator = new PatientFieldsValidator();
        public void AddPatient(Patient newState)
        {
            _validator.ValidateNewPatientId(newState.PatientId, newState);
      
           _context.Add<Patient>(newState);
           
            ChangeBedStatus(newState.BedId, true);
            _context.SaveChanges();
        }
        public void RemovePatient(string patientId)
        {
            _validator.ValidateOldPatientId(patientId);
            var patients = _context.Patients.ToList();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId == patientId)
                {
                    _context.Remove<Patient>(patients[i]);
                    ChangeBedStatus(patients[i].BedId, false);
                    return;
                }
            }
        }
        public void UpdatePatient(string patientId, Patient state)
        {
            _validator.ValidateOldPatientId(patientId);
            _validator.ValidatePatientRecord(state);

            var patients = _context.Patients.ToList();
            for (var i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId == patientId)
                {
                    patients.Insert(i, state);
                    return;
                }
            }
        }
        public IEnumerable<Patient> GetAllPatients()
        {
            var patients = _context.Patients.ToList();
            return patients;
        }
        public void ChangeBedStatus(string bedId, bool status)
        {
            var bedList = _context.Beds.ToList();
            foreach (var bed in bedList)
            {
                if (bed.BedId == bedId)
                {
                    bed.Status = status;
                    return;
                }
            }
            throw new Exception("Invalid data field");
        }
    }
}
