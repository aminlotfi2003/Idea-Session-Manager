namespace ISM.SharedKernel.Common.Domain;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent @event)
        => _domainEvents.Add(@event);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
