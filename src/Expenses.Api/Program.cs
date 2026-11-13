using Expenses.Api.Data;
using Expenses.Api.Models;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExpenseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExpensesDb")));

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("ExpensesDb")!, name: "postgres")
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!, name: "redis");
    

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/get-expense", async Task<Expense?> (Guid id, ExpenseDbContext db) => 
{
    var expense = await db.Expenses.FirstOrDefaultAsync(obj => obj.Id == id);

    return expense;
});

app.MapPost("/create-expense", async Task<Expense> (CreateExpense expense, ExpenseDbContext db) => 
{
    var created = new Expense
    {
        Amount = expense.amount,
        Description = expense.description,
        ExpenseDate = expense.expenseDate
    };

    db.Expenses.Add(created);
    await db.SaveChangesAsync();

    return created;
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();


public record CreateExpense(decimal amount, string description, DateTime expenseDate);

