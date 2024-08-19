using ClinicApp.Domain.Primitives;
using MediatR;

namespace ClinicApp.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
