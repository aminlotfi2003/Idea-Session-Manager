using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.DomainEvents;

public sealed class InnovationEventCreatedDomainEvent : IDomainEvent
{
    public InnovationEventCreatedDomainEvent(Guid eventId)
    {
        EventId = eventId;
        OccurredOn = DateTimeOffset.UtcNow;
    }

    public Guid EventId { get; }
    public DateTimeOffset OccurredOn { get; }
}
