using AlertToCareAPI.Models;

namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class AddressFieldsValidator
    {
        readonly CommonFieldValidator _validator = new CommonFieldValidator();
        public void ValidateVitalsList(PatientAddress address)
        {
            _validator.IsWhitespaceOrEmptyOrNull(address.HouseNo);
            _validator.IsWhitespaceOrEmptyOrNull(address.Street);
            _validator.IsWhitespaceOrEmptyOrNull(address.City);
            _validator.IsWhitespaceOrEmptyOrNull(address.State);
            _validator.IsWhitespaceOrEmptyOrNull(address.Pincode);

        }
    }
}
