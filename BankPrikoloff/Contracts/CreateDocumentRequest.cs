namespace BankPrikoloff.Contracts
{
    public class CreateDocumentRequest
    {
        public string ClientId { get; set; } = null!;
        public int TypeId { get; set; }
    }
}