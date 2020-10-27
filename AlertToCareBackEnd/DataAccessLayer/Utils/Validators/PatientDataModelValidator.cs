using DataModels;

namespace DataAccessLayer.Utils.Validators
{
    public static class PatientDataModelValidator
    {
        public static void ValidatePatientDataModel(Patient patient)
        {
            CommonFieldValidator.StringValidator(patient.PatientId);
            CommonFieldValidator.StringValidator(patient.PatientName);
            CommonFieldValidator.IntegerValidator(patient.Age);
            CommonFieldValidator.StringValidator(patient.ContactNumber);
            CommonFieldValidator.StringValidator(patient.Email);
            CommonFieldValidator.StringValidator(patient.BedId);
            CommonFieldValidator.StringValidator(patient.IcuId);
            CommonFieldValidator.StringValidator(patient.Address);
        }
    }
}
