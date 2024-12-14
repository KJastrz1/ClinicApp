using ClinicApp.App.Configuration;
using ClinicApp.Infrastructure.Authentication.IdentityCore;
using ClinicApp.Infrastructure.Database.DataSeeders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
        builder.Configuration,
        typeof(IServiceInstaller).Assembly);

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    if (app.Environment.IsDevelopment())
    {
        IEnumerable<IDataSeeder> seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (IDataSeeder seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
    else if (app.Environment.IsProduction())
    {
        RoleSeeder roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
        await roleSeeder.SeedAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapIdentityApi<User>();

app.MapControllers();

app.Run();
