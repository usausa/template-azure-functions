using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Smart.Data;
using Smart.Data.Accessor.Extensions.DependencyInjection;

using Template.Components.Json;
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

// JSON
builder.Services.ConfigureHttpJsonOptions(static options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new Template.Components.Json.DateTimeConverter());
});

// System
builder.Services.AddSingleton(TimeProvider.System);

// Data
var connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
builder.Services.AddSingleton<IDbProvider>(new DelegateDbProvider(() => new SqlConnection(connectionString)));
builder.Services.AddDataAccessor(static c =>
{
    c.EngineOption.ConfigureTypeMap(static map =>
    {
        map[typeof(DateTime)] = DbType.DateTime2;
    });
});

// Service
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<Service>();

builder.Build().Run();
