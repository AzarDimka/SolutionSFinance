namespace SodruzhestvoFinance.Areas.Loan.Models
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LoanTransaction> LoanTransactions { get; set; }
    }
}
