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

        public DbSet<LoanStatus> LoanStatuses { get; set; }

        public DbSet<LoanTransaction> LoanTransaction { get; set; }

        public DbSet<TransactionType> TransactionType { get; set; }

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

            modelBuilder.Entity<LoanTransaction>()
                .Property(lt => lt.Amount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<LoanTransaction>()
                .Property(lt => lt.NewCurrentBalance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<LoanStatus>().HasData(
                new LoanStatus { LoanStatusId = 1, StatusName = "Новый", Description = "Займ создан, но еще не активирован" },
                new LoanStatus { LoanStatusId = 2, StatusName = "Активный", Description = "Займ выдан и по нему производятся платежи" },
                new LoanStatus { LoanStatusId = 3, StatusName = "Просрочен", Description = "Платежи просрочены" },
                new LoanStatus { LoanStatusId = 4, StatusName = "Погашен", Description = "Займ полностью выплачен" },
                new LoanStatus { LoanStatusId = 5, StatusName = "Списан", Description = "Займ списан как безнадежный" }
            );

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType { TransactionTypeId = 1, TransactionTypeName = "Выдача займа", Description = "Выдача денежных средств заемщику" },
                new TransactionType { TransactionTypeId = 2, TransactionTypeName = "Платеж", Description = "Погашение займа" },
                new TransactionType { TransactionTypeId = 3, TransactionTypeName = "Начисление процентов", Description = "Начисление процентов по займу" },
                new TransactionType { TransactionTypeId = 4, TransactionTypeName = "Штраф за просрочку", Description = "Начисление штрафа за просрочку платежа" },
                new TransactionType { TransactionTypeId = 5, TransactionTypeName = "Списание займа", Description = "Списание займа как безнадежного" },
                new TransactionType { TransactionTypeId = 6, TransactionTypeName = "Корректировка", Description = "Корректировка суммы долга" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
