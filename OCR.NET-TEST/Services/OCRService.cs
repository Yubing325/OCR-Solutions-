using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OCR.NET_TEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OCR.NET_TEST.Services
{
    public class OCRService
    {
        private readonly IConfiguration configuration;
        public static long COUNT = 0;

        public OCRService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Root> GetInvoice()
        {
            var clientId = configuration["Baidu:clientId"];
            var secret= configuration["Baidu:clientSecret"];

            var token = BaiduAccessToken.getAccessToken(clientId, secret);            

            var tokenModel = JsonConvert.DeserializeObject<ResultToken>(token);

            string filePath = @"C:\Users\yblia\Desktop\发票图片.jpg";

            var result = baiduOcrService.vatInvoice(tokenModel.Access_Token, filePath);

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);

            myDeserializedClass = caculateRequestCount(myDeserializedClass);

            return myDeserializedClass;
        }

        private Root caculateRequestCount(Root model)
        {
            COUNT++;
            model.RequestedCount = COUNT;
            return model;
        }
    }
}
