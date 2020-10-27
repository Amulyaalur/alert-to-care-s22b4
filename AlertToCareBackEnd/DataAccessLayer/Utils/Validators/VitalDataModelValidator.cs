using DataModels;

namespace DataAccessLayer.Utils.Validators
{
    public static class VitalDataModelValidator
    {
        public static void ValidateVitalDataModel(Vital vital)
        {
            CommonFieldValidator.StringValidator(vital.PatientId);
            CommonFieldValidator.FloatValidator(vital.Bpm);
            CommonFieldValidator.FloatValidator(vital.Spo2);
            CommonFieldValidator.FloatValidator(vital.RespRate);
        }
    }
}