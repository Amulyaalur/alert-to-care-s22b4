using System;
using System.ComponentModel.DataAnnotations;
namespace AlertToCareAPI.ICUDatabase.Entities
{
    public class Beds
    {  
        [Key]
        public string BedId { get; set; }
        public string IcuId { get; set; }

        public bool Status { get; set; }
    }
}
