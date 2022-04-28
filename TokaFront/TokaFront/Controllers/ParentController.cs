using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TokaFront.Controllers
{
    public class ParentController : Controller
    {
        private readonly string ServiceURL = "http://localhost:8556";
        protected HttpClient client;
        public ParentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ServiceURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
        
}
}
