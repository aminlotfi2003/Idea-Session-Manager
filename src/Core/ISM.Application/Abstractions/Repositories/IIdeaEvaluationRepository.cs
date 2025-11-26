using ISM.Domain.Entities;

namespace ISM.Application.Abstractions.Repositories;

public interface IIdeaEvaluationRepository : IRepository<IdeaEvaluation>
{
    Task<List<IdeaEvaluation>> GetByIdeaIdAsync(Guid ideaId, CancellationToken cancellationToken = default);
}
