using LossPrevention.Api.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Loss Prevention & Goods Security API",
        Version = "v1",
        Description = "Production .NET 8 Web API backend connecting to RFID_ReaderDB for multi-store telemetry, incident detection, and operational analytics."
    });
});

// 2. Register Database Connection Factory
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

// 3. Configure CORS Policy
var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>() 
    ?? new[] { "http://localhost:5173", "http://127.0.0.1:5173", "http://localhost:3000" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// 4. Configure HTTP Request Pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loss Prevention API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
