using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class Judge : Entity, IAggregateRoot
{
    private Judge() { }

    public Guid UserId { get; private set; } // Relation with User
    public string FullName { get; private set; } = default!;
    public string ExpertiseAreas { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public static Judge Create(Guid userId, string fullName, string expertiseAreas)
    {
        return new Judge
        {
            UserId = userId,
            FullName = fullName,
            ExpertiseAreas = expertiseAreas,
            IsActive = true
        };
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
