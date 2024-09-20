using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Auth;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Actions.Accounts.Query.GetAccountById;

public sealed record  GetAccountByIdQuery
    (Guid Id) : IQuery<AccountResponse>;
