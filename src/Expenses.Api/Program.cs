using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("ExpensesDb")!, name: "postgres")
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!, name: "redis");
    

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

