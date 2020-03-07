using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.DataAccess.UnitOfWork;
using Models;

namespace Messenger.Services
{
    public class MessagesService
    {
        private readonly DialoguesService _dialoguesService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UsersService _usersService;


        public MessagesService(IUnitOfWork unitOfWork, UsersService usersService, DialoguesService dialoguesService)
        {
            _unitOfWork = unitOfWork;
            _usersService = usersService;
            _dialoguesService = dialoguesService;
        }

        public async Task SendMessage(int id, string token, int idDial, string data)
        {
            if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
            if (!await _dialoguesService.IsDialogue(idDial)) throw new Exception("Error! No dialogue with such id");
            _unitOfWork.Repository<Message>().Add(new Message
            {
                Data = data,
                DialogueId = idDial,
                MsgTime = DateTime.Now,
                UserIdFrom = id
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveMessage(int id, string token, int idMess)
        {
            if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
            if (!await IsMessage(idMess)) throw new Exception("Error! No message with such id");
            var mes = await _unitOfWork.Repository<Message>().GetAsync(m => m.Id == idMess);
            _unitOfWork.Repository<Message>().Delete(mes);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<bool> IsMessage(int id)
        {
            var m = await _unitOfWork.Repository<Message>().GetAsync(m => m.Id == id);
            return m != null;
        }

        public async Task EditMessage(int id, string token, int idMess, string data)
        {
            if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
            if (!await IsMessage(idMess)) throw new Exception("Error! No message with such id");
            var mes = await _unitOfWork.Repository<Message>().GetAsync(m => m.Id == idMess);
            mes.Data = data;
            _unitOfWork.Repository<Message>().Update(mes);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAll(int id, string token, int idDial)
                {
                    if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
                    var ms = await _unitOfWork.Repository<Message>().GetListAsync(
                                                                  m => m.DialogueId == idDial);
                    return ms;
                }
    }
}