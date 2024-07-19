using ECommerce.Ordering.API;
using ECommerce.Ordering.Application;
using ECommerce.Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the application

builder.Services
       .AddApplicationServices()
       .AddInfrastructureServices(builder.Configuration)
       .AddWebServices();


var app = builder.Build();

// Configure the Http Request Pipeline
app.UseWebServices();

app.Run();
