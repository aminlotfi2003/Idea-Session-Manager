using ISM.Domain.Entities;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IParticipantProfileRepository : IRepository<ParticipantProfile>
{
    Task<ParticipantProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
