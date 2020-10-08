
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ICU
    {
        [Key]
        public Guid IcuId { get; set; }

        public string Layout { get; set; }

        public int BedsCount { get; set; }

        public string BedIdPrefix { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}