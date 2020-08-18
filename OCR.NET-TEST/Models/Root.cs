using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR.NET_TEST.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Root
    {
        public WordsResult words_result { get; set; }
        public long log_id { get; set; }
        public int words_result_num { get; set; }
        public long RequestedCount{ get; set; }
    }

    public class CommodityTaxRate
    {
        public string row { get; set; }
        public string word { get; set; }
    }

    public class CommodityAmount
    {
        public string row { get; set; }
        public string word { get; set; }
    }

    public class CommodityTax
    {
        public string row { get; set; }
        public string word { get; set; }
    }

    public class CommodityName
    {
        public string row { get; set; }
        public string word { get; set; }
    }

    public class WordsResult
    {
        public string AmountInWords { get; set; }
        public List<object> CommodityPrice { get; set; }
        public string NoteDrawer { get; set; }
        public string SellerAddress { get; set; }
        public List<object> CommodityNum { get; set; }
        public string SellerRegisterNum { get; set; }
        public string Remarks { get; set; }
        public string SellerBank { get; set; }
        public List<CommodityTaxRate> CommodityTaxRate { get; set; }
        public string TotalTax { get; set; }
        public string CheckCode { get; set; }
        public string InvoiceCode { get; set; }
        public string InvoiceDate { get; set; }
        public string PurchaserRegisterNum { get; set; }
        public string InvoiceTypeOrg { get; set; }
        public string Password { get; set; }
        public string AmountInFiguers { get; set; }
        public string PurchaserBank { get; set; }
        public string Checker { get; set; }
        public string TotalAmount { get; set; }
        public List<CommodityAmount> CommodityAmount { get; set; }
        public string PurchaserName { get; set; }
        public List<object> CommodityType { get; set; }
        public string InvoiceType { get; set; }
        public string PurchaserAddress { get; set; }
        public List<CommodityTax> CommodityTax { get; set; }
        public List<object> CommodityUnit { get; set; }
        public string Payee { get; set; }
        public List<CommodityName> CommodityName { get; set; }
        public string SellerName { get; set; }
        public string InvoiceNum { get; set; }
    }

    
}
