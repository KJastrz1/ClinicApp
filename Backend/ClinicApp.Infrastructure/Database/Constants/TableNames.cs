namespace ClinicApp.Infrastructure.Database.Constants;

internal static class TableNames
{
    internal const string Users = nameof(Users);

    internal const string Appointments = nameof(Appointments);

    internal const string OutboxMessages = nameof(OutboxMessages);

    internal const string OutboxMessageConsumers = nameof(OutboxMessageConsumers);

    internal const string Roles = nameof(Roles);

    internal const string Permissions = nameof(Permissions);
}
