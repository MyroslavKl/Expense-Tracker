using Expenses.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Api.Data
{
    public class ExpenseDbContext: DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options): base(options)
        {
                    
        }

        public DbSet<Expense> Expenses => Set<Expense>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(e =>
            {
                e.Property(x => x.Amount).HasPrecision(18, 2);
                e.Property(x => x.Currency).HasMaxLength(3).IsFixedLength();
                e.Property(x => x.Description).HasMaxLength(200);
            });
        }
    }
}
