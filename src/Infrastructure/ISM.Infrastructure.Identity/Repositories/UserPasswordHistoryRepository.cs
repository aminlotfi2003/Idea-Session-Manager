using ISM.Application.Abstractions.Repositories.Identity;
using ISM.Domain.Identity;
using ISM.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISM.Infrastructure.Identity.Repositories;

public class UserPasswordHistoryRepository(ApplicationDbContext context) : IUserPasswordHistoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IReadOnlyList<UserPasswordHistory>> GetRecentAsync(
        Guid userId,
        int count,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserPasswordHistories
            .Where(history => history.UserId == userId)
            .OrderByDescending(history => history.ChangedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UserPasswordHistory history, CancellationToken cancellationToken = default)
    {
        await _context.UserPasswordHistories.AddAsync(history, cancellationToken);
    }

    public async Task PruneExcessAsync(Guid userId, int maxEntries, CancellationToken cancellationToken = default)
    {
        var toRemove = await _context.UserPasswordHistories
            .Where(history => history.UserId == userId)
            .OrderByDescending(history => history.ChangedAt)
            .Skip(maxEntries)
            .ToListAsync(cancellationToken);

        if (toRemove.Count != 0)
            _context.UserPasswordHistories.RemoveRange(toRemove);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
