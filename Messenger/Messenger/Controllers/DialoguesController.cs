
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Messenger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DialoguesController: ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public DialoguesController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet]
        public async Task<IEnumerable<Dialogue>> Get()
        {
            return await _unit.Repository<Dialogue>().GetAllAsync();
        }
    }
}