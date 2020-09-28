using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR.NET_TEST.Models
{
    public class VatInvoice
    {
        public string InvoiceType { get; set; }

        public string InvoiceTypeOrg { get; set; }

        public List<Comodity> Comodities { get; set; }

    }
}
