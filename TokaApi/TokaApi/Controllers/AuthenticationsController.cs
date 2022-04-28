using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Interfaces;
using TokaApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TokaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthenticationsController(IAuthService service)
        {
            _service = service;
        }
        // POST api/<AuthenticationsController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LogIn m)
        {
           var res= await _service.PostLogIn(m);

            return Ok(res);
        }

        // PUT api/<AuthenticationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
