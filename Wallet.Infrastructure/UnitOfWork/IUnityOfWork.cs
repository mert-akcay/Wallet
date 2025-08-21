using Wallet.Domain.Entities;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Infrastructure.UnitOfWork
{
    public interface IUnityOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        void SaveChanges(CancellationToken token);
        Task SaveChangesAsync(CancellationToken token);
    }
}
