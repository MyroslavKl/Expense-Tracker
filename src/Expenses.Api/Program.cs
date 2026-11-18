using Expenses.Api.Data;
using Expenses.Api.Models;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExpenseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExpensesDb"))
           .UseSnakeCaseNamingConvention());

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("ExpensesDb")!, name: "postgres")
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!, name: "redis");
    

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/expenses/{id}", async Task<Results<Ok<Expense>, NotFound>> (Guid id, ExpenseDbContext db) =>
   await db.Expenses.FindAsync(id) is Expense expense
         ? TypedResults.Ok(expense)
         : TypedResults.NotFound())
    .WithName("GetExpenseById");

app.MapPost("/expenses", 
    async Task<CreatedAtRoute<Expense>> (CreateExpense expense, ExpenseDbContext db) => 

{
    var created = new Expense
    {
        Amount = expense.Amount,
        Description = expense.Description,
        ExpenseDate = expense.ExpenseDate
    };

    db.Expenses.Add(created);
    await db.SaveChangesAsync();

    return TypedResults.CreatedAtRoute(created, "GetExpenseById", new { id = created.Id  });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();


public record CreateExpense(decimal Amount, string Description, DateOnly ExpenseDate);

