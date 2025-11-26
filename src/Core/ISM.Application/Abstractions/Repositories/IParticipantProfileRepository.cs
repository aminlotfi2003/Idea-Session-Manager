using ISM.Domain.Entities;

namespace ISM.Application.Abstractions.Repositories;

public interface IParticipantProfileRepository : IRepository<ParticipantProfile>
{
    Task<ParticipantProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
