using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Database");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

//Add services
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
    options.Connection(dbConnectionString!);
    options.Schema.For<ShoppingCart>().Identity(s => s.Username);
}).UseLightweightSessions();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = redisConnectionString;
});

builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.Decorate<IBasketRepository,CachedBasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnectionString!)
    .AddRedis(redisConnectionString!);

var app = builder.Build();

// Configure Http Request pipeline

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// Configure Http Pipeline
app.MapCarter();

//Custom Exception Handler 
app.UseExceptionHandler(options => { });

app.Run();
