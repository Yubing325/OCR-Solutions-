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

        public StringBuilder ExportToCsv(List<Root> roots)
        {
            var sb = new StringBuilder();

            sb.AppendLine("type, name, commodity");
            foreach (var root in roots)
            {

                sb.AppendLine($"{root.words_result.InvoiceType}, {root.words_result.InvoiceTypeOrg}");

            }

            return sb;
        }
            
            

    }
}
