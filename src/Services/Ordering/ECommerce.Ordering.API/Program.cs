
var builder = WebApplication.CreateBuilder(args);

//Application Services
var assembly = typeof(Program).Assembly;
var productDbConnectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddMediatR(configuration =>
{
    //Add Handlers 
    configuration.RegisterServicesFromAssembly(assembly);
    //Add validation handlers
    configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});


var app = builder.Build();

// Configure the Http Request Pipeline

app.Run();
