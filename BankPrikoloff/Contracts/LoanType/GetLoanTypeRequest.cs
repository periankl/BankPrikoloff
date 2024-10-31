namespace BankPrikoloff.Contracts
{
    public class GetLoanTypeRequest
    {

        public int LoanTypeId { get; set; }
        public string Name { get; set; } = null!;
        public decimal InterestRate { get; set; }
        public decimal MaxLoanAmount { get; set; }
    }
}