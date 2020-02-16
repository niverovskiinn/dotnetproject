using Messenger.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Messenger.Services
{
    public class DialoguesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DialoguesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}