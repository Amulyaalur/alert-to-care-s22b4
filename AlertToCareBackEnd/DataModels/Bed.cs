using System.Diagnostics.CodeAnalysis;

namespace DataModels
{
    [ExcludeFromCodeCoverage]
    public class Bed
    {
        public string BedId { get; set; }
        public string IcuId { get; set; }
        public bool Status { get; set; }
    }
}
