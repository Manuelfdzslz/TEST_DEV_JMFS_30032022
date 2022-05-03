using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TokaFront.Attributes;
using TokaFront.Models;

namespace TokaFront.Controllers
{
    public class LoginController : ParentController
    {
        private IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInAsync(LogIn m)
        {
            User r = new User();
            var client = _httpClientFactory.CreateClient("Api");
            StringContent content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
            var resp = client.PostAsync($"api/Authentications", content);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                r = JsonConvert.DeserializeObject<User>(res);

                _httpContextAccessor.HttpContext.Response.Cookies.Append("toka-token-app",
                r.Token.ToString(), new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7),
                    IsEssential = true
                });
                return RedirectToAction("Index","PersonaFisica");
            }
            else
            {
                return RedirectToAction("Index");
            }
            

        }

        [HttpGet]
        [Authentication]
        public async Task<IActionResult> LogOutAsync()
        {
            User r = new User();
            r.UserID=int.Parse(GetTokenValue("UserId"));
            var client = _httpClientFactory.CreateClient("Api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetTokenValue("Token"));
            StringContent content = new StringContent(JsonConvert.SerializeObject(r), Encoding.UTF8, "application/json");
            var resp = client.PutAsync($"api/Authentications/{r.UserID}", content);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                r = JsonConvert.DeserializeObject<User>(res);
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("toka-token-app");
               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
    }
}
