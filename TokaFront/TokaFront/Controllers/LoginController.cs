using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
            

        }
    }
}
