﻿using Newtonsoft.Json;
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
        
        public OCRService()
        {
            
        }

        public string getToken()
        {
            var token = BaiduAccessToken.getAccessToken();            

            var tokenModel = JsonConvert.DeserializeObject<ResultToken>(token);

            string filePath = @"C:\Users\yblia\Desktop\发票图片.jpg";

            var result = baiduOcrService.vatInvoice(tokenModel.Access_Token, filePath);

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);

            

            return result;
        }


    }
}
