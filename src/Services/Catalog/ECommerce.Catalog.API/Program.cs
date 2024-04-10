var builder = WebApplication.CreateBuilder(args);

//Add Dependencies  and services to di

var app = builder.Build();

// Configure Http Pipeline

app.Run();
