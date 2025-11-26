using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;

namespace ISM.Infrastructure.Persistence.Repositories;

public class EvaluationScoreRepository : Repository<EvaluationScore>, IEvaluationScoreRepository
{
    public EvaluationScoreRepository(ApplicationDbContext context) : base(context)
    {
    }
}
