namespace BankPrikoloff.Contracts
{
    public class CreateFileRequest
    {
        public string FileId { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int MessageId { get; set; }
        public string ClientId { get; set; } = null!;
        public DateTime UploadAt { get; set; }
    }
}
