using System;
using System.Collections.Generic;
using System.Net.Http;


namespace OCR.NET_TEST.Services
{
    public static class BaiduAccessToken
    {
		public static String getAccessToken(string clientId, string clientSecret)
		{
			String authHost = "https://aip.baidubce.com/oauth/2.0/token";
			HttpClient client = new HttpClient();
			List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
			paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
			paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
			paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

			HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
			String result = response.Content.ReadAsStringAsync().Result;
			Console.WriteLine(result);
			return result;
		}
	}
}
