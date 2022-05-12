using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    [Authentication]
    public class PersonafisicaController : ParentController
    {
        protected readonly IRestConector _restConector;

        public PersonafisicaController( IRestConector restConector)
        {
            _restConector = restConector;
        }
        // GET: PersonafisicaController
        public async Task<ActionResult> IndexAsync()
        {
            
            List<PersonasFisica> r = new List<PersonasFisica>();
            try
            {
               r=await _restConector.GetAsync<List<PersonasFisica>>(AppSettings.Current.ServiceUrl, $"api/PersonasFisica", new Dictionary<string, string> { { "Authorization", GetTokenValue("Token") } });
            }
            catch (Exception)
            {

            }
         
            return View(r);
        }

        // GET: PersonafisicaController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            PersonasFisica r = new PersonasFisica();

            try
            {
                r = await _restConector.GetAsync<PersonasFisica>(AppSettings.Current.ServiceUrl, $"api/PersonasFisica/{id}", new Dictionary<string, string> { { "Authorization", GetTokenValue("Token") } });
                return Json(new { IsSuccess = true, r });
            }
            catch (Exception ex)
            {
                var apiRes = JsonConvert.DeserializeObject<ApiResponse>(ex.Message);
                return Json(new { IsSuccess = apiRes.IsSuccess, Message = apiRes.Message});

            }

        }


        // POST: PersonafisicaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(PersonasFisica m)
        {

            if (!ModelState.IsValid)
            {
                var lErrors = ModelState.Values.Where(v => v.Errors.Count > 0)
                       .SelectMany(v => v.Errors)
                       .Select(v => v.ErrorMessage)
                       .ToArray();

                string errors = "";
                
                errors = String.Join(",", lErrors);

                return Json(new { IsSuccess = false, Message = errors });
            }

            m.UsuarioAgrega = int.Parse(GetTokenValue("UserId"));
            ApiResponse r = new ApiResponse();
            try
            {
                r = await _restConector.PostAsync<ApiResponse, PersonasFisica>(AppSettings.Current.ServiceUrl, $"api/PersonasFisica/", m, new Dictionary<string, string> { { "Authorization", GetTokenValue("Token") } });
                return Json(new { IsSuccess = r.IsSuccess, Message = r.Message });
            }
            catch (Exception ex)
            {
                var apiRes = JsonConvert.DeserializeObject<ApiResponse>(ex.Message);
                return Json(new { IsSuccess = apiRes.IsSuccess, Message = apiRes.Message });

            }

        }


        // POST: PersonafisicaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync(PersonasFisica m)
        {
            if (!ModelState.IsValid)
            {
                var lErrors = ModelState.Values.Where(v => v.Errors.Count > 0)
                       .SelectMany(v => v.Errors)
                       .Select(v => v.ErrorMessage)
                       .ToArray();

                string errors = "";

                errors = String.Join(",", lErrors);

                return Json(new { IsSuccess = false, Message = errors });
            }

            m.UsuarioAgrega = int.Parse(GetTokenValue("UserId"));
            ApiResponse r = new ApiResponse();
            try
            {
                r = await _restConector.PutAsync<ApiResponse, PersonasFisica>(AppSettings.Current.ServiceUrl, $"api/PersonasFisica/{m.IdPersonaFisica}", m, new Dictionary<string, string> { { "Authorization", GetTokenValue("Token") } });
                return Json(new { IsSuccess = r.IsSuccess, Message = r.Message });
            }
            catch (Exception ex)
            {
                var apiRes = JsonConvert.DeserializeObject<ApiResponse>(ex.Message);
                return Json(new { IsSuccess = apiRes.IsSuccess, Message = apiRes.Message });

            }


        }

        

        // POST: PersonafisicaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            ApiResponse r = new ApiResponse();
            try
            {
                r = await _restConector.DeleteAsync<ApiResponse>(AppSettings.Current.ServiceUrl, $"api/PersonasFisica/{id}", new Dictionary<string, string> { { "Authorization", GetTokenValue("Token") } });
                return Json(new { IsSuccess = r.IsSuccess, Message = r.Message });
            }
            catch (Exception ex)
            {
                var apiRes = JsonConvert.DeserializeObject<ApiResponse>(ex.Message);
                return Json(new { IsSuccess = apiRes.IsSuccess, Message = apiRes.Message });

            }
        }
    }
}
