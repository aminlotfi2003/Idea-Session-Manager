using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Identity;

public class UserPasswordHistory : Entity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public string PasswordHash { get; set; } = default!;
    public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.UtcNow;

    public static UserPasswordHistory Create(Guid userId, string passwordHash, DateTimeOffset changedAt)
        => new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PasswordHash = passwordHash,
            ChangedAt = changedAt
        };
}
