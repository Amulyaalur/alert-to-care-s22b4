using System;


namespace AlertToCareAPI.Repositories.Field_Validators
{
    public class CommonFieldValidator
    {
        public void IsWhitespaceOrEmptyOrNull(string field)
        {
            var condition1 = string.IsNullOrEmpty(field);
            var condition2 = string.IsNullOrWhiteSpace(field) && condition1;

            if (!condition2)
            {
                return;
            }
            throw new Exception("Invalid data field");
        }
    }
}
