using ClinicApp.App.Configuration;
using ClinicApp.Infrastructure.Database.DataSeeders;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
        builder.Configuration,
        typeof(IServiceInstaller).Assembly);

WebApplication app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using (IServiceScope scope = app.Services.CreateScope())
    {
        IServiceProvider services = scope.ServiceProvider;

        try
        {
            AccountSeeder userSeeder = services.GetRequiredService<AccountSeeder>();
            await userSeeder.SeedAsync();
        }
        catch (Exception ex)
        {
     
        }
    }
}

app.UseHttpsRedirection();

// app.UseAuthentication();
//
// app.UseAuthorization();

app.MapControllers();

app.Run();
