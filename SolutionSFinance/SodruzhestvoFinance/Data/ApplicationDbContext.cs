using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SodruzhestvoFinance.Areas.Administration.Models;
using SodruzhestvoFinance.Areas.Employees.Models;
using SodruzhestvoFinance.Areas.Loan.Models;

namespace SodruzhestvoFinance.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.CurrentBalance)
                    .HasPrecision(18, 2);

                entity.Property(e => e.InterestRate)
                    .HasPrecision(5, 4);

                entity.Property(e => e.LoanAmount)
                    .HasPrecision(18, 2);
            });
        }
    }
}
