using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Events;

public sealed class InnovationEventResultsFinalizedDomainEvent : IDomainEvent
{
    public InnovationEventResultsFinalizedDomainEvent(Guid eventId)
    {
        EventId = eventId;
        OccurredOn = DateTimeOffset.UtcNow;
    }

    public Guid EventId { get; }
    public DateTimeOffset OccurredOn { get; }
}
