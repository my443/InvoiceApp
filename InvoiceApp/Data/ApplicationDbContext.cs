using InvoiceApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Define the SQLite connection string (replace with your desired file path)
                optionsBuilder.UseSqlite("Data Source=C:\\Users\\jvand\\source\\repos\\InvoiceApp\\InvoiceDatabase.db3");
            }
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<ExpenseStatus> ExpenseStatus { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<GLAccount> GLAccounts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
