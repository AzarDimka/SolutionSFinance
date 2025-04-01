namespace SodruzhestvoFinance.Areas.Loan.Models
{
    public class LoanTransactionsViewModel
    {
        public List<LoanTransaction> Transactions { get; set; }
        public int? LoanId { get; set; } // Nullable int: будет null, если показываем все транзакции
    }
}
