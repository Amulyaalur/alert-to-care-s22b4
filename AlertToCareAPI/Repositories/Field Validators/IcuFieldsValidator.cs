using System;
using System.Linq;
using System.Collections.Generic;
using AlertToCareAPI.ICUDatabase.Entities;
using AlertToCareAPI.ICUDatabase;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class IcuFieldsValidator
    {
        readonly IcuContext _context = new IcuContext();
        readonly CommonFieldValidator _validator = new CommonFieldValidator();
        readonly PatientFieldsValidator _patientRecordValidator = new PatientFieldsValidator();
        public void ValidateIcuRecord(ICU icu)
        {
            _validator.IsWhitespaceOrEmptyOrNull(icu.IcuId);
            _validator.IsWhitespaceOrEmptyOrNull(icu.BedsCount.ToString());
            _validator.IsWhitespaceOrEmptyOrNull(icu.LayoutId);
            _validator.IsWhitespaceOrEmptyOrNull(icu.BedIdPrefix);
            ValidatePatientsList(icu.Patients);
        }

        public void ValidatePatientsList(ICollection<Patient> patients)
        {
            foreach (var patient in patients)
            {
                _patientRecordValidator.ValidatePatientRecord(patient);
            }
        }

        public void ValidateOldIcuId(string icuId)
        {
            var icuStore = _context.IcuList.ToList();
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    return;
                }
            }
            throw new Exception("Invalid Patient Id");
        }

        public void ValidateNewIcuId(string icuId, ICU icuRecord)
        {
            var icuStore = _context.IcuList.ToList();
            foreach (var icu in icuStore)
            {
                if (icu.IcuId == icuId)
                {
                    throw new Exception("Invalid Patient Id");
                }
            }

            ValidateIcuRecord(icuRecord);
        }
    }
}
