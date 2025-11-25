using ISM.SharedKernel.Common.Domain;
using Microsoft.AspNetCore.Identity;

namespace ISM.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public Guid? TenantId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public DateTimeOffset? PasswordLastChangedAt { get; set; }
    public bool MustChangePasswordOnFirstLogin { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<UserLoginHistory> LoginHistories { get; set; } = new HashSet<UserLoginHistory>();
    public ICollection<UserPasswordHistory> PasswordHistories { get; set; } = new HashSet<UserPasswordHistory>();
    public ICollection<UserRefreshToken> RefreshTokens { get; set; } = new HashSet<UserRefreshToken>();
}
