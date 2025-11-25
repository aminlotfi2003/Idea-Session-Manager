namespace ISM.SharedKernel.Common.Domain;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
