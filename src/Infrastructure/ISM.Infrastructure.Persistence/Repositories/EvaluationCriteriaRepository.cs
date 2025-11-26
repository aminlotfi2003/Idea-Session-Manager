using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;

namespace ISM.Infrastructure.Persistence.Repositories;

public class EvaluationCriteriaRepository : Repository<EvaluationCriteria>, IEvaluationCriteriaRepository
{
    public EvaluationCriteriaRepository(ApplicationDbContext context) : base(context)
    {
    }
}
