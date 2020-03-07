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
    public class DialoguesController : ControllerBase
    {
        private readonly DialoguesService _service;

        public DialoguesController(DialoguesService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateDialogue(CreateDialogueForm form)
        {
            try
            {
                await _service.CreateDialogue(form.Id, form.Token, form.IdUser);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dialogue>>> GetDialogues(int id, string token)
        {
            try
            {
                return Ok(await _service.GetAll(id, token));
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }

        [HttpPost("remove")]
        public async Task<ActionResult> RemoveDialogue(RemoveDialogueForm form)
        {
            try
            {
                await _service.RemoveDialogue(form.Id, form.Token, form.IdDial);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
    }
}