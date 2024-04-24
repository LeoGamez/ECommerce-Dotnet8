

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

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
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure Http Pipeline
app.MapCarter();

//Custom Exception Handler 
app.UseExceptionHandler(options => { });

//-> removed to use custom Exception handler
//app.UseExceptionHandler(exceptionHandlerApp => {

//    exceptionHandlerApp.Run(async context => {

//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (exception == null)
//        {
//            return;
//        }

//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace,
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType="application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

// Run app
app.Run();

