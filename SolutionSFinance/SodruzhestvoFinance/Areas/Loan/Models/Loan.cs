using SodruzhestvoFinance.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SodruzhestvoFinance.Areas.Loan.Models
{
    [Table("Loan")]
    public class Loan
    {
        public int LoanId { get; set; }

        [Required(ErrorMessage = "Пайщик обязателен для выбора")]
        [Display(Name = "Пайщик")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Сумма займа обязательна для заполнения")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма займа должна быть больше 0")]
        [Display(Name = "Сумма займа")]
        public decimal LoanAmount { get; set; }

        //[Required(ErrorMessage = "Процентная ставка обязательна для заполнения")]
        //[Range(0, 100, ErrorMessage = "Процентная ставка должна быть в диапазоне от 0 до 100")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true, NullDisplayText = "")] // Два знака после запятой
        [Display(Name = "Процентная ставка (%)")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Срок займа обязателен для заполнения")]
        [Range(1, 120, ErrorMessage = "Срок займа должен быть в диапазоне от 1 до 120 месяцев")]
        [Display(Name = "Срок займа (месяцы)")]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Дата выдачи обязательна для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата выдачи")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Текущий остаток долга")]
        public decimal CurrentBalance { get; set; }

        [Display(Name = "Статус")]
        public int LoanStatusId { get; set; } // Foreign Key to LoanStatus

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("LoanStatusId")]
        public virtual LoanStatus LoanStatus { get; set; }

        public virtual ICollection<LoanTransaction> LoanTransactions { get; set; }
    }
}
