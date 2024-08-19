using ClinicApp.Domain.Shared;
using MediatR;

namespace ClinicApp.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
