using System.Reflection;

namespace ECommerce.Basket.API.Helpers;

public static class ECommerceModules
{
    public static void LoadModulesFromAssembly(IEndpointRouteBuilder endpoints)
    {
        // Get the assembly where your modules are defined (main project assembly)
        var assembly = Assembly.GetExecutingAssembly();

        // Get all types in the assembly that inherit from CarterModule
        var moduleTypes = assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(ICarterModule)));

        // Iterate through each module type and map its routes
        foreach (var moduleType in moduleTypes)
        {
            var moduleInstance = Activator.CreateInstance(moduleType) as ICarterModule;
            moduleInstance.AddRoutes(endpoints);
        }
    }
}

