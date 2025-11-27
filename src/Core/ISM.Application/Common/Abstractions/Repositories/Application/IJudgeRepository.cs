using ISM.Domain.Entities;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IJudgeRepository : IRepository<Judge>
{
    Task<Judge?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
