

using ECommerce.Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var productDbConnectionString = builder.Configuration.GetConnectionString("Database");
//Add Dependencies  and services to DI
builder.Services.AddMediatR(configuration =>
    {
        //Add Handlers 
        configuration.RegisterServicesFromAssembly(assembly);
        //Add validation handlers
        configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        configuration.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    });

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(productDbConnectionString!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(productDbConnectionString!);

var app = builder.Build();

// Configure Http Pipeline
app.MapCarter();

//Custom Exception Handler 
app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
// Run app
app.Run();


