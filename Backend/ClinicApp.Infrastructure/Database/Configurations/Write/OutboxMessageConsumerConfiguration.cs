using ClinicApp.Infrastructure.Database.Constants;
using ClinicApp.Infrastructure.Database.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        builder.ToTable(TableNames.OutboxMessageConsumers);

        builder.HasKey(outboxMessageConsumer => new
        {
            outboxMessageConsumer.Id,
            outboxMessageConsumer.Name
        });
    }
}
