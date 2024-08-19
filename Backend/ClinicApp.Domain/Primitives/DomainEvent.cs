namespace ClinicApp.Domain.Primitives;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
