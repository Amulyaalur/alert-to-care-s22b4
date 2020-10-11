using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class Beds
    {  
        [Key]
        public string BedId { get; set; }
        public string IcuId { get; set; }
        public Boolean BedStatus { get; set; }
    }
}
