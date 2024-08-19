using ClinicApp.Domain.Shared;
using MediatR;

namespace ClinicApp.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}