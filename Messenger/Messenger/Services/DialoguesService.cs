using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.DataAccess.UnitOfWork;
using Models;

namespace Messenger.Services
{
    public class DialoguesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UsersService _usersService;

        public DialoguesService(IUnitOfWork unitOfWork, UsersService usersService)
        {
            _unitOfWork = unitOfWork;
            _usersService = usersService;
        }

        public async Task CreateDialogue(int id, string token, int idUser)
        {
            if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
            if (!await _usersService.IsUser(id)) throw new Exception("Error! No user with such id");

            _unitOfWork.Repository<Dialogue>().Add(new Dialogue
            {
                Created = DateTime.Now,
                UserIdFirst = id,
                UserIdSecond = idUser
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveDialogue(int id, string token, int idDialog)
        {
            if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
            if (!await IsDialogue(idDialog)) throw new Exception("Error! No dialogue with such id");
            var mesgs = await _unitOfWork.Repository<Message>().GetListAsync(
                m => m.DialogueId == idDialog);
            foreach (var message in mesgs)
                _unitOfWork.Repository<Message>().Delete(message);
            _unitOfWork.Repository<Dialogue>().Delete(
                await _unitOfWork.Repository<Dialogue>().GetAsync(d => d.Id == idDialog)
            );
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsDialogue(int id)
        {
            var d = await _unitOfWork.Repository<Dialogue>().GetAsync(d => d.Id == id);
            return d != null;
        }

        public async Task<IEnumerable<Dialogue>> GetAll(int id, string token)
        {
            if (!await _usersService.IsUserAuth(id, token)) throw new Exception("Authentication error");
            var d1 = await _unitOfWork.Repository<Dialogue>().GetListAsync(
                                                          d => d.UserIdFirst == id);
            var d2 = await _unitOfWork.Repository<Dialogue>().GetListAsync(
                d => d.UserIdSecond == id);
            return d1.Concat(d2).OrderBy(d => d.Created);
        }
    }
}