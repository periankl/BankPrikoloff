namespace BankPrikoloff.Contracts
{
    public class GetOperationHistoryRequest
    {
        public string OperationId { get; set; } = null!;
        public string SenderAccountId { get; set; } = null!;
        public string? SenderCardId { get; set; }
        public string DestinationAccountId { get; set; } = null!;
        public string? DestinationCardId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}