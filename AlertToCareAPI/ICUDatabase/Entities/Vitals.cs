

using System.ComponentModel.DataAnnotations;

namespace AlertToCareAPI.ICUDatabase.Entities
{
    public class Vitals
    {
        [Key]
        public string PatientId { get; set; }
        public float Bpm { get; set; }
        public float Spo2 { get; set; }
        public float RespRate { get; set; }
    }
}


