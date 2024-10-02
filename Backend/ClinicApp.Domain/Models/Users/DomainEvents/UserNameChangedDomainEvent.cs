﻿using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Users.DomainEvents;

public sealed record UserNameChangedDomainEvent(Guid Id, Guid UserId) : DomainEvent(Id);