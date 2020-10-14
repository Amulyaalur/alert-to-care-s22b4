
using System.Collections.Generic;

namespace AlertToCareAPI.Models
{
    public class Icu
    {
     
        public string IcuId { get; set; }
        public string LayoutId { get; set; }
        public int BedsCount { get; set; }
        public List<Bed> Beds { get; set; }
    }
}