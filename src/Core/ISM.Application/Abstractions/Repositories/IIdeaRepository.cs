using ISM.Domain.Entities;

namespace ISM.Application.Abstractions.Repositories;

public interface IIdeaRepository : IRepository<Idea>
{
    Task<List<Idea>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
}
