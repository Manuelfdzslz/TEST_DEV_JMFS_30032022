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
using TokaFront.Rest;

namespace TokaFront.Attributes
{
    public class Authentication : Attribute, IAuthorizationFilter
    {
        private string _controller;
        private string _action;
        private static HttpClient client;
        private readonly IRestConector _restConector;

        public Authentication(IRestConector restConector)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(AppSettings.Current.ServiceUrl);
            _restConector = restConector;

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
                    isValid =  _restConector.GetAsync<bool>(AppSettings.Current.ServiceUrl, $"api/Authentications/token", new Dictionary<string, string> { { "Authorization", token } }).Result;
                }
                catch (Exception)
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
