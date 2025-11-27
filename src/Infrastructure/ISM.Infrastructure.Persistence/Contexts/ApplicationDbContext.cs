using ISM.Application.Common.Abstractions.Persistence;
using ISM.Domain.Entities;
using ISM.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISM.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<InnovationEvent> InnovationEvents => Set<InnovationEvent>();
    public DbSet<Idea> Ideas => Set<Idea>();
    public DbSet<ParticipantProfile> Participants => Set<ParticipantProfile>();
    public DbSet<Judge> Judges => Set<Judge>();
    public DbSet<EvaluationCriteria> EvaluationCriteria => Set<EvaluationCriteria>();
    public DbSet<IdeaEvaluation> IdeaEvaluations => Set<IdeaEvaluation>();
    public DbSet<EvaluationScore> EvaluationScores => Set<EvaluationScore>();

    public DbSet<UserLoginHistory> UserLoginHistories => Set<UserLoginHistory>();
    public DbSet<UserPasswordHistory> UserPasswordHistories => Set<UserPasswordHistory>();
    public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Register Fluent API Configurations
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
