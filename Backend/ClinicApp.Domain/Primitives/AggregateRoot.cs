namespace ClinicApp.Domain.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : ValueObject
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    protected AggregateRoot() 
    {
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}
