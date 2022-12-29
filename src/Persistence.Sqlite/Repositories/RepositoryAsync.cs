using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CleanArchitectureDemo.Application.Repositories;
using CleanArchitectureDemo.Domain.Contracts;
using CleanArchitectureDemo.Persistence.Sqlite.Context;

namespace CleanArchitectureDemo.Persistence.Sqlite.Repositories;

public class RepositoryAsync<TEntity, TId> : IRepositoryAsync<TEntity, TId> where TEntity : AuditableEntity<TId>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryAsync(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> Entities => _dbContext.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<TEntity>()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<TEntity>()
            .Where(filter)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        TEntity exist = await _dbContext.Set<TEntity>().FindAsync(entity.Id, cancellationToken);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    }
}