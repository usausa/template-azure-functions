using System.Text.Json;
using System.Text.Json.Serialization;

using AzureFunctionsExtension;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Data.SqlClient;

using Smart.Data;

using Template.Accessors;
using Template.Functions.Functions;
using Template.Functions.Infrastructure;
using Template.Services;

var builder = FunctionsApplication.CreateBuilder(args);

// Web
builder.ConfigureFunctionsWebApplication();

// Middleware
builder.UseMiddleware<ExceptionLoggingMiddleware>();

// Telemetry
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// Extension
builder.Services.Configure<JsonOptions>(static options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new Template.Components.Json.DateTimeConverter());
});
builder.Services.AddAzureFunctionExtension();

// System
builder.Services.AddSingleton(TimeProvider.System);

// Data
var connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
builder.Services.AddSingleton<IDbProvider>(new DelegateDbProvider(() => new SqlConnection(connectionString)));
builder.Services.AddDataAccessors(typeof(DataAccessor).Assembly);

// Service
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<Service>();

// Function
builder.Services.AddTransient<HttpFunction>();
builder.Services.AddTransient<DataFunction>();
builder.Services.AddTransient<QueueFunction>();
builder.Services.AddTransient<TimerFunction>();

builder.Build().Run();
