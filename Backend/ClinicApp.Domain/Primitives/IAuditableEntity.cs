﻿namespace ClinicApp.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; }

    DateTime? ModifiedOnUtc { get;  }
}
