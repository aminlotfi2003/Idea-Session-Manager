using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISM.Infrastructure.Persistence.Repositories;

public class IdeaRepository : Repository<Idea>, IIdeaRepository
{
    public IdeaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Idea>> GetByEventIdAsync(Guid innovationEventId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(i => i.InnovationEventId == innovationEventId)
            .ToListAsync(cancellationToken);
    }
}
