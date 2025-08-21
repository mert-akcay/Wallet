using Wallet.Domain.Entities;
using Wallet.Infrastructure.DbContext;
using Wallet.Infrastructure.Repositories;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnityOfWork
    {
        private bool disposed = false;

        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync();
        }


        public void SaveChanges(CancellationToken token)
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
