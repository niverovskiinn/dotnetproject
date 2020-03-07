using System;
using System.Threading.Tasks;
using Messenger.DataAccess.Repository;
using Models;

namespace Messenger.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class, IEntity;
        Task SaveChangesAsync();
    }
}