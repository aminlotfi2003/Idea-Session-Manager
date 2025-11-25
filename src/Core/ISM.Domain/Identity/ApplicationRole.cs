using ISM.SharedKernel.Common.Domain;
using Microsoft.AspNetCore.Identity;

namespace ISM.Domain.Identity;

public class ApplicationRole : IdentityRole<Guid>, IAuditableEntity
{
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
}
