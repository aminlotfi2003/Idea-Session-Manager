using ISM.Domain.Entities;

namespace ISM.Application.Common.Abstractions.Repositories.Application;

public interface IIdeaEvaluationRepository : IRepository<IdeaEvaluation>
{
    Task<List<IdeaEvaluation>> GetByIdeaIdAsync(Guid ideaId, CancellationToken cancellationToken = default);
}
