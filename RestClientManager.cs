using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using fastJSON;
using Newtonsoft.Json.Linq;
using System.Net;

namespace RTEvents
{
   
        public class RestClientManager
        {
            public JObject Post(string baseUrl, string url, JObject sendData)
            {
                return Post(baseUrl, url, sendData, string.Empty);
            }

            public JObject Post(string baseUrl, string url, JObject sendData, string token)
            {
                RestClient client = new RestClient(baseUrl);
                var request = new RestRequest(url, Method.POST);
                var contenType = "application/json";
                request.AddHeader("Accept", contenType);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.AddHeader("token", token);
                }
                if (sendData == null)
                {
                    sendData = new JObject();
                }
                request.AddParameter(contenType, sendData, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    return null;
                }
                return JObject.Parse(response.Content);
            }

            public JObject Get(string baseUrl, string url, JObject sendData)
            {
                return Get(baseUrl, url, sendData, string.Empty);
            }

            public JObject Get(string baseUrl, string url, JObject sendData, string token)
            {
                try
                {
                    string parames = string.Empty;
                    if (sendData != null)
                    {
                        StringBuilder datas = new StringBuilder();
                        foreach (var item in sendData)
                        {
                            datas.AppendFormat("{0}={1}", item.Key, item.Value);
                        }
                        parames = string.Format("?{0}", string.Join("&", datas));
                    }
                    var client = new RestClient(string.Format("{0}{1}{2}", baseUrl, url, parames));
                    var request = new RestRequest(Method.GET);
                    var response = client.Execute(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        return JObject.Parse(response.Content);
                    }
                    else
                    {

                        return new JObject();
                    }
                }
                catch { 
            
                }
                finally { 
            
                }
                return new JObject();

        }
        }
    
}
