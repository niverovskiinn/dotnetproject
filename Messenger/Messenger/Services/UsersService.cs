using Messenger.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Messenger.Services
{
    public class UsersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SignInUser(object data)
        {
            throw new NotImplementedException();
        }

        public async Task<User> LogIn(object data)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUserList(object data)
        {
            throw new NotImplementedException();
        }
    }
}