using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaFront.Interfaces
{
    public interface IRestConector
    {
        Task<T> GetAsync<T>(string baseurl, string requestUri, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T, TP>(string baseurl, string requestUri, TP postData, Dictionary<string, string> headers = null);
        Task<T> PutAsync<T, TP>(string baseurl, string requestUri, TP postData, Dictionary<string, string> headers = null);
        Task<T> DeleteAsync<T>(string baseurl, string requestUri, Dictionary<string, string> headers = null);
    }
}
