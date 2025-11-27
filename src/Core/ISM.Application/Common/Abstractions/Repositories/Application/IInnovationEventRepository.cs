using ISM.Domain.Entities;
using ISM.Domain.Enums;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IInnovationEventRepository : IRepository<InnovationEvent>
{
    Task<InnovationEvent?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<InnovationEvent>> GetByStatusAsync(IEnumerable<EventStatus> statuses, CancellationToken cancellationToken = default);

    Task<bool> AnyOverlappingAsync(DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
}
