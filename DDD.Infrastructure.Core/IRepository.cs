using DDD.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Core
{
    /// <summary>
    /// 普通实体的仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        bool Remove(Entity entity);
        Task<bool> RemoveAsync(Entity entity);
    }

    /// <summary>
    /// 包含主键类型实体的仓储
    /// 拥有上面所有的方法，还可以根据主键类型操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {
        bool Delete(TKey id);
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
