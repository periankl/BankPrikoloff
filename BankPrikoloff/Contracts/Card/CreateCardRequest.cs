namespace BankPrikoloff.Contracts
{
    public class CreateCardRequest
    {
        public int TypeId { get; set; }
        public string AccountId { get; set; } = null!;
        public string OwnerName { get; set; } = null!;

    }
}