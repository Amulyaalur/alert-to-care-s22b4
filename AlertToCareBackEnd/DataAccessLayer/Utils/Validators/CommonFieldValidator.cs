using System;

namespace DataAccessLayer.Utils.Validators
{
    public static class CommonFieldValidator
    {
        public static void StringValidator(string field)
        {
            
            var condition = string.IsNullOrWhiteSpace(field) ;

            if (!condition)
            {
                return;
            }
            throw new ArgumentException("Data Field(s) cannot be null");
        }

        public static void IntegerValidator(int field)
        {
            if (field > 0)
            {
                return;
            }
            throw new ArgumentException(message:"Integer Field(s) is less than or equal to zero");
        }

        public static void FloatValidator(float field)
        {
            if (field >= 0)
            {
                return;
            }
            throw new ArgumentException(message: "Float Field(s) is less than to zero");
        }
    }
}
