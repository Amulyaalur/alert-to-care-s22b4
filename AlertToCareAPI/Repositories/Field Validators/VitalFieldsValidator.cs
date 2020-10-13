
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class VitalFieldsValidator
    {
        readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidateVitalsList(Vitals vitals)
        {
            _validator.IsWhitespaceOrEmptyOrNull(vitals.Bpm.ToString());
            _validator.IsWhitespaceOrEmptyOrNull(vitals.Spo2.ToString());
            _validator.IsWhitespaceOrEmptyOrNull(vitals.RespRate.ToString());

        }
    }
}
