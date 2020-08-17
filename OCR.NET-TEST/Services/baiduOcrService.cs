﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace OCR.NET_TEST.Services
{
    public class baiduOcrService
    {
        // 增值税发票识别
        public static string vatInvoice(string token, string path)
        {
            //string token = "[调用鉴权接口获取的token]";
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/vat_invoice?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            // 图片的base64编码
            string base64 = getFileBase64(path);
            String str = "image=" + HttpUtility.UrlEncode(base64);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("增值税发票识别:");
            Console.WriteLine(result);
            return result;
        }

        public static String getFileBase64(String fileName)
        {
            FileStream filestream = new FileStream(fileName, FileMode.Open);
            byte[] arr = new byte[filestream.Length];
            filestream.Read(arr, 0, (int)filestream.Length);
            string baser64 = Convert.ToBase64String(arr);
            filestream.Close();
            return baser64;
        }
    }
}



