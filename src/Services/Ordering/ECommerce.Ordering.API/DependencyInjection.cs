
using Carter;
using ECommerce.Shared.Exceptions;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ECommerce.Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString=configuration.GetConnectionString("Database");
        var assembly = typeof(Program).Assembly;
        //Add Services
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks().AddSqlServer(dbConnectionString!);
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }

    public static WebApplication UseWebServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
        return app;
    }
}
