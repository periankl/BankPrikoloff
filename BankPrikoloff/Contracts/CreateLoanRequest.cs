namespace BankPrikoloff.Contracts
{
    public class CreateLoanRequest
    {
        public string LoanId { get; set; } = null!;
        public string? AccountId { get; set; }
        public int LoanTypeId { get; set; }
        public string DocumentId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime EndDate { get; set; }
    }
}