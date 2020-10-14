
using System.Globalization;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class VitalFieldsValidator
    {
        private readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidateVitalsList(Vitals vitals)
        {
            _validator.IsWhitespaceOrEmptyOrNull(vitals.Bpm.ToString(CultureInfo.CurrentCulture));
            _validator.IsWhitespaceOrEmptyOrNull(vitals.Spo2.ToString(CultureInfo.CurrentCulture));
            _validator.IsWhitespaceOrEmptyOrNull(vitals.RespRate.ToString(CultureInfo.CurrentCulture));

        }
    }
}
