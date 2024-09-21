namespace BankPrikoloff.Contracts
{
    public class GetDepositTypeRequest
    {
        public int DepositTypeId { get; set; }
        public string Name { get; set; } = null!;
        public decimal InterestRate { get; set; }
        public decimal? MinAmount { get; set; }
        public int? MinTerm { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
