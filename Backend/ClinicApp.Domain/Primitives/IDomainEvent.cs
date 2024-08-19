using MediatR;

namespace ClinicApp.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
