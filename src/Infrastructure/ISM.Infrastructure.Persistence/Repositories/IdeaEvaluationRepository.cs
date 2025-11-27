using ISM.Application.Common.Abstractions.Repositories.Application;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISM.Infrastructure.Persistence.Repositories;

public class IdeaEvaluationRepository : Repository<IdeaEvaluation>, IIdeaEvaluationRepository
{
    public IdeaEvaluationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<IdeaEvaluation>> GetByIdeaIdAsync(Guid ideaId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(e => e.Scores)
            .Where(e => e.IdeaId == ideaId)
            .ToListAsync(cancellationToken);
    }
}
