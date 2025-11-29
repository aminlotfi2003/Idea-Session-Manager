using ISM.Domain.Entities;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IIdeaRepository : IRepository<Idea>
{
    IQueryable<Idea> QueryByEventId(Guid eventId);

    Task<Idea?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}
