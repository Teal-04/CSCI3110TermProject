using Microsoft.EntityFrameworkCore;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<CategorySummary> CategorySummaries { get; set; }
    }
}
