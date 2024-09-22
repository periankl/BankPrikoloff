namespace BankPrikoloff.Contracts
{
    public class CreateLoanTypeRequest
    {
        public string Name { get; set; } = null!;
        public decimal InterestRate { get; set; }
        public decimal MaxLoanAmount { get; set; }
    }
}