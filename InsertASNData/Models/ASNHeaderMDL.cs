using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertASNData.Models
{
    public class ASNHeaderMDL
    {
        public long ASNHeaderId { get; set; }
        public string AccountNumber { get; set; }
        public string  VendorRefernce { get; set; }
        public DateTime SentDate { get; set; }
        public int Cartons { get; set; }
        public int Pallets { get; set; }
        public int StatusId { get; set; }
    }
}
