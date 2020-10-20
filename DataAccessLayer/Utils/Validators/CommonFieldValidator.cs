using System;

namespace DataAccessLayer.Utils.Validators
{
    public class CommonFieldValidator
    {
        public static void StringValidator(string field)
        {
            
            var condition = string.IsNullOrWhiteSpace(field) ;

            if (!condition)
            {
                return;
            }
            throw new ArgumentNullException();
        }

        public static void IntegerValidator(int field)
        {
            if (field > 0)
            {
                return;
            }
            throw new ArgumentException(message:"Field less than or equal to zero");
        }
    }
}
