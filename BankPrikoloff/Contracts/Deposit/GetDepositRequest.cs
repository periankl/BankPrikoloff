namespace BankPrikoloff.Contracts
{
    public class GetDepositRequest
    {
        public string DepositId { get; set; } = null!;
        public int DepositTypeId { get; set; }
        public int StatusId { get; set; }
        public string DocumentId { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}