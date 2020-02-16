using Messenger.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Messenger.Services
{
    public class MessagesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}