namespace Expenses.Api.Models
{
    public class Expense
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "UAN";
        public string Description { get; set; } = string.Empty;
        public DateOnly ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
