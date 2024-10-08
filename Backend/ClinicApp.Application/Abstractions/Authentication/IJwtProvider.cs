﻿using ClinicApp.Application.Actions.Accounts.Query.LoginAccount;
using ClinicApp.Domain.Models.Accounts;

namespace ClinicApp.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string Generate(Account account);
}
