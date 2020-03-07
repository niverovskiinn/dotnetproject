using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.Services;
using Messenger.Services.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        private readonly ILogger<UsersController> _logger;
        public UsersController(UsersService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(SignUpForm form)
        {
            try
            {
                await _service.SignUpUser(form);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.ToString());
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult<User>> SignIn(SignInForm form)
        {
            var u = await _service.LogIn( form.Login, form.Password);
            return u == null ? StatusCode(215, "There is no such user!") : Ok(u);
        }

        [HttpPost("signout")]
        public async Task<ActionResult> SignOut(SignOutForm form)
        {
            try
            {
                await _service.SignOut(form.Id, form.Token);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(int id, string token, string data)
        {
            try
            {
                return Ok(await _service.GetUserList(id,token,data));
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
    }
}