namespace BankPrikoloff.Contracts
{
    public class GetMessageRequest
    {
        public int MessageId { get; set; }
        public int StatusId { get; set; }
        public int TredId { get; set; }
        public string ClientId { get; set; } = null!;

        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}