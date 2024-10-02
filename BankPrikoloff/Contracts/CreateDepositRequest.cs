namespace BankPrikoloff.Contracts
{
    public class CreateDepositRequest
    {
        public int DepositTypeId { get; set; }
        public int StatusId { get; set; }
        public string AccountId { get; set; } = null!;
        public DateTime? EndDate { get; set; }
    }
}