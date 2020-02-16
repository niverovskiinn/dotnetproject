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
    public class MessagesController: ControllerBase
    {
        private readonly MessagesService _service;

        public MessagesController(MessagesService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            return await _service.GetAllAsync();
        }
    }
}