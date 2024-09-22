namespace BankPrikoloff.Contracts
{
    public class GetTredRequest
    {
        public int TredId { get; set; }
        public int ChatId { get; set; }
        public string? OperatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
