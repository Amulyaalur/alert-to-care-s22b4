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
        readonly IcuContext _context = new IcuContext();
        readonly PatientFieldsValidator _validator = new PatientFieldsValidator();
        public void AddPatient(Patient newState)
        {
            _validator.ValidateNewPatientId(newState.PatientId, newState);
            _context.Add<Patient>(newState);
            _context.SaveChanges();
            ChangeBedStatus(newState.BedId, true);
        }
        public void RemovePatient(string patientId)
        {
            _validator.ValidateOldPatientId(patientId);
            var patients = _context.Patients.ToList();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId == patientId)
                {
                    _context.Remove(_context.Patients.Single(a => a.PatientId == patientId));
                    _context.SaveChanges();
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
                    _context.Remove(_context.Patients.Single(a => a.PatientId == patientId));
                    _context.Add<Patient>(state);
                    _context.SaveChanges();
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
                    var entity = _context.Beds.First(a => a.BedId == bedId);
                    entity.Status = status;
                    _context.SaveChanges();
                    return;
                }
            }
            throw new Exception("Invalid data field");
        }
    }
}
