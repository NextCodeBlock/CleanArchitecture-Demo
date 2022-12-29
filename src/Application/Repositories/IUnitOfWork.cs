using CleanArchitectureDemo.Domain.Contracts;

namespace CleanArchitectureDemo.Application.Repositories;

public interface IUnitOfWork<TId> : IDisposable
{
    IRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>;
    Task<int> Commit(CancellationToken cancellationToken);
    Task Rollback();
}