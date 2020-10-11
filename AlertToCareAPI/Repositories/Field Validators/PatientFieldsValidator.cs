using System;
using System.Linq;
using AlertToCareAPI.ICUDatabase.Entities;
using AlertToCareAPI.ICUDatabase;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class PatientFieldsValidator
    {
        readonly IcuContext _context = new IcuContext();
        readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidatePatientRecord(Patient patient)
        {
           _validator.IsWhitespaceOrEmptyOrNull(patient.PatientId);
           _validator.IsWhitespaceOrEmptyOrNull(patient.PatientName);
           _validator.IsWhitespaceOrEmptyOrNull(patient.Age.ToString());
           _validator.IsWhitespaceOrEmptyOrNull(patient.ContactNo);
           _validator.IsWhitespaceOrEmptyOrNull(patient.BedId);
           _validator.IsWhitespaceOrEmptyOrNull(patient.IcuId);
           CheckConsistencyInPatientIdFields(patient);

        }

        public void ValidateOldPatientId(string patientId)
        {
            var patients = _context.Patients.ToList();
            foreach (var patient in patients)
            {
                if (patient.PatientId == patientId)
                {
                    return;
                }
            }
            throw new Exception("Invalid Patient Id");
        }

        public void ValidateNewPatientId(string patientId, Patient patientRecord)
        {
            var patients = _context.Patients.ToList();
            foreach (var patient in patients)
            {
                if (patient.PatientId == patientId)
                {
                    throw new Exception("Invalid Patient Id");
                }
            }

            ValidatePatientRecord(patientRecord);
        }

        public void CheckConsistencyInPatientIdFields(Patient patient)
        {
            if (patient.PatientId.ToLower() == patient.Vitals.PatientId.ToLower())
            {
                if (patient.PatientId.ToLower() == patient.Address.PatientId.ToLower())
                {
                    return;
                }
            }
            throw new Exception("Invalid data field");
        }

    }
}
