using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.UseCases.Accounts.Query.GetAccountById;

public sealed record GetAccountByIdQuery
    (Guid Id) : IQuery<AccountResponse>;
