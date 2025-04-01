using System.ComponentModel.DataAnnotations.Schema;

namespace SodruzhestvoFinance.Areas.Loan.Models
{
    [Table("LoanTransaction")]
    public class LoanTransaction
    {
        public int LoanTransactionId { get; set; }
        public int LoanId { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal NewCurrentBalance { get; set; }

        [ForeignKey("LoanId")]
        public virtual Loan Loan { get; set; }

        [ForeignKey("TransactionTypeId")]
        public virtual TransactionType TransactionType { get; set; }
    }
}
