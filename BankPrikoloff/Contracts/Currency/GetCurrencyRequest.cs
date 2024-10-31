namespace BankPrikoloff.Contracts
{
    public class GetCurrencyRequest
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Course { get; set; }
    }
}