using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Messenger.DataAccess.UnitOfWork;
using Messenger.Services.Utility;
using Models;

namespace Messenger.Services
{
    public class UsersService
    {
        private const int MinutesToSignOut = 15;
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task SignUpUser(SignUpForm form)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Login == form.Login);
            if (user == null)
            {
                _unitOfWork.Repository<User>().Add(new User
                {
                    Login = form.Login,
                    Password = Utilities.ComputeHash(form.Password, new MD5CryptoServiceProvider()),
                    Birthday = DateTime.Parse(form.Birthday),
                    Name = form.Name,
                    SignInTime = DateTime.Now
                });
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User with that login already exists.");
            }
        }

        public async Task<User> LogIn(string login, string password)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(
                u => u.Login == login &&
                     u.Password == Utilities.ComputeHash(password, new MD5CryptoServiceProvider()));

            if (user == null) return null;

            user.Token = Utilities.ComputeToken();
            user.SignInTime = DateTime.Now;
            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetUserList(int id, string token, string data)
        {
            if (!await IsUserAuth(id, token)) throw new Exception("Authentication error");

            var users = await _unitOfWork.Repository<User>().GetListAsync(u => u.Login.Contains(data)
                                                                               || u.Name.Contains(data));
            var userList = users.ToList();
            foreach (var user in userList)
            {
                user.Password = null;
                user.Token = null;
            }

            return userList;
        }

        public async Task SignOut(int id, string token)
        {
            if (!await IsUserAuth(id, token))
                throw new Exception("Unexpected!");

            var us = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == id);
            us.Token = null;
            _unitOfWork.Repository<User>().Update(us);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsUserAuth(int id, string token)
        {
            var u = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == id && u.Token == token);
            return u != null && (DateTime.Now - u.SignInTime).TotalMinutes < MinutesToSignOut;
        }

        public async Task<bool> IsUser(int id)
        {
            var u = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == id);
            return u != null;
        }
    }
}