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
using TokaFront.Models;

namespace TokaFront.Controllers
{
    public class PersonafisicaController : ParentController
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        public PersonafisicaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: PersonafisicaController
        public async Task<ActionResult> IndexAsync()
        {
            List<PersonasFisica> r = new List<PersonasFisica>();
            var client = _httpClientFactory.CreateClient("Api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetTokenValue("Token"));
            var resp = client.GetAsync($"api/PersonasFisica");
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                r = JsonConvert.DeserializeObject<List<PersonasFisica>>(res);

            }
            
            return View(r);
        }

        // GET: PersonafisicaController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            PersonasFisica r = new PersonasFisica();
            var client = _httpClientFactory.CreateClient("Api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetTokenValue("Token"));
            var resp = client.GetAsync($"api/PersonasFisica/"+id);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                r = JsonConvert.DeserializeObject<PersonasFisica>(res);
               return Json(new { IsSuccess = true, r });

            }
            return Json(new { IsSuccess = false, Message = "Not found" });



        }


        // POST: PersonafisicaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(PersonasFisica m)
        {
            try
            {
                m.UsuarioAgrega = int.Parse(GetTokenValue("UserId"));
                PersonasFisica r = new PersonasFisica();
                var client = _httpClientFactory.CreateClient("Api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetTokenValue("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                var resp = client.PostAsync($"api/PersonasFisica", content);
                HttpResponseMessage response = resp.Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    r = JsonConvert.DeserializeObject<PersonasFisica>(res);
                    return Json(new { IsSuccess = true, Message = "Registro guardado con exito" });
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                   var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(res);
                    return Json(new { IsSuccess = false, Message =string.Join(",", errorResponse.Errors.ToArray()) });
                }
                
            } 
            catch(Exception ex)
            {
                return Json(new { IsSuccess = false, Message=ex.Message });
            }
        }

       
        // POST: PersonafisicaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync(PersonasFisica m)
        {
            try
            {
                m.UsuarioAgrega = int.Parse(GetTokenValue("UserId"));
                PersonasFisica r = new PersonasFisica();
                var client = _httpClientFactory.CreateClient("Api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetTokenValue("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                var resp = client.PutAsync($"api/PersonasFisica/"+m.IdPersonaFisica, content);
                HttpResponseMessage response = resp.Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    r = JsonConvert.DeserializeObject<PersonasFisica>(res);
                    return Json(new { IsSuccess = true, Message = "Registro actualizado con exito" });
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(res);
                    return Json(new { IsSuccess = false, Message = string.Join(",", errorResponse.Errors.ToArray()) });
                }

            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }

        

        // POST: PersonafisicaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                PersonasFisica r = new PersonasFisica();
                var client = _httpClientFactory.CreateClient("Api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetTokenValue("Token"));
                var resp = client.DeleteAsync($"api/PersonasFisica/" + id);
                HttpResponseMessage response = resp.Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return Json(new { IsSuccess = true, Message = "Registro actualizado con exito" });
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(res);
                    return Json(new { IsSuccess = false, Message = string.Join(",", errorResponse.Errors.ToArray()) });
                }

            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
