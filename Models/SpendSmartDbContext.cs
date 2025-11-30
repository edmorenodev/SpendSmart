using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext : DbContext
    {
        // DbSet<T>: Representa una tabla
        // DbSet<Expense>: Representa la tabla de expenses
        public DbSet<Expense> Expenses { get; set; }

        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options)
            : base(options)
        {
        }
    }
}