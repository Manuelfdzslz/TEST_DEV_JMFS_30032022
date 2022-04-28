using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogIn m)
        {
            User r = new User();
            StringContent content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
            var resp = client.PostAsync($"api/Authentications", content);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                r = JsonConvert.DeserializeObject<User>(res);
            }
            return RedirectToAction("Index");

        }
    }
}
