

namespace AlertToCareAPI.Models
{
    public class Vital
    {
        public string PatientId { get; set; }
        public float Bpm { get; set; }
        public float Spo2 { get; set; }
        public float RespRate { get; set; }
    }
}


