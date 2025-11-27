using ISM.Domain.Entities;
using ISM.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace ISM.Application.Common.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DbSet<ParticipantProfile> Participants { get; }
    DbSet<Judge> Judges { get; }
    DbSet<UserLoginHistory> UserLoginHistories { get; }
    DbSet<UserPasswordHistory> UserPasswordHistories { get; }
    DbSet<UserRefreshToken> UserRefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
