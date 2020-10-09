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
        public Guid BedId { get; set; }
        public Guid IcuId { get; set; }

        public Boolean BedStatus { get; set; }
    }
}
