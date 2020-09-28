using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OCR.NET_TEST.Models;
using OCR.NET_TEST.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OCR.NET_TEST.Services
{
    public class OCRService
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;
        public static long COUNT = 0;

        public OCRService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public Task<List<Root>> GetInvoice(FileModel model)
        {
            var clientId = configuration["Baidu:clientId"];
            var secret = configuration["Baidu:clientSecret"];

            var token = BaiduAccessToken.getAccessToken(clientId, secret);

            var tokenModel = JsonConvert.DeserializeObject<ResultToken>(token);

            var rootList = new List<Root>();

            foreach (var item in model.Files)
            {
                string filePath = GetFilePath(item);

                var result = baiduOcrService.vatInvoice(tokenModel.Access_Token, filePath);

                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);

                myDeserializedClass = CaculateRequestCount(myDeserializedClass);

                rootList.Add(myDeserializedClass);
            }

            return Task.FromResult(rootList);
        }

        public string GetFilePath(IFormFile model)
        {
            if (model != null)
            {

                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    model.CopyTo(fs);
                    fs.Flush();
                }
                return filePath;

            }

            return null;
        }

        private Root CaculateRequestCount(Root model)
        {
            COUNT++;
            model.RequestedCount = COUNT;
            return model;
        }

        public List<VatInvoice> ModelTransform(List<Root> roots)
        {
            List<VatInvoice> invoices = new List<VatInvoice>();

            foreach(var root in roots)
            {
                var invoice = new VatInvoice();

                invoice.InvoiceType = root.words_result.InvoiceType;

                invoice.InvoiceTypeOrg = root.words_result.InvoiceTypeOrg;

                invoice.Comodities = new List<Comodity>();

                for(var i = 0; i< root.words_result.CommodityName.Count; i++)
                {
                    var commodity = new Comodity();

                    if (root.words_result.CommodityName != null && root.words_result.CommodityName.Count > 0)
                    {
                        //commodity.Name =  root.words_result.CommodityName.Select(t => t.word).ToArray()[i];
                        commodity.Name =  root.words_result.CommodityName.Find(t => t.row == (i + 1).ToString()).word;
                    }
                    //type
                    if (root.words_result.CommodityType != null && root.words_result.CommodityType.Count > 0)
                    {
                        commodity.Type = root.words_result.CommodityType.Find(t => t.row == (i + 1).ToString()).word;
                    }
                    //unit
                    if (root.words_result.CommodityUnit != null && root.words_result.CommodityUnit.Count > 0)
                    {
                        commodity.Unit = root.words_result.CommodityUnit.Find(t => t.row == (i + 1).ToString()).word;
                    }
                    //num
                    if (root.words_result.CommodityNum != null && root.words_result.CommodityNum.Count > 0)
                    {
                        commodity.Quantity = root.words_result.CommodityNum.Find(t => t.row == (i + 1).ToString()).word;
                    }

                    //UnitPrice
                    if (root.words_result.CommodityPrice != null && root.words_result.CommodityPrice.Count > 0)
                    {
                        commodity.UnitPrice = root.words_result.CommodityPrice.Find(t => t.row == (i + 1).ToString()).word;
                    }

                    //Amount
                    if (root.words_result.CommodityAmount != null && root.words_result.CommodityAmount.Count > 0)
                    {
                        commodity.Price = root.words_result.CommodityAmount.Find(t => t.row == (i + 1).ToString()).word;
                    }

                    //TaxRate
                    if (root.words_result.CommodityTaxRate != null && root.words_result.CommodityTaxRate.Count > 0)
                    {
                        commodity.TaxRate = root.words_result.CommodityTaxRate.Find(t => t.row == (i + 1).ToString()).word;
                    }

                    //TaxAmount
                    if (root.words_result.CommodityTax != null && root.words_result.CommodityTax.Count > 0)
                    {
                        commodity.TaxAmount = root.words_result.CommodityTax.Find(t => t.row == (i + 1).ToString()).word;
                    }

                    invoice.Comodities.Add(commodity);
                }
               
                invoices.Add(invoice);
            }

            return invoices;
        }

        public StringBuilder ExportToCsv(List<VatInvoice> invoices)
        {
            var sb = new StringBuilder();

            sb.AppendLine("货物名称,规格型号,单位,数量,单价,货物金额,货物税率,货物税额, 发票类型, 发票名称");

            foreach (var item in invoices)
            {
                
                if( item.Comodities!=null && item.Comodities.Count > 0)
                {
                    for(var i = 0; i < item.Comodities.Count; i++)
                    {
                        if (i == 0)
                        {
                            sb.AppendLine($"{item.Comodities[i].Name},{item.Comodities[i].Type},{item.Comodities[i].Unit},{item.Comodities[i].Quantity}," +
                                $"{item.Comodities[i].UnitPrice},{item.Comodities[i].Price},{item.Comodities[i].TaxRate},{item.Comodities[i].TaxAmount}," +
                                $"{item.InvoiceType}, { item.InvoiceTypeOrg}");
                        }
                        else
                        {   //just print commodity info
                            sb.AppendLine($"{item.Comodities[i].Name},{item.Comodities[i].Type},{item.Comodities[i].Unit},{item.Comodities[i].Quantity}," +
                               $"{item.Comodities[i].UnitPrice},{item.Comodities[i].Price},{item.Comodities[i].TaxRate},{item.Comodities[i].TaxAmount}");
                        }
                    }
                }

            }

            return sb;
        }
            
            

    }
}
