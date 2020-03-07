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
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService _service;

        public MessagesController(MessagesService service)
        {
            _service = service;
        }

        [HttpPost("send")]
        public async Task<ActionResult> SendMessage(SendMessageForm form)
        {
            try
            {
                await _service.SendMessage(form.Id, form.Token, form.IdDial, form.Data);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }

        [HttpPost("remove")]
        public async Task<ActionResult> RemoveMessage(RemoveMessageForm form)
        {
            try
            {
                await _service.RemoveMessage(form.Id, form.Token, form.IdMess);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
        
        [HttpPost("edit")]
        public async Task<ActionResult> EditMessage(EditMessageForm form)
        {
            try
            {
                await _service.EditMessage(form.Id, form.Token, form.IdMess,form.Data);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
        
        [HttpGet("dial")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int id, string token, int idDial)
        {
            try
            {
                return Ok(await _service.GetAll(id, token, idDial));
            }
            catch (Exception e)
            {
                return StatusCode(215, e.Message);
            }
        }
    }
}