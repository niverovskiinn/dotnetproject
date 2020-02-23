using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.Services;
using Messenger.Services.Utility;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(RegForm data)
        {
            try
            {
                await _service.SignInUser(data);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult<User>> SignIn(SignInForm data)
        {
            var u = await _service.LogIn(data);
            return u == null ? StatusCode(215, "There is no such user!") : Ok(u);
        }
        
        [HttpPost("signout")]
        public async Task<ActionResult> SignOut(SignOutForm data)
        {
            try
            {
                await _service.SignOut(data);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(int id, string token, string data)
        {
            return Ok(await _service.GetUserList(id, token, data));
        }
        
    }
}