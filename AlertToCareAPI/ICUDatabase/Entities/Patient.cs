
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Patient
    {
        [Key]
        public string Mrn { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string ContactNo { get; set; }
        public string BedId { get; set; }
        public Guid ICUId { get; set; }
        public Vitals Vitals { get; set; }
        public PatientAddress Address { get; set; }
    }
}