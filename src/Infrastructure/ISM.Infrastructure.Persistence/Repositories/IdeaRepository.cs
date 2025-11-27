using ISM.Application.Common.Abstractions.Repositories.Application;
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
            .Include(i => i.ConfidentialLink)
            .ToListAsync(cancellationToken);
    }

    public async Task<Idea?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(i => i.ConfidentialLink)
            .Include(i => i.Evaluations)
            .ThenInclude(e => e.Scores)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}
