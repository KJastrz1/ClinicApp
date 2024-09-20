using ClinicApp.Domain.Primitives;
using ClinicApp.Infrastructure.Database;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Infrastructure.Database.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace ClinicApp.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly WriteDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(WriteDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                continue;
            }

            await _publisher.Publish(domainEvent, context.CancellationToken);

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
