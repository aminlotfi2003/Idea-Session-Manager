using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Events;

public sealed class IdeaEvaluatedDomainEvent : IDomainEvent
{
    public IdeaEvaluatedDomainEvent(Guid ideaId, Guid eventId, double finalScore, OverallDecision decision)
    {
        IdeaId = ideaId;
        EventId = eventId;
        FinalScore = finalScore;
        Decision = decision;
        OccurredOn = DateTimeOffset.UtcNow;
    }

    public Guid IdeaId { get; }
    public Guid EventId { get; }
    public double FinalScore { get; }
    public OverallDecision Decision { get; }
    public DateTimeOffset OccurredOn { get; }
}
