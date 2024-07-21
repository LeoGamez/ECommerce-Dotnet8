
using ECommerce.Ordering.Application.Data;
using ECommerce.Ordering.Infrastructure.Data.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventsDispatchInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp,options )=> {
            options.AddInterceptors(new AuditableEntityInterceptor(),
                                    new DomainEventsDispatchInterceptor(sp.GetRequiredService<IMediator>()));
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
