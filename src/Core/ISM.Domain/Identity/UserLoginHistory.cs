using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Identity;

public sealed class UserLoginHistory : Entity, IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;
    public string? IpAddress { get; set; }
    public string? Host { get; set; }
    public bool Success { get; set; }
    public int FailureCountBeforeSuccess { get; set; }
}
