using System.Diagnostics.CodeAnalysis;

namespace DataModels
{
    // ReSharper disable All
    [ExcludeFromCodeCoverage]
    public class Bed
    {
        public string BedId { get; set; }
        public string IcuId { get; set; }
        public bool Status { get; set; }
    }
}
