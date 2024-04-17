using Carter;

var builder = WebApplication.CreateBuilder(args);

//Add Dependencies  and services to DI
builder.Services.AddCarter();

builder.Services.AddMediatR(configuration => 
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMarten(options => {
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();


var app = builder.Build();

// Configure Http Pipeline
app.MapCarter();

// Run app
app.Run();

