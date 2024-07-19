
namespace ECommerce.Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        //Add Services

        return services;
    }

    public static WebApplication UseWebServices(this WebApplication app)
    {

        return app;
    }
}
