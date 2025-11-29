using ISM.SharedKernel.Common.Domain;
using System.Linq.Expressions;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IRepository<T> where T : Entity
{
    IQueryable<T> Query();

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void Update(T entity);

    void Remove(T entity);
}
