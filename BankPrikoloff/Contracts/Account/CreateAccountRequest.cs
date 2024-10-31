namespace BankPrikoloff.Contracts
{
    public class CreateAccountRequest
    {
        public string ClientId { get; set; } = null!;
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
    }
}