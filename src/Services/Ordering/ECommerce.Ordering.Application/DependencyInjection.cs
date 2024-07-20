﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Add Mediatr
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
