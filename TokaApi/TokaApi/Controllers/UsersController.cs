using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Interfaces;
using TokaApi.Models;

namespace TokaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _user;

        public UsersController(IUserService user)
        {
            _user = user;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            List<User> list = new List<User>();
            return list = (await _user.GetAsync()).ToList();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _user.GetByIDAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }
            var User = await _user.GetByIDAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            var res = await _user.PutAsync(user);

            return Ok();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            User resp = await _user.PostAsync(user);

            return CreatedAtAction("GetUser", new { id = resp.UserID }, resp);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _user.GetByIDAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _user.DeleteAsync(id);

            return Ok();
        }


    }
}
