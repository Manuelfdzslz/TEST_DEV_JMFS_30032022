﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> PutPersonasFisica(int id, PersonasFisica personasFisica)
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

            var res = await _personaFisica.PutAsync(personasFisica);

            return Ok();
        }

        // POST: api/PersonasFisica
        [HttpPost]
        public async Task<ActionResult<PersonasFisica>> PostPersonasFisica([FromBody]PersonasFisica personaFisica)
        {
           PersonasFisica resp=  await _personaFisica.PostAsync(personaFisica);

            return CreatedAtAction("GetPersonasFisica", new { id = resp.IdPersonaFisica }, resp);
        }

        // DELETE: api/PersonasFisica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonasFisica(int id)
        {
            var Per = await _personaFisica.GetByIDAsync(id);
            if (Per == null)
            {
                return NotFound();
            }

            await _personaFisica.DeleteAsync(id);

            return Ok();
        }

        
    }
}