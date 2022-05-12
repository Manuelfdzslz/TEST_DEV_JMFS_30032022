using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TokaFront.Interfaces;
using TokaFront.Models;

namespace TokaFront.Attributes
{
    public class Authentication : Attribute, IAuthorizationFilter
    {
        private string _controller;
        private string _action;
        private static HttpClient client;

        public Authentication()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(AppSettings.Current.ServiceUrl);

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isValid = false;

            _controller = "Login";
            _action = "index";

            string token = context.HttpContext.Request.Cookies["toka-token-app"];
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                    var resp = client.GetAsync($"api/Authentications/token");
                    HttpResponseMessage response = resp.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        isValid = JsonConvert.DeserializeObject<bool>(res);

                    } 

                }
                catch (Exception ex)
                {
                }
            }

            if (!isValid)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = _controller, action = _action }));
            }
        }

        
    }
}
