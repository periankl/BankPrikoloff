namespace BankPrikoloff.Contracts
{
    public class CreateDocumentRequest
    {
        public string DocumentId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}