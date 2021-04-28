using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
    public class PastelDataClass
    {
        public int? PastelID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        public int? CtnQty { get; set; }
        public string CtnSize { get; set; }
        public string Grade { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }         
    }
}
