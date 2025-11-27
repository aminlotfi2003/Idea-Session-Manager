using ISM.Application.Common.Abstractions.Repositories.Application;
using ISM.Domain.Entities;
using ISM.Domain.Enums;
using ISM.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ISM.Infrastructure.Persistence.Repositories;

public class InnovationEventRepository : Repository<InnovationEvent>, IInnovationEventRepository
{
    public InnovationEventRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<InnovationEvent?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(e => e.Criteria)
            .Include(e => e.EventJudges)
            .Include(e => e.Ideas)
            .ThenInclude(i => i.Evaluations)
            .ThenInclude(ev => ev.Scores)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InnovationEvent>> GetByStatusAsync(IEnumerable<EventStatus> statuses, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(e => statuses.Contains(e.Status))
            .ToListAsync(cancellationToken);
    }

    public Task<bool> AnyOverlappingAsync(DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(
            e => e.IdeaSubmissionStart <= end && e.IdeaSubmissionEnd >= start,
            cancellationToken
        );
    }
}
