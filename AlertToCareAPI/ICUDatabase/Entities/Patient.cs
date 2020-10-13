
using System.ComponentModel.DataAnnotations;

namespace AlertToCareAPI.ICUDatabase.Entities
{
    public class Patient
    {
        [Key]
        [Required]
        public string PatientId { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public int Age { get; set; }

        [MaxLength(10)]
        [Required]
        public string ContactNo { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string BedId { get; set; }

        [Required]
        public string IcuId { get; set; }

        [Required]
        public Vitals Vitals { get; set; }

        [Required]
        public PatientAddress Address { get; set; }
    }
}