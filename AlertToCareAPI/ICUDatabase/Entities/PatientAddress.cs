using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class PatientAddress
    {
        [Key]
        public string PatientId { get; set; }

        public string HouseNo { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }



    }
}