using System;
using System.Collections.Generic;
using AlertToCareAPI.Database;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class PatientFieldsValidator
    {
        private readonly CommonFieldValidator _validator = new CommonFieldValidator();
        private readonly VitalFieldsValidator _vitalsValidator = new VitalFieldsValidator();
        private readonly AddressFieldsValidator _addressValidator = new AddressFieldsValidator();
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
           _vitalsValidator.ValidateVitalsList(patient.Vitals);
           _addressValidator.ValidateAddressFields(patient.Address);
           CheckConsistencyInIcuIdFields(patient.IcuId, patient.BedId);

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
            CheckIcuPresence(patientRecord.IcuId);
            CheckBedStatus(patientRecord.BedId);
            foreach (var patient in patients)
            {
                if (patient.PatientId == patientId)
                {
                    throw new Exception("Invalid Patient Id");
                }
            }

            ValidatePatientRecord(patientRecord);
        }

        private static void CheckConsistencyInPatientIdFields(Patient patient)
        {
            if (patient.PatientId.ToLower() == patient.Vitals.PatientId.ToLower())
            {
               return;
            }
            throw new Exception("Invalid data field");
        }

        private static void CheckConsistencyInIcuIdFields(string icuId, string bedId)
        {
            var database = new DatabaseManager();
            var beds = database.ReadBedsDatabase();
            foreach (var bed in beds)
            {
                if (bed.BedId == bedId)
                {
                    if (bed.IcuId == icuId)
                    {
                        return;
                    }
                }
            }
            throw new Exception("Invalid data field");
        }

        private static void CheckIcuPresence(string icuId)
        {
            var database = new DatabaseManager();
            var icuList = database.ReadIcuDatabase();
            foreach (var icu in icuList)
            {
                if (icu.IcuId == icuId)
                {
                    return;
                }
            }

            throw new Exception("Invalid data field");
        }

        private static void CheckBedStatus(string bedId)
        {
            var database = new DatabaseManager();
            var beds = database.ReadBedsDatabase();
            foreach (var bed in beds)
            {
                if (bed.BedId == bedId)
                {
                    if (bed.Status == false)
                    {
                        return;
                    }
                }
            }
            throw new Exception("Invalid data field");
        }
    }
}
