namespace ISM.SharedKernel.Common.Domain;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }
    Guid? CreatedBy { get; set; }
    DateTimeOffset? ModifiedAt { get; set; }
    Guid? ModifiedBy { get; set; }
}
