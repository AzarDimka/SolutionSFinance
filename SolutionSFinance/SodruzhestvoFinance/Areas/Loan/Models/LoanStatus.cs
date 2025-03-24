namespace SodruzhestvoFinance.Areas.Loan.Models
{
    public class LoanStatus
    {
        public int LoanStatusId { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
