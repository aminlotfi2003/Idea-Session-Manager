using ISM.WebApp.Services.ApiClients.Models.Evaluation;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IEvaluationsApiClient
{
    Task<IEnumerable<EvaluationDto>> GetByIdeaAsync(Guid ideaId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, EvaluationUpdateRequest request, CancellationToken cancellationToken = default);
}
