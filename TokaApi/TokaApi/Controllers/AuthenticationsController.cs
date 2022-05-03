using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        protected string Token
        {
            get
            {
                if (Request == null)
                    return "";

                if (string.IsNullOrWhiteSpace(Request.Headers["Authorization"].ToString()))
                    return "";

                var tk = (Request.Headers["Authorization"].ToString()).Replace("bearer ", "").Replace("Bearer ", "").Trim();
                return tk;
            }
        }

        public AuthenticationsController(IAuthService service)
        {
            _service = service;
        }

        // Get api/token
        [HttpGet("token")]
        public async Task<bool> GetTokenAsync()
        {
           
            if (!String.IsNullOrEmpty(Token))
            {
                var isvalid = _service.GetTokenAsync(Token);
                return isvalid;
            }
            throw new Exception("Invalid Token");
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
        public async Task<IActionResult> PutAsync(int id, [FromBody] User m)
        {
            var res = await _service.PutLogAutAsync(m);
            return Ok(res);
        }

        // DELETE api/<AuthenticationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
