using ISM.Domain.Entities;

namespace ISM.Application.Abstractions.Repositories;

public interface IJudgeRepository : IRepository<Judge>
{
    Task<Judge?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
