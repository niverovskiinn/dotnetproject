using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Messenger.DataAccess.UnitOfWork;
using Messenger.Services.Utility;
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

        

        public async Task SignInUser(RegForm form)
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

        public async Task<User> LogIn(SignInForm data)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(
                u => u.Login == data.Login &&
                     u.Password == Utilities.ComputeHash(data.Password, new MD5CryptoServiceProvider()));

            if (user == null) return null;

            user.Token = Utilities.ComputeToken();
            user.SignInTime = DateTime.Now;
            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetUserList(int id, string token, string data)
        {
            if (await IsUserAuth(id, token))
            {
                return await _unitOfWork.Repository<User>().GetListAsync(u => u.Login.Contains(data)
                                                                              || u.Name.Contains(data));
            }

            throw new Exception("Authentication error");
        }

        public async Task SignOut(SignOutForm data)
        {

            if (!await IsUserAuth(data.Id, data.Token))
                throw new Exception("Unexpected!");

            var us = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == data.Id);
            us.Token = null;
            _unitOfWork.Repository<User>().Update(us);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsUserAuth(int id, string token)
        {
            var u = await _unitOfWork.Repository<User>().GetAsync(u => u.Id == id && u.Token == token);
            return  u != null && (DateTime.Now - u.SignInTime).TotalMinutes > 15;
        }
    }
}