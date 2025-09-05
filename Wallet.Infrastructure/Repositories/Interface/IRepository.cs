using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Repositories.Interface;

/// <summary>
/// Tüm repository'ler için temel CRUD ve sorgu işlemlerini tanımlayan genel arayüz.
/// </summary>
/// <typeparam name="TEntity">İşlem yapılacak entity tipi (Örn: Product, Category)</typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Belirtilen ID'ye sahip entity'yi asenkron olarak bulur.
    /// </summary>
    /// <param name="id">Aranan entity'nin ID'si.</param>
    /// <returns>Bulunan entity veya bulunamazsa null.</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Tüm entity'leri asenkron olarak getirir.
    /// </summary>
    /// <returns>Entity listesi.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Belirtilen bir koşula uyan tüm entity'leri asenkron olarak bulur.
    /// </summary>
    /// <param name="predicate">Filtreleme koşulu (lambda expression).</param>
    /// <returns>Koşula uyan entity listesi.</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Yeni bir entity'yi veritabanına asenkron olarak ekler.
    /// </summary>
    /// <param name="entity">Eklenecek entity.</param>
    Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

    /// <summary>
    /// Birden fazla entity'yi veritabanına asenkron olarak ekler.
    /// </summary>
    /// <param name="entities">Eklenecek entity listesi.</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Mevcut bir entity'yi günceller.
    /// (Bu metod genellikle senkron bırakılır çünkü sadece entity'nin state'ini değiştirir).
    /// </summary>
    /// <param name="entity">Güncellenecek entity.</param>
    bool Update(TEntity entity);

    /// <summary>
    /// Verilen bir entity'yi veritabanından siler.
    /// </summary>
    /// <param name="entity">Silinecek entity.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// Birden fazla entity'yi veritabanından siler.
    /// </summary>
    /// <param name="entities">Silinecek entity listesi.</param>
    void RemoveRange(IEnumerable<TEntity> entities);
}
