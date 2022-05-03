using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokaApi.Attributes;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [TkAuth]
    public class PersonasFisicaController : ControllerBase
    {
        private readonly TokaContext _context;
        private IPersonaFisca _personaFisica;

        public PersonasFisicaController(TokaContext context,IPersonaFisca personaFisca)
        {
            _context = context;
            _personaFisica = personaFisca;
        }

        // GET: api/PersonasFisica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasFisica>>> GetPersonasFisicas()
        {
            List<PersonasFisica> list = new List<PersonasFisica>();
            return list = (await _personaFisica.GetAsync()).ToList() ;
        }

        // GET: api/PersonasFisica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonasFisica>> GetPersonasFisica(int id)
        {

            var PersonasFisica = await _personaFisica.GetByIDAsync(id);

            if (PersonasFisica == null)
            {
                return NotFound();
            }

            return PersonasFisica;
        }

        // PUT: api/PersonasFisica/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutPersonasFisica(int id, PersonasFisica personasFisica)
        {
            try
            {
                if (id != personasFisica.IdPersonaFisica)
                {
                    return BadRequest();
                }

                var PersonasFisica = await _personaFisica.GetByIDAsync(id);

                if (PersonasFisica == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string errors = "";
                    foreach (var error in ModelState.Values)
                    {
                        errors += String.Concat(",", error.Errors.Select(x => x.ErrorMessage).ToArray());
                    }
                    return Problem(errors);
                }
                var res = await _personaFisica.PutAsync(personasFisica);

                return Ok(res);

            }
            catch (Exception ex)
            {
                ApiResponse r = new ApiResponse();
                r.Code = "500";
                r.Message = ex.Message;
                r.Errors.Add(ex.Message);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(r, options);
                return Problem(jsonString);
            }
            
        }

        // POST: api/PersonasFisica
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> PostPersonasFisica([FromBody]PersonasFisica personaFisica)
        {

                ApiResponse resp = await _personaFisica.PostAsync(personaFisica);

                return Ok(resp);
            
            
           
           
        }

        // DELETE: api/PersonasFisica/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePersonasFisica(int id)
        {
            var Per = await _personaFisica.GetByIDAsync(id);
            if (Per == null)
            {
                return NotFound();
            }

            var r= await _personaFisica.DeleteAsync(id);

            return Ok(r);
        }

        
    }
}
