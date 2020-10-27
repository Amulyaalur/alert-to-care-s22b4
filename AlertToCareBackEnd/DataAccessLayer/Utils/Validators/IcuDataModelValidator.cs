using DataModels;

namespace DataAccessLayer.Utils.Validators
{
    public static class IcuDataModelValidator
    {
        
        public static void ValidateIcuDataModel(Icu icu)
        {
            CommonFieldValidator.StringValidator(icu.IcuId);
            CommonFieldValidator.IntegerValidator(icu.BedsCount);
            CommonFieldValidator.StringValidator(icu.LayoutId);
        }

        
    }
}
