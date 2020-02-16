using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.DataAccess.UnitOfWork;
using Messenger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        public async Task<ActionResult> SignInUser(dynamic data)
        {
            await _service.SignInUser(data);
            return Ok();
        }

        public async Task<ActionResult<User>> LogIn(dynamic data)
        {
            return Ok(await _service.LogIn(data));
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(dynamic data)
        {
            return Ok(await _service.GetUserList(data));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}