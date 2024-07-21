using ECommerce.Ordering.API;
using ECommerce.Ordering.Application;
using ECommerce.Ordering.Infrastructure;
using ECommerce.Ordering.Infrastructure.Data.Extensions;
using ECommerce.Shared.Exceptions;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the application

builder.Services
       .AddApplicationServices()
       .AddInfrastructureServices(builder.Configuration)
       .AddWebServices(builder.Configuration);
    
var app = builder.Build();

// Configure the Http Request Pipeline
app.UseWebServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();
