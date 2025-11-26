using ISM.Domain.Identity;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class Judge : Entity, IAggregateRoot
{
    private Judge() { }

    public Guid ApplicationUserId { get; private set; }
    public ApplicationUser ApplicationUser { get; private set; } = null!;

    public string FullName { get; private set; } = default!;
    public string ExpertiseAreas { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public ICollection<EventJudge> EventJudges { get; private set; } = new HashSet<EventJudge>();
    public ICollection<IdeaEvaluation> Evaluations { get; private set; } = new HashSet<IdeaEvaluation>();

    public static Judge Create(Guid applicationUserId, string fullName, string expertiseAreas)
    {
        return new Judge
        {
            Id = applicationUserId,
            ApplicationUserId = applicationUserId,
            FullName = fullName,
            ExpertiseAreas = expertiseAreas,
            IsActive = true
        };
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
