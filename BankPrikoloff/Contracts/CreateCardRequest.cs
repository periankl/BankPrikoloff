namespace BankPrikoloff.Contracts
{
    public class CreateCardRequest
    {
        public string CardId { get; set; } = null!;
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
        public string AccountId { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public DateTime ExpDate { get; set; }
        public string Cvv { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool Blocked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? BlockedAt { get; set; }
    }
}