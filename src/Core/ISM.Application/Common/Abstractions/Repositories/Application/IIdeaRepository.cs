using ISM.Domain.Entities;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IIdeaRepository : IRepository<Idea>
{
    Task<List<Idea>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);

    Task<Idea?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}
