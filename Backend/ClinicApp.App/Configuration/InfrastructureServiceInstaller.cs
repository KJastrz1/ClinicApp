using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Repositories.Read;
using ClinicApp.Infrastructure;
using ClinicApp.Infrastructure.Database;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Infrastructure.Database.Interceptors;
using ClinicApp.Infrastructure.Database.Repositories.Read;
using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace ClinicApp.App.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new InvalidOperationException("Connection string 'Database' not found.");

        services.AddSingleton(connectionString);
        
        services.AddDbContext<WriteDbContext>(
            optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();


        // services.AddDbContext<ReadDbContext>(
        //     optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
    }
}
