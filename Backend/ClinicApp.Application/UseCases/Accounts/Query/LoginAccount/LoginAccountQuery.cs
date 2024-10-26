using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Application.UseCases.Accounts.Query.LoginAccount;

public sealed record LoginAccountQuery
    (string Username, string Password) : IQuery<LoginResponse>;
