using System.Diagnostics.CodeAnalysis;

namespace DataModels
{
    [ExcludeFromCodeCoverage]
    public class Icu
    {
        public string IcuId { get; set; }
        public string LayoutId { get; set; }
        public int BedsCount { get; set; }
    }
}