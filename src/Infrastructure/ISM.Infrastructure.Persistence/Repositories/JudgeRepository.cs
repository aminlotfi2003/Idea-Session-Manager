using ISM.Application.Common.Abstractions.Repositories.Application;
using ISM.Domain.Entities;
using ISM.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISM.Infrastructure.Persistence.Repositories;

public class JudgeRepository : Repository<Judge>, IJudgeRepository
{
    public JudgeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Judge?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(j => j.ApplicationUserId == userId, cancellationToken);
    }
}
