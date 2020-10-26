using System.Diagnostics.CodeAnalysis;

namespace DataModels
{
    [ExcludeFromCodeCoverage]
    public class Patient
    {
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string BedId { get; set; }
        public string IcuId { get; set; }
        public string Address { get; set; }
    }
}