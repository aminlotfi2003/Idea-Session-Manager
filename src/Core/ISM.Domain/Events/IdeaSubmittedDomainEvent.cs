using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Events;

public sealed class IdeaSubmittedDomainEvent : IDomainEvent
{
    public IdeaSubmittedDomainEvent(Guid ideaId, Guid eventId)
    {
        IdeaId = ideaId;
        EventId = eventId;
        OccurredOn = DateTimeOffset.UtcNow;
    }

    public Guid IdeaId { get; }
    public Guid EventId { get; }
    public DateTimeOffset OccurredOn { get; }
}
