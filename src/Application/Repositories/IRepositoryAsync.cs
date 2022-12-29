using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CleanArchitectureDemo.Domain.Contracts;

namespace CleanArchitectureDemo.Application.Repositories;

public interface IRepositoryAsync<TEntity, in TId> where TEntity : class, IEntity<TId>
{
    IQueryable<TEntity> Entities { get; }
    Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity);
}