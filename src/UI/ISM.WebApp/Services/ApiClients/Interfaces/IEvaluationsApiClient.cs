using ISM.WebApp.Services.ApiClients.Models.Common;
using ISM.WebApp.Services.ApiClients.Models.Evaluation;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IEvaluationsApiClient
{
    Task<PaginatedResult<JudgeAssignedIdeaDto>?> GetAssignedIdeasAsync(Guid eventId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
