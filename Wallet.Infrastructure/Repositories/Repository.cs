using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;
using Wallet.Infrastructure.DbContext;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Infrastructure.Repositories
{
    /// <summary>
    /// IBaseRepository arayüzünü Entity Framework Core kullanarak implemente eden temel sınıf.
    /// </summary>
    /// <typeparam name="TEntity">İşlem yapılacak entity tipi.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        // Veritabanı context'i. protected olarak tanımlanır ki bu sınıftan türeyen
        // özel repository'ler (ProductRepository gibi) de context'e erişebilsin.
        protected readonly ApplicationDbContext _context;

        // TEntity tipine ait DbSet'i temsil eder. Her seferinde _context.Set<TEntity>()
        // çağırmamak için bir değişkende tutulur.
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor. Gerekli DbContext'i dependency injection ile alır.
        /// </summary>
        /// <param name="context">Uygulamanın DbContext'i.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // FindAsync, primary key'e göre arama yapmak için en verimli yöntemdir.
            // Önce bellekte (context'te) arar, bulamazsa veritabanına gider.
            return await _dbSet.FindAsync(predicate);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public void Remove(TEntity entity)
        {
            // Remove ve Update metotları asenkron değildir.
            // Çünkü bu işlemler sadece entity'nin durumunu (state) "Deleted" veya "Modified"
            // olarak işaretler. Asıl veritabanı işlemi SaveChangesAsync() çağrıldığında yapılır.
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

    }
}
