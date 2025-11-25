using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Events;

public sealed class InnovationEventPublishedDomainEvent : IDomainEvent
{
    public InnovationEventPublishedDomainEvent(Guid eventId)
    {
        EventId = eventId;
        OccurredOn = DateTimeOffset.UtcNow;
    }

    public Guid EventId { get; }
    public DateTimeOffset OccurredOn { get; }
}
