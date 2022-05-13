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
using TokaFront.Interfaces;
using TokaFront.Models;

namespace TokaFront.Controllers
{
    public class LoginController : ParentController
    {
        private IHttpContextAccessor _httpContextAccessor;
        protected readonly IRestConector _restConector;

        public LoginController(IHttpContextAccessor httpContextAccessor, IRestConector restConector)
        {
            _httpContextAccessor = httpContextAccessor;
            _restConector = restConector;
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

            if (!ModelState.IsValid)
            {
                return View("Index", m);
            }

            try
            {
                User r = new User();
                r = await _restConector.PostAsync<User, LogIn>(AppSettings.Current.ServiceUrl, $"api/Authentications", m);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("toka-token-app",
                r.Token.ToString(), new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7),
                    IsEssential = true
                });
                return RedirectToAction("Index", "PersonaFisica");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");

            }


        }

        [HttpGet]
        [ServiceFilter(typeof(Authentication))]
        public async Task<IActionResult> LogOutAsync()
        {
            try
            {
                User r = new User();
                r.UserID = int.Parse(GetTokenValue("UserId"));
                r = await _restConector.PutAsync<User, User>(AppSettings.Current.ServiceUrl, $"api/Authentications/{r.UserID}", r, new Dictionary<string, string> { { "Authorization", GetTokenValue("Token") } });
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("toka-token-app");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");

            }
            
        }
    }
}
