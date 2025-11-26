using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISM.Infrastructure.Persistence.Repositories;

public class ParticipantProfileRepository : Repository<ParticipantProfile>, IParticipantProfileRepository
{
    public ParticipantProfileRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ParticipantProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.ApplicationUserId == userId, cancellationToken);
    }
}
