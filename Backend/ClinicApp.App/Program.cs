using ClinicApp.App.Configuration;
using ClinicApp.Infrastructure.Authentication.IdentityCore;


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
    
    // using (IServiceScope scope = app.Services.CreateScope())
    // {
    //     IServiceProvider services = scope.ServiceProvider;
    //
    //     try
    //     {
    //         AccountSeeder accountSeeder = services.GetRequiredService<AccountSeeder>();
    //         await accountSeeder.SeedAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //  
    //     }
    // }
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapIdentityApi<User>();

app.MapControllers();

app.Run();
