using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR.NET_TEST.Models
{
    public class InvoiceModel
    {
        public string AmountInWords { get; set; }
        public string[] CommodityPrice { get; set; }
        public string NoteDrawer { get; set; }
        public string SellerAddress { get; set; }
        public string[] CommodityNum { get; set; }
        public string SellerRegisterNum { get; set; }
        public string Remarks { get; set; }

    }
}
