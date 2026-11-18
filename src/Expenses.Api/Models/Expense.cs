namespace Expenses.Api.Models
{
    public class Expense
    {
        public Guid Id { get; } = Guid.CreateVersion7();
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "UAH";
        public string Description { get; set; } = string.Empty;
        public DateOnly ExpenseDate { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}
