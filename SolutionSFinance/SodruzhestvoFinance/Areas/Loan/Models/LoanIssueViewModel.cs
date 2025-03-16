using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SodruzhestvoFinance.Areas.Loan.Models
{
    public class LoanIssueViewModel
    {
        [Required(ErrorMessage = "Пайщик обязателен для выбора")]
        [Display(Name = "Пайщик")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Сумма займа обязательна для заполнения")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма займа должна быть больше 0")]
        [Display(Name = "Сумма займа")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Процентная ставка обязательна для заполнения")]
        [Range(0, 100, ErrorMessage = "Процентная ставка должна быть в диапазоне от 0 до 100")]
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

        // Списки для отображения DropDownList
        public List<SelectListItem>? Employees { get; set; }
    }
}
