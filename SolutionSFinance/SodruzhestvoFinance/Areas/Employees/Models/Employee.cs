using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SodruzhestvoFinance.Areas.Employees.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения.")]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения.")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле 'Отчество' обязательно для заполнения.")]
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле 'Должность' обязательно для заполнения.")]
        [DisplayName("Должность")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Поле 'Телефон' обязательно для заполнения.")]
        [DisplayName("Телефон")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Поле 'Зарплата' обязательно для заполнения.")]
        [DisplayName("Зарплата")]
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }

        // Navigation properties
        //public ICollection<Loan> Loans { get; set; }  // Если Loan находится в другом Area, то нужно будет поправить namespace
        //public ICollection<FinancialTransaction> FinancialTransactions { get; set; } // Аналогично для FinancialTransaction
    }
}

