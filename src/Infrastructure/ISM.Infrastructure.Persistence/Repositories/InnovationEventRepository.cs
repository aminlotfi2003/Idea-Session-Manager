using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;

namespace ISM.Infrastructure.Persistence.Repositories;

public class InnovationEventRepository : Repository<InnovationEvent>, IInnovationEventRepository
{
    public InnovationEventRepository(ApplicationDbContext context) : base(context)
    {
    }
}
