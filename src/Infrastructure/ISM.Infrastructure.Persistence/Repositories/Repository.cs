using ISM.Application.Common.Abstractions.Repositories.Application;
using ISM.Infrastructure.Persistence.Contexts;
using ISM.SharedKernel.Common.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ISM.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }
}
