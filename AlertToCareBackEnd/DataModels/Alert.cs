using System.Diagnostics.CodeAnalysis;

namespace DataModels
{
    [ExcludeFromCodeCoverage]
    public class Alert
    {
        public int AlertId { get; set; }
        public string LayoutId { get; set; }
        public string IcuId { get; set; }
        public string BedId { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public float Bpm { get; set; }
        public float Spo2 { get; set; }
        public float RespRate { get; set; }
        public bool AlertStatus { get; set; }

    }
}