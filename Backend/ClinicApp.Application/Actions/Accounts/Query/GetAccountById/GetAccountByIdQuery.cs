using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Account.Responses;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Accounts.Query.GetAccountById;

public sealed record GetAccountByIdQuery
    (Guid Id) : IQuery<AccountResponse>;
