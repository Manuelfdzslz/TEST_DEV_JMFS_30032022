using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TokaFront.Interfaces;
using TokaFront.Models;

namespace TokaFront.Rest
{
    public class RestConector: IRestConector
    {
        private readonly HttpClient _httpClient;
        private readonly string baseURL = AppSettings.Current.ServiceUrl;
        private static readonly MediaTypeWithQualityHeaderValue ApplicationJson =
           new MediaTypeWithQualityHeaderValue("application/json");

        public RestConector(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string baseurl, string requestUri, Dictionary<string, string> headers=null)
        {
            SetUpClient(baseurl, headers);
            var resp = _httpClient.GetAsync(requestUri);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var apiRes = JsonConvert.DeserializeObject<T>(res);
                return apiRes;
            }
            else
            {
                ApiResponse r = new ApiResponse();
                r.IsSuccess = false;
                r.Code = response.StatusCode.ToString();
                r.Message = response.ReasonPhrase;
                throw new Exception(JsonConvert.SerializeObject(r));
            }
            
        }

        public async Task<T> PostAsync<T, TP>(string baseurl, string requestUri, TP postData, Dictionary<string, string> headers = null)
        {
            SetUpClient(baseurl, headers);
            StringContent content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            var resp = _httpClient.PostAsync(requestUri,content);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var apiRes = JsonConvert.DeserializeObject<T>(res);
                return apiRes;
            }
            else
            {
                ApiResponse r = new ApiResponse();
                r.IsSuccess = false;
                r.Code = response.StatusCode.ToString();
                r.Message = response.ReasonPhrase;
                throw new Exception(JsonConvert.SerializeObject(r));
            }
        }

        public async Task<T> PutAsync<T, TP>(string baseurl, string requestUri, TP postData, Dictionary<string, string> headers = null)
        {
            SetUpClient(baseurl, headers);
            StringContent content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            var resp = _httpClient.PutAsync(requestUri, content);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var apiRes = JsonConvert.DeserializeObject<T>(res);
                return apiRes;
            }
            else
            {
                ApiResponse r = new ApiResponse();
                r.IsSuccess = false;
                r.Code = response.StatusCode.ToString();
                r.Message = response.ReasonPhrase;
                throw new Exception(JsonConvert.SerializeObject(r));
            }
        }

        public async Task<T> DeleteAsync<T>(string baseurl, string requestUri, Dictionary<string, string> headers = null)
        {
            SetUpClient(baseurl, headers);
            var resp = _httpClient.DeleteAsync(requestUri);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var apiRes = JsonConvert.DeserializeObject<T>(res);
                return apiRes;
            }
            else
            {
                ApiResponse r = new ApiResponse();
                r.IsSuccess = false;
                r.Code = response.StatusCode.ToString();
                r.Message = response.ReasonPhrase;
                throw new Exception(JsonConvert.SerializeObject(r));
            }
        }


        private void SetUpClient(string url,Dictionary<string, string> headers)
        {
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(ApplicationJson);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
          
        }
    }
}
