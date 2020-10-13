using System;
using System.Collections.Generic;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class PatientFieldsValidator
    {
        readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidatePatientRecord(Patient patient)
        {
           _validator.IsWhitespaceOrEmptyOrNull(patient.PatientId);
           _validator.IsWhitespaceOrEmptyOrNull(patient.PatientName);
           _validator.IsWhitespaceOrEmptyOrNull(patient.Age.ToString());
           _validator.IsWhitespaceOrEmptyOrNull(patient.ContactNo);
           _validator.IsWhitespaceOrEmptyOrNull(patient.Email);
           _validator.IsWhitespaceOrEmptyOrNull(patient.BedId);
           _validator.IsWhitespaceOrEmptyOrNull(patient.IcuId);
           CheckConsistencyInPatientIdFields(patient);

        }

        public void ValidateOldPatientId(string patientId, List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                if (patient.PatientId == patientId)
                {
                    return;
                }
            }
            throw new Exception("Invalid Patient Id");
        }

        public void ValidateNewPatientId(string patientId, Patient patientRecord, List<Patient> patients)
        {
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
               return;
            }
            throw new Exception("Invalid data field");
        }

    }
}
