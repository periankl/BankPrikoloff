namespace BankPrikoloff.Contracts
{
    public class CreateLoanRequest
    {
        public string? AccountId { get; set; }
        public int LoanTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime EndDate { get; set; }
    }
}