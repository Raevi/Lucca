using Infrastructure.SqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer
{
    public class ExpenseContext : DbContext
    {
        public DbSet<ExpenseDao> Expenses { get; set; }
        public DbSet<UserDao> Users { get; set; }

        public ExpenseContext(DbContextOptions<ExpenseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDao>().ToTable("Users");
            modelBuilder.Entity<ExpenseDao>().ToTable("Expenses");
        }
    }
}

