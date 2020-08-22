using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OCR.NET_TEST.Models;
using OCR.NET_TEST.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public Task<Root> GetInvoice(FileModel model)
        {
            var clientId = configuration["Baidu:clientId"];
            var secret= configuration["Baidu:clientSecret"];

            var token = BaiduAccessToken.getAccessToken(clientId, secret);            

            var tokenModel = JsonConvert.DeserializeObject<ResultToken>(token);

            string filePath = GetFilePath(model);

            var result = baiduOcrService.vatInvoice(tokenModel.Access_Token, filePath);

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);

            myDeserializedClass = caculateRequestCount(myDeserializedClass);

            return Task.FromResult(myDeserializedClass);
        }

        public string GetFilePath(FileModel model)
        {
            if (model.File != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.File.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                //FileStream fs = new FileStream(filePath, FileMode.Create);
                //model.File.CopyToAsync(fs);
                //fs.Dispose();

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    model.File.CopyTo(fs);
                    fs.Flush();
                }
                return filePath;
            }

            return null;
        }
        
        private Root caculateRequestCount(Root model)
        {
            COUNT++;
            model.RequestedCount = COUNT;
            return model;
        }
    }
}
