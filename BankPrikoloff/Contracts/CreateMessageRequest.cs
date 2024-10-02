namespace BankPrikoloff.Contracts
{
    public class CreateMessageRequest
    {
        public int TredId { get; set; }
        public string ClientId { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}