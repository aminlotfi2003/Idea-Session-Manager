using ISM.Domain.Entities;
using ISM.Domain.Enums;
using System.Linq;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IInnovationEventRepository : IRepository<InnovationEvent>
{
    Task<InnovationEvent?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    IQueryable<InnovationEvent> QueryByStatus(IEnumerable<EventStatus> statuses);

    Task<bool> AnyOverlappingAsync(DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
}
